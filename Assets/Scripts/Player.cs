using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
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

    public GameObject bulletPrefab;
    float lastShoot;

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
                // ÂøÁö
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
                //°è¼Ó °È´ÂÁß
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
                //Á¡ÇÁ ½ÃÀÛ
                GetComponent<Animator>().SetTrigger("Jump");
            }

            isGrounded= false;
        }


        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            GetComponent<Rigidbody2D>().velocity = Vector2.up * jumpSpeed;
        }


        prevVx = vx;

        //ÃÑ¾Ë ¹ß»ç
        if(Input.GetButtonDown("Fire1") && lastShoot + 0.5f < Time.time)
        {
            Vector2 bulletV = Vector2.zero;

            if(GetComponent<SpriteRenderer>().flipX)
            {
                bulletV = new Vector2(-10, 0);
            }
            else
            {
                bulletV = new Vector2(10, 0);
            }

            GameObject bullet = ObjectPool.Instance.GetBullet();
            bullet.transform.position = transform.position;
            bullet.GetComponent<Bullet>().velocity = bulletV;
            lastShoot = Time.time;
        }

    }

    
    private void FixedUpdate()
    {
        transform.Translate(Vector2.right * vx * speed * Time.fixedDeltaTime);
    }
}
