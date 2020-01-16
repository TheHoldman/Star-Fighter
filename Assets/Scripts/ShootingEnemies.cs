using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingEnemies : MonoBehaviour
{
    public Animator animator; 
    
    public int life = 3;
    public int collisionDamage = 1;
    public int scoreReward = 50;

    public float speed = 2f;
    public float verticalDirectionChangeTimer = 3f;
    public float shootTimer = 0.5f;
    private float ShootTime;
    private float changeDirectionTime;

    public bool startGoingDown = true;

    public GameObject projectile;
    public GameObject gunPosition;
    public GameObject deathAnimation;
    
    bool directionSwitch = false;

    private void Awake()
    {
        ShootTime = Random.Range(0, 0.5f);
    }

    private void Update()
    {
        //start shoot timer
        ShootTime += Time.deltaTime;

        //start direction counter
        changeDirectionTime += Time.deltaTime;

        //counts time before changing the direction
        if (changeDirectionTime >= verticalDirectionChangeTimer)
        {
            if (directionSwitch)
            {
                directionSwitch = false;
            }
            else
            {
                directionSwitch = true;
            }
            
            changeDirectionTime = 0;
        }

        //shoot if timer is up
        if (ShootTime >= shootTimer)
        {
            Instantiate(projectile, gunPosition.transform.position, new Quaternion());
            ShootTime = 0;
        }

        //moves up or down as per switch
        if (startGoingDown)
        {
            if (directionSwitch)
            {
                Move(1);
            }
            else
            {
                Move(-1);
            }
        }
        else
        {
            if (directionSwitch)
            {
                Move(-1);
            }
            else
            {
                Move(1);
            }
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

        if (life == 0)
        {
            GameObject cloneDeath =  Instantiate(deathAnimation, transform.position, new Quaternion());
            Destroy(cloneDeath, 2f);
            Destroy(gameObject);

            GameManagerScript.score += scoreReward;
        }
    }

    private void Move(float upOrDown)
    {
        Vector2 newPosition = new Vector2(-1f, upOrDown * 1f) * Time.deltaTime * speed;
        transform.Translate(newPosition, Space.World);
    }
}
