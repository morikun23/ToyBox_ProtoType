using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player2 : MonoBehaviour {

    Rigidbody2D rigid;
    [SerializeField]
    float moveSpeed = 1;

    SpriteRenderer sprite;

    enum State
    {
        Ground,
        Actioning,
        Attract,//引き寄せる
        Strain,//糸を張る
        HangDown
    }
    State state;

    Vector3 mousePos;

    Vector3 direction, addVector;

    Animator anim;

    //手
    public GameObject hand;
    [SerializeField]
    float addExtend = 0.3f;
    float extendNum = 0;

    //伸ばしているかどうか
    bool handExtend;
    //掴んでいるかどうか
    bool hangOn;

    //Hand2
    Hand2 handScript;

    //レティクル
    [SerializeField]
    GameObject tage;

    Animator tageAnime;

    [SerializeField]
    GameObject colOriginal;

    GameObject col;

    // Use this for initialization
    void Start () {

        state = State.Ground;
        rigid = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();

        handExtend = false;
        hangOn = false;

        handScript = hand.GetComponent<Hand2>();
        
        tageAnime = tage.transform.FindChild("Tage").GetComponent<Animator>();

        anim = GetComponent<Animator>();
    }
	
	// Update is called once per frame
	void Update () {

        mousePos = Input.mousePosition;
        mousePos.z = 10;
        mousePos = Camera.main.ScreenToWorldPoint(mousePos);

        Vector3 movement = new Vector3(mousePos.x - hand.transform.position.x, mousePos.y - hand.transform.position.y, 0);
        Vector3 newPos = hand.transform.position + movement;
        Vector3 offset = newPos - hand.transform.position; 

        mousePos = hand.transform.position + Vector3.ClampMagnitude(offset,4);
        

        switch (state)
        {

            case State.Ground:

                if (Input.GetMouseButtonDown(0))
                {
                    handExtend = true;
                    rigid.velocity = Vector2.zero;
                    rigid.gravityScale = 0;
                }

                if (Input.GetMouseButtonUp(0) || Vector2.Distance(hand.transform.position, tage.transform.position) < 0.1f)
                {
                    handExtend = false;
                    handScript.hang = false;
                }

                if (handExtend == true)
                {
                    extendNum += addExtend;
                    hand.transform.Translate(new Vector3(0, addExtend, 0));
                }
                else {

                    if (extendNum > 0)
                    {
                        if (handExtend == false)
                        {
                            extendNum -= addExtend * 2;
                            hand.transform.Translate(0, -addExtend * 2, 0);
                        }
                    }
                    else
                    {
                        hand.transform.position = transform.position;
                        rigid.gravityScale = 2;
                        extendNum = 0;
                    }

                    if (extendNum <= 0)
                    {
                        Ray2D ray = new Ray2D(transform.position, mousePos - transform.position);

                        int layeyrMask = ~(1 << 8 | 1 << 9 | 1 << 10 | 1 << 11);
                        RaycastHit2D hit = Physics2D.Raycast(transform.position, mousePos - transform.position, Vector2.Distance(transform.position, mousePos), layeyrMask);


                        if (hit.collider)
                        {
                            
                            tage.transform.position = hit.point;

                            if (hit.transform.gameObject.tag == "Grip" || hit.transform.gameObject.tag == "Strain")
                            {
                                tageAnime.SetBool("tageRed", true);
                            }
                            else
                            {
                                tageAnime.SetBool("tageRed", false);
                            }
                        }
                        else {
                            tage.transform.position = mousePos;
                            tageAnime.SetBool("tageRed", false);
                        }
                        Move_Graound();
                        hand.transform.rotation = Quaternion.FromToRotation(Vector3.up, mousePos - transform.position);
                    }

                }

                break;

            //引き寄せられる
            case State.Actioning:

                Actioning();

                if (Input.GetMouseButtonUp(0))
                {
                    Exit();
                }

                break;

            //引き寄せる
            case State.Attract:
                
                if (Input.GetMouseButtonUp(0))
                {
                    Exit();
                }

                break;

            //糸を張る
            case State.Strain:

                
                if (Input.GetMouseButtonUp(0))
                {
                    Destroy(col);
                    Exit();
                }

                break;

            case State.HangDown:

                transform.position = hand.transform.position;

                if (Input.GetMouseButtonUp(0))
                {
                    Exit();
                }

                break;
        }

        
    }

    //地上にいる時のムーブ関数
    void Move_Graound()
    {
        if (Input.GetAxisRaw("Horizontal") < 0)
        {
            sprite.flipX = true;
            //anim.SetBool("run", true);
        }
        
        if (Input.GetAxisRaw("Horizontal") > 0)
        {
            sprite.flipX = false;
            //anim.SetBool("run", true);
        }

        /*if (Input.GetAxisRaw("Horizontal") == 0)
        {
            anim.SetBool("run", false);
        }*/


        rigid.velocity = new Vector2(moveSpeed * Input.GetAxisRaw("Horizontal"), rigid.velocity.y);

        if (Input.GetKeyDown(KeyCode.W))
        {
            rigid.velocity = new Vector2(rigid.velocity.x, moveSpeed);
        }
    }

    //伸ばしているかどうか
    public bool isExtend()
    {
        return handExtend;
    }

    //掴んだ瞬間＆＆掴んだ対象が引き寄せ型でない場合、リ○クのフックショット的な動き
    void Actioning()
    {
        direction = hand.transform.position - transform.position;
        float rad = Mathf.Atan2(direction.y,
                                direction.x);
        Debug.Log(Vector2.Distance(hand.transform.position, transform.position));

        addVector = new Vector3(Mathf.Cos(rad), Mathf.Sin(rad), 0);
        addVector.Normalize();

        rigid.velocity = new Vector2(addVector.x, addVector.y) * moveSpeed * moveSpeed;

        if (Vector2.Distance(hand.transform.position, transform.position) < 1)
        {
            rigid.velocity = Vector2.zero;
            state = State.HangDown;
        }
    }

    //掴んでいる状態へ移行
    public void Act() {

        state = State.Actioning;

    }

    //掴み解除
    public void Exit()
    {
        state = State.Ground;
        handExtend = false;
        extendNum = 0;
        handScript.hang = false;
    }

    //引き寄せている状態へ移行
    public void Attract()
    {
        //コライダーを作る
        col = Instantiate(colOriginal, transform.position, Quaternion.identity) as GameObject;
        col.transform.rotation = Quaternion.FromToRotation(Vector3.up, hand.transform.position - col.transform.position);
        col.transform.position = (hand.transform.position + col.transform.position) / 2;
        col.transform.localScale = new Vector3(col.transform.localScale.x, (hand.transform.position - col.transform.position).magnitude * 2, col.transform.localScale.z);

        rigid.velocity = Vector2.zero;
        rigid.gravityScale = 2;
        state = State.Strain;

    }

    void OnCollisionEnter2D(Collision2D other)
    {

        if(other.gameObject.tag == "MoveFloor")
        {
            transform.parent = other.gameObject.transform;
        }

    }

    void OnCollisionExit2D(Collision2D other)
    {

        if (other.gameObject.tag == "MoveFloor")
        {
            transform.parent = null;
        }

    }


}
