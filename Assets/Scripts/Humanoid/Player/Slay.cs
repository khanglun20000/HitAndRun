using UnityEngine;

public class Slay : MonoBehaviour
{
    public GameObject hitEffect;
    private GameObject effect;
    public GameObject sp;
    
    public int SlayDamage = 5;
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        effect = Instantiate(hitEffect, transform.position, transform.rotation);
        Destroy(effect, 1f);
        if (collision.gameObject.layer == 10)
        {
            if (collision.gameObject.GetComponent<HealthController>() != null)
                collision.gameObject.GetComponent<HealthController>().TakeDamage(SlayDamage);
        }
    }
}
