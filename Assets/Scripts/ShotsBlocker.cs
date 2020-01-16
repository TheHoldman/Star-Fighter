using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotsBlocker : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Projectile")
        {
            Destroy(collision.gameObject);
        }
        
    }
}
