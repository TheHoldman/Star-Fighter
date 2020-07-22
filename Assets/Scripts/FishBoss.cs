using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishBoss : MonoBehaviour
{
    enum BossStatus
    {
        attacking,waiting,dead
    }
    enum MoveDirection
    {
        down, up, stop
    }

    private BossStatus bossStatus = new BossStatus();
    private MoveDirection moveDirection = new MoveDirection();

    public int life;
    public int collisionDamage;
    public int scoreReward;

    public float speed;
    public float attackSpeed;
    private float attackTime;
    public float maxAttackTime;
    public float minAttackTime;

    public Rigidbody2D rb;

    private float fishMaxYPosition;
    private float fishMaxXPosition;
    Camera mainCamera;

    private bool isAttackLeft = true;

    void Start()
    {
        mainCamera = Camera.main;
        fishMaxYPosition = mainCamera.ScreenToWorldPoint(new Vector3(0f, Screen.height, 0f)).y;
        fishMaxXPosition = mainCamera.ScreenToWorldPoint(new Vector3(Screen.width * 0.87f, 0f, 0f)).x;

        bossStatus = BossStatus.waiting;

        rb.AddForce(new Vector2(-250f, 0f));

        moveDirection = MoveDirection.up;

        attackTime = Random.Range(minAttackTime, maxAttackTime);
    }

    
    void Update()
    {    
        if (attackTime <= 0)
        {
            bossStatus = BossStatus.attacking;
        }
        else if(bossStatus == BossStatus.waiting)
        {
            attackTime -= Time.deltaTime;
        }

        if (bossStatus == BossStatus.attacking)
        {
            Attack();
        }
        else
        {
            Move();
        }
    }

    private void Attack()
    {
     
        if (isAttackLeft)
        {
            Vector2 newPosition = new Vector2(-1f, 0f) * Time.deltaTime * attackSpeed;
            transform.Translate(newPosition, Space.World);
        }
        else
        {            
            Vector2 newPosition = new Vector2(1f, 0f) * Time.deltaTime * attackSpeed;
            transform.Translate(newPosition, Space.World);                    
        }

        if (checkIfAtXEdge())
        {
            bossStatus = BossStatus.waiting;

            isAttackLeft = true;

            attackTime = Random.Range(5f, maxAttackTime);
        }
               
    }

    private bool checkIfAtXEdge()
    {
        if (transform.position.x < - fishMaxXPosition)
        {
            isAttackLeft = false;
            return false;
        }
        else if (transform.position.x > fishMaxXPosition -1f)
        {
            Vector2 newPosition = new Vector2(-2f, 0f) * Time.deltaTime * attackSpeed;
            transform.Translate(newPosition, Space.World);          

            return true;
        }
        else
        {
            return false;
        }        
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Ship")
        {
            collision.GetComponentInParent<Ship>().Damage(collisionDamage);
        }
    }

    public void Damage(int damage)
    {
        life -= damage;

        if (life <= 0)
        {
            //GameObject cloneDeath = Instantiate(deathAnimation, transform.position, new Quaternion());
            //Destroy(cloneDeath, 2f);
            Destroy(gameObject);

            GameManagerScript.score += scoreReward;
        }
    }

    private void Move()
    {
        checkIfAtYEdge();

        if (moveDirection == MoveDirection.down)
        {
            Vector2 newPosition = new Vector2(0f, -1f) * Time.deltaTime * speed;
            transform.Translate(newPosition, Space.World);
        }
        else if (moveDirection == MoveDirection.up)
        {
            Vector2 newPosition = new Vector2(0f, 1f) * Time.deltaTime * speed;
            transform.Translate(newPosition, Space.World);
        }
        
    }


    private void checkIfAtYEdge()
    {
        if (Mathf.Abs(transform.position.y) > fishMaxYPosition - 0.5f)
        {
            if (transform.position.y > 0)
            {
                moveDirection = MoveDirection.down;
            }
            else if (transform.position.y < 0)
            {
                moveDirection = MoveDirection.up;
            }
            else
            {
                moveDirection = MoveDirection.stop;
            }
        }

    }


}
