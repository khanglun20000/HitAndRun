using UnityEngine;
using System.Collections;


public class PowerUpController : MonoBehaviour
{ 
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.transform.CompareTag("PlayerBullet") && !collision.transform.CompareTag("EnemyBullet"))
        {
            Destroy(gameObject);
        }
    }
}
    