using UnityEngine;

public class Shield : MonoBehaviour
{
    public GameObject hitEffect;
    public GameObject enemyS;
    private GameObject HitEffect;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("PlayerBullet"))
        {
            HitEffect = Instantiate(hitEffect, transform.position, transform.rotation);
            Destroy(HitEffect, 1f);
        }
        if (collision.gameObject.CompareTag("Player"))
        {
            HitEffect = Instantiate(hitEffect, transform.position, transform.rotation);
            Destroy(HitEffect, 1f);
            Player player = FindObjectOfType<Player>();
            player.TakeDamage(2);
        }
    }
}
