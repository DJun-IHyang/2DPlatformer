using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    public float speed = 7;
    public float jumpSpeed = 15;
    public Collider2D bottomCollider;
    public CompositeCollider2D terrainCollider;

    float vx = 0;
    float prevVx = 0;
    bool isGrounded;
    Vector2 originPosition;

    // Start is called before the first frame update
    void Start()
    {
        originPosition = transform.position;
    }

    public void Restart()
    {
        transform.position = originPosition;
        GetComponent<Rigidbody2D>().velocity = Vector2.zero;
    }

    // Update is called once per frame
    void Update()
    {
        vx = Input.GetAxisRaw("Horizontal");

        if(vx < 0 )
        {
            GetComponent<SpriteRenderer>().flipX = true;
        }

        if(vx>0)
        {
            GetComponent<SpriteRenderer>().flipX = false;

        }

        if ( bottomCollider.IsTouching(terrainCollider))
        {
            if (!isGrounded)
            {
                // 착지
                if(vx==0)
                {
                    GetComponent<Animator>().SetTrigger("Idle");
                }
                else
                {
                    GetComponent<Animator>().SetTrigger("Run");
                }
            }
            else
            {
                //계속 걷는중
                if(prevVx != vx)
                {
                    if (vx == 0)
                    {
                        GetComponent<Animator>().SetTrigger("Idle");
                    }
                    else
                    {
                        GetComponent<Animator>().SetTrigger("Run");
                    }
                }
            }
            isGrounded = true;
        }
        else
        {
            if(isGrounded)
            {
                //점프 시작
                GetComponent<Animator>().SetTrigger("Jump");
            }

            isGrounded= false;
        }


        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            GetComponent<Rigidbody2D>().velocity = Vector2.up * jumpSpeed;
        }


        prevVx = vx;
    }


    private void FixedUpdate()
    {
        transform.Translate(Vector2.right * vx * speed * Time.fixedDeltaTime);
    }
}
