using UnityEngine;

public abstract class BulletController : MonoBehaviour
{
    public GameObject hitEffect;
    private GameObject effect;
    public int BulletDamage = 50;
    public float speed;
    private Rigidbody2D rb;

    private AbilitiesController ab;
    private bool m_onetime=true;
    public int timesBounce=2;
    protected virtual void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        ab = FindObjectOfType<AbilitiesController>();
        if (ab.isCritical == true)
        {
            gameObject.GetComponent<TrailRenderer>().enabled = true;
        }
    }
    protected virtual void FixedUpdate()
    {
        if (ab.BounceBullet == true)
        {
            gameObject.GetComponent<CircleCollider2D>().isTrigger = false;
        }
        if (m_onetime)
        {
            rb.AddForce(transform.up * speed * Time.deltaTime, ForceMode2D.Impulse);
            m_onetime = false;
        }
    }

    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        effect = Instantiate(hitEffect, transform.position, transform.rotation);
        Destroy(effect, 1f);
        Destroy(gameObject);
        if (collision.gameObject.layer==10)
        {
            if (collision.gameObject.GetComponent<HealthController>() != null)
            {
                if (ab.isCritical == false)
                {
                    collision.gameObject.GetComponent<HealthController>().TakeDamage(BulletDamage);
                }
                else
                {
                    collision.gameObject.GetComponent<HealthController>().TakeDamage(BulletDamage*2);
                }
            }
        }
    }
    protected virtual void OnCollisionEnter2D(Collision2D collision)
    {
        effect = Instantiate(hitEffect, transform.position, transform.rotation);
        Destroy(effect, 1f);
        if (timesBounce > 0)
        {
            if (collision.transform.CompareTag("Wall"))
            {
                timesBounce--;
            }
            else
            {
                Destroy(gameObject);
                if (collision.gameObject.layer == 10)
                {
                    if (collision.gameObject.GetComponent<HealthController>() != null)
                        collision.gameObject.GetComponent<HealthController>().TakeDamage(BulletDamage);
                }
            }
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
