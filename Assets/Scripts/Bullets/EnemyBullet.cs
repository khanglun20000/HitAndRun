using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    public GameObject hitEffect;
    private GameObject effect;
    protected Player player;
    protected Debuff debuff;
    public GameObject enemy;
    public int Damage;
    public float speed;
    private Rigidbody2D rb;

    
    public virtual void Start()
    {
        player = FindObjectOfType<Player>();
        debuff = FindObjectOfType<Debuff>();
        rb = GetComponent<Rigidbody2D>();
    }
    public virtual void FixedUpdate()
    {
        if (enemy.GetComponent<HealthController>().isAlive == true)
        {
            rb.velocity = enemy.GetComponent<Rigidbody2D>().velocity;
        }
        else
        {
            rb.velocity = rb.velocity;
        }
        
        rb.AddForce(-transform.up * speed, ForceMode2D.Impulse);
        Destroy(gameObject, 10f);
    }

    public virtual void OnTriggerEnter2D(Collider2D collision)
    {
        
        if (collision.gameObject.layer!=10)
        {
            effect = Instantiate(hitEffect, transform.position, transform.rotation);
            Destroy(effect, 1f);
            Destroy(gameObject);
            if (collision.CompareTag("Player"))
            {
                player.TakeDamage(Damage);
            }
            
        }
    }
}
