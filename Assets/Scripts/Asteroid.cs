using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour
{
    public float speed;    
    public float rotation;
    public float yDirection;
    public float xDirection;
    public Rigidbody2D rb;

    private void Awake()
    {              
        rb.AddTorque(rotation);
    }

    private void Update()
    {
        Vector2 newPosition = new Vector2(xDirection, yDirection) * Time.deltaTime * speed;
        transform.Translate(newPosition, Space.World);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Ship")
        {
            collision.GetComponentInParent<Ship>().Damage(3);
        }
    }

}
