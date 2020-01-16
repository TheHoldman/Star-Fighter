using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpScript : MonoBehaviour
{   
    public int scoreReward = 100;

    public float speed = 2f;
    public float verticalDirectionChangeTimer = 2f;
    public float shootTimer = 0.5f;
    private float ShootTime;
    private float changeDirectionTime;

    public bool startGoingDown = true;

    bool directionSwitch = false;
    
    private void Update()
    {
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
            Destroy(gameObject);
            GameManagerScript.score += scoreReward;
            GameManagerScript.lives += 2;
        }
    }    

    private void Move(float upOrDown)
    {
        Vector2 newPosition = new Vector2(-1f, upOrDown * 1f) * Time.deltaTime * speed;
        transform.Translate(newPosition, Space.World);
    }
}
