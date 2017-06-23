using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    Rigidbody2D rigid;
    [SerializeField]
    float moveSpeed = 1;

    SpriteRenderer sprite;

    public GameObject hand;
    Hand handScript;

    Rigidbody2D handRigid;

    Vector3 mousePos;
    Vector3 direction,addVector;

    enum State
    {
        Ground,
        HangDown
    }

    State state;
    bool flag = false;

    public bool isHanging;

    float num = 0;
    public float addnum = 0.1f;

    SpringJoint2D spring;

    bool hangOn = false;

    // Use this for initialization
    void Start () {
        state = State.Ground;
        rigid = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
        handRigid = hand.GetComponent<Rigidbody2D>();
        handRigid.isKinematic = false;
        isHanging = false;

        handScript = hand.GetComponent<Hand>();
    }
	
	// Update is called once per frame
	void Update () {

        mousePos = Input.mousePosition;
        mousePos.z = 10;
        mousePos = Camera.main.ScreenToWorldPoint(mousePos);
       

        switch (state)
        {

            case State.Ground:
                handRigid.isKinematic = false;
                Move_Graound();

                if (Input.GetMouseButtonDown(0))
                {
                    
                    flag = true;
                    handRigid.isKinematic = true;
                    isHanging = true;
                }
                
                if (Input.GetMouseButtonUp(0))
                {

                    flag = false;
                    isHanging = false;
                }

                if (flag) {

                   /* direction = mousePos - transform.position;
                    float rad = Mathf.Atan2(mousePos.y - transform.position.y,
                                            mousePos.x - transform.position.x);
                    Debug.Log(rad);

                    addVector = new Vector3(Mathf.Cos(rad), Mathf.Sin(rad), 0);
                    addVector.Normalize();
                    */
                    //伸ばしたい
                    num += addnum;
                    hand.transform.Translate(0,num,0);
                }
                else
                {
                    if (num > 0)
                    {
                        if (flag == false)
                        {
                            num -= addnum * 5;
                            hand.transform.Translate(0, num * 5, 0);
                        }
                    }
                    else
                    {
                        hand.transform.position = transform.position;
                        num = 0;
                    }
                }

                if(num <= 0)
                {
                    hand.transform.rotation = Quaternion.FromToRotation(Vector3.up, mousePos - transform.position);
                }

                break;

            case State.HangDown:

                if (hangOn == false)
                {
                    if (handScript.GetHangFlag() == true)
                    {
                        StopActioning();
                    }
                    else
                    {
                        Actioning();
                    }
                }
                else {
                    Move_HangOn();
                }

                if (Input.GetMouseButtonUp(0))
                {
                    handScript.hang = false;
                    flag = false;
                    state = State.Ground;
                    Destroy(spring);
                    handRigid.velocity = Vector2.zero;
                    handRigid.constraints = RigidbodyConstraints2D.None;
                    handRigid.constraints = RigidbodyConstraints2D.FreezeRotation;
                    hangOn = false;
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
        }
        
        if(Input.GetAxisRaw("Horizontal") > 0)
        {
            sprite.flipX = false;
        }

        rigid.velocity = new Vector2(moveSpeed * Input.GetAxisRaw("Horizontal"), rigid.velocity.y);

        if (Input.GetKeyDown(KeyCode.W) && rigid.velocity.y == 0)
        {
            rigid.velocity = new Vector2(rigid.velocity.x, moveSpeed);
        }
    }

    //掴んだ瞬間＆＆掴んだ対象が引き寄せ型でない場合、リ○クのフックショット的な動き
    void Actioning()
    {
        direction = hand.transform.position - transform.position;
        float rad = Mathf.Atan2(direction.y,
                                direction.x);
        Debug.Log(Vector2.Distance(hand.transform.position,transform.position));

        addVector = new Vector3(Mathf.Cos(rad), Mathf.Sin(rad), 0);
        addVector.Normalize();

        rigid.velocity = new Vector2(addVector.x, addVector.y) * moveSpeed * moveSpeed;

        if(Vector2.Distance(hand.transform.position, transform.position) < 1)
        {
            rigid.velocity = Vector2.zero;
            hangOn = true;
        }
    }
    
    //掴んだ瞬間＆＆掴んだ対象が引き寄せ型である場合、その場でストップ
    void StopActioning()
    {
        spring.distance = Vector2.Distance(hand.transform.position, transform.position);
        rigid.velocity = Vector2.zero;
        hangOn = true;
        
    }

    //ぶら下がり時のムーブ関数
    void Move_HangOn()
    {

        if (Input.GetAxisRaw("Horizontal") < 0)
        {
            sprite.flipX = true;
        }

        if (Input.GetAxisRaw("Horizontal") > 0)
        {
            sprite.flipX = false;
        }

        
        direction = hand.transform.position - transform.position;
        float rad = Mathf.Atan2(direction.y,
                                direction.x);
        Debug.Log(direction);

        addVector = new Vector3(Mathf.Cos(rad), Mathf.Sin(rad), 0);
        addVector.Normalize();

        if (Input.GetKey(KeyCode.A))
        {
            rigid.AddForce(new Vector2(-0.0005f,0));
        }
        if (Input.GetKey(KeyCode.D))
        {
            rigid.AddForce(new Vector2(0.0005f, 0));
        }
        if (Input.GetKey(KeyCode.W))
        {
            spring.distance -= 0.1f;
            //transform.position -= new Vector3(0,-0.1f,0);
        }
        if (Input.GetKey(KeyCode.S))
        {
            spring.distance += 0.1f;
            //transform.position -= new Vector3(0, 0.1f, 0);
        }
    }

    //ぶら下がり状態へ遷移
    public void HangOn()
    {

        state = State.HangDown;
        spring = hand.AddComponent<SpringJoint2D>();
        spring.connectedBody = rigid;
        spring.dampingRatio = 1;
        spring.distance = 0;
        spring.autoConfigureDistance = false;
        isHanging = false;
        handRigid.constraints = RigidbodyConstraints2D.FreezeAll;
        //rigid.gravityScale = 0;
    }

    //ぶら下がり中じゃないかを見る
    public bool isGround()
    {

        if (state == State.Ground)
        {
            return true;
        }
        else {
            return false;
        }
    }
}
