using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float shotSpeed = 50f;
    public Rigidbody2D rb;
    public bool directionRight = true;
    public float timeToDelete = 1.5f;
    public int damage = 1;
    public GameObject hitAnimation;
    public bool isEnemyProjectile = true;
    
    void Start()
    {
        if (directionRight)
        {
            rb.velocity = new Vector2(shotSpeed, 0f);
            Destroy(gameObject, timeToDelete);
        }
        else
        {
            rb.velocity = new Vector2(-shotSpeed, 0f);
            Destroy(gameObject, timeToDelete);
        }        
    }

    private void OnTriggerEnter2D(Collider2D hitInfo)
    {
        if (isEnemyProjectile == false)
        {
            ShootingEnemies enemy = hitInfo.GetComponent<ShootingEnemies>();

            if (enemy != null)
            {
                enemy.Damage(damage);
                AnimateAndDestroyProjectile();
            }
            else if (hitInfo.tag == "Ship" || hitInfo.tag == "Shield" || hitInfo.tag == "Powerup") { }
            else
            {
                AnimateAndDestroyProjectile();
            }            
        }
        else
        {
            if (hitInfo.tag == "Ship")
            {
                hitInfo.GetComponentInParent<Ship>().Damage(damage);

                AnimateAndDestroyProjectile();
            }
            else if (hitInfo.tag == "Enemy" || hitInfo.tag == "Powerup") { }
            else
            {
                AnimateAndDestroyProjectile();
            }
        }
    }

    private void AnimateAndDestroyProjectile()
    {
        GameObject hitAnimationClone = Instantiate(hitAnimation, transform.position, Quaternion.identity);
        Destroy(hitAnimationClone, 0.55f);
        Destroy(gameObject);
    }

}
