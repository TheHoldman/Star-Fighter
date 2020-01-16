using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Ship : MonoBehaviour
{  

    public Rigidbody2D rb;
    public Joystick joystick;
    Vector2 move;

    public float speed = 3;
    public float shootingRate = 0.3f;
    public float invulTimeLimit = 1f;
    private float shootTimer;
    private float invulTimer;
    private float shipMaxXPosition;
    private float shipMaxYPosition;
    private GameObject shieldClone;

    public GameObject lazerShot;
    public GameObject gunPosition_Idle1;
    public GameObject gunPosition_Idle2;
    public GameObject gunPosition_Up1;
    public GameObject gunPosition_Up2;
    public GameObject gunPosition_Down1;
    public GameObject gunPosition_Down2;
    public GameObject deathAnimation;
    public GameObject shield;
    public Camera mainCamera;

    public GameObject Ship_Idle;
    public GameObject Ship_Up;
    public GameObject Ship_Down;

    public Transform buttonA;

    public bool buttonAPressed = false;

    private void Awake()
    {      

        shipMaxXPosition = mainCamera.ScreenToWorldPoint(new Vector3(Screen.width, 0f, 0f)).x;
        shipMaxYPosition = mainCamera.ScreenToWorldPoint(new Vector3(0f, Screen.height, 0f)).y;        

        Ship_Idle.SetActive(true);
        Ship_Up.SetActive(false);
        Ship_Down.SetActive(false);

        shootTimer = shootingRate;

    }

    private void Update()
    {
        //invul time
        if (invulTimer > 0)
        {
            invulTimer -= Time.deltaTime;
            shieldClone.transform.position = transform.position;
        }
        else if (shield != null)
        {
            Destroy(shieldClone);
        }

        //start shooting timer
        shootTimer += Time.deltaTime;

        //used for movement
        if (joystick.Direction.y > 0)
        {         
            Vector2 newPosition = new Vector2(0f, 1f) * Time.deltaTime * speed;
            transform.Translate(newPosition, Space.World);
            CheckIfOutOfBounds();

            Ship_Idle.SetActive(false);
            Ship_Down.SetActive(false);
            ActivateAndMoveObject(Ship_Up, transform.position);

            ShootGuns(lazerShot, gunPosition_Up1.transform.position, gunPosition_Up2.transform.position);
            MoveSideways();
        }
        else if (joystick.Direction.y < 0)
        {           
            Vector2 newPosition = new Vector2(0f, -1f) * Time.deltaTime * speed;
            transform.Translate(newPosition, Space.World);
            CheckIfOutOfBounds();

            Ship_Up.SetActive(false);
            Ship_Idle.SetActive(false);
            ActivateAndMoveObject(Ship_Down, transform.position);

            ShootGuns(lazerShot, gunPosition_Down1.transform.position, gunPosition_Down2.transform.position);
            MoveSideways();
        }
        else
        {           

            Ship_Up.SetActive(false);
            Ship_Down.SetActive(false);
            ActivateAndMoveObject(Ship_Idle, transform.position);

            ShootGuns(lazerShot, gunPosition_Idle1.transform.position, gunPosition_Idle2.transform.position);
            MoveSideways();
        }
        
        //detects touch of red buttons coords for shooting
        if (Input.touchCount > 0)
        {
            for (int i = 0; i < Input.touchCount; i++)
            {
                Touch touch = Input.GetTouch(i);

                Vector2 touchPosition = Camera.main.ScreenToWorldPoint(touch.position);
                
                if ((touchPosition.x > shipMaxXPosition - 1) && (touchPosition.x < shipMaxXPosition + 0.5) &&
                    (touchPosition.y > -shipMaxYPosition) && (touchPosition.y < -shipMaxYPosition + 1.5))
                {
                    buttonAPressed = true;
                }
            }
        }        
    }

    //detects shooting
    void ShootGuns(GameObject shot, Vector3 gunPosition1,Vector3 gunPosition2)
    {
        if (buttonAPressed)
        {
            if (shootTimer >= shootingRate)
            {
                Instantiate(shot, gunPosition1, new Quaternion());
                Instantiate(shot, gunPosition2, new Quaternion());
                shootTimer = 0;
                buttonAPressed = false;
            }
        }
    }    

    //detects Moving sideways
    void MoveSideways()
    {
        if (joystick.Direction.x > 0)
        {
            Vector2 newPosition = new Vector2(1f, 0f) * Time.deltaTime * speed;
            transform.Translate(newPosition, Space.World);
            CheckIfOutOfBounds();
        }
        if (joystick.Direction.x < 0)
        {
            Vector2 newPosition = new Vector2(-1f, 0f) * Time.deltaTime * speed;
            transform.Translate(newPosition, Space.World);
            CheckIfOutOfBounds();
        }
    }

    void ActivateAndMoveObject(GameObject shipVersion, Vector3 position)
    {
        shipVersion.SetActive(true);
        shipVersion.transform.position = position;
    }
    //blocks going out of screen
    void CheckIfOutOfBounds()
    {
        if (Mathf.Abs(transform.position.x) > shipMaxXPosition-0.5f)
        {
            if (transform.position.x > 0)
            {
                transform.Translate(new Vector3( - 1f, 0) * Time.deltaTime * speed);
            }
            else if (transform.position.x < 0)            
            {
                transform.Translate(new Vector3(1f, 0) * Time.deltaTime * speed);
            }
        }
        else if (Mathf.Abs(transform.position.y) > shipMaxYPosition-0.5f)
        {
            if (transform.position.y > 0)
            {
                transform.Translate(new Vector3(0, -1f) * Time.deltaTime * speed);
            }
            else if(transform.position.y < 0)
            {
                transform.Translate(new Vector3(0,  1f) * Time.deltaTime * speed);
            } 
        }
    }

    public void Damage(int damage)
    {
        if (invulTimer<=0)
        {
            GameManagerScript.lives -= damage;
            if (GameManagerScript.lives <= 0)
            {
                Die();
            }
            else
            {
                invulTimer = invulTimeLimit;
                shieldClone = Instantiate(shield, transform.position, Quaternion.identity);
            }
        }       
    }

    private void Die()
    {
        GameObject cloneDeath =  Instantiate(deathAnimation, transform.position, new Quaternion());
        Destroy(gameObject);
        Destroy(cloneDeath, 2f);
        SceneManager.LoadScene(0);
    }

}
