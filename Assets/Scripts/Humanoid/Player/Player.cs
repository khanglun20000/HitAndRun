using System.Collections;
using TMPro;
using UnityEngine;

public class Player : MonoBehaviour
{
    public GameObject pfDamagePopup;
    public GameObject pfHealPopup;
    private Rigidbody2D rb;
    private AbilitiesController ab;
    public bool isLooking = true;
    public bool isMoving = true;
    public GameObject lookPoint;
    public GameObject shootPoint;


    [HideInInspector]
    public Vector2 movement;
    public Vector2 movement1;
    public float rotationSpeed;
    public float speed;


    public float lookDistance;


    public float maxHealth = 7f;
    public float health;

    public float hurtTime=0.5f;
    public float startHurtTime = 0.5f;

    public bool isInvulnerable = false;

    void Start()
    {
        ab = FindObjectOfType<AbilitiesController>();
        rb = GetComponent<Rigidbody2D>();
        health = maxHealth;
    }
    

    // Update is called once per frame
    private void FixedUpdate()
    {
        if (health >= maxHealth)
        {
            health = maxHealth;
        }
        if (isLooking == true)
        {
            if (ab.isShooting == false)
            {
                Vector2 dir = (-shootPoint.transform.position + transform.position).normalized;
                float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg+90f;
                transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.AngleAxis(angle, Vector3.forward), Time.deltaTime * rotationSpeed);
            }
        }
        movement.x = SimpleInput.GetAxis("Horizontal");
        movement.y = SimpleInput.GetAxis("Vertical");
        movement1.x = Input.GetAxis("Horizontal");
        movement1.y = Input.GetAxis("Vertical");
        if (isMoving == true)
        {
            moveCharacter();
        }
    }
    public void TakeDamage(int damage)
    {
        if (!isInvulnerable)
        {
            StartCoroutine(Blink());
            health -= damage;
            if (pfDamagePopup)
            {
                ShowFloatingDamage(damage);
            }
        }
    }
    IEnumerator Blink()
    {
        gameObject.GetComponent<SpriteRenderer>().color = new Color32(0, 255, 255, 255);
        yield return new WaitForSeconds(0.05f);
        gameObject.GetComponent<SpriteRenderer>().color = Color.white;
        yield return new WaitForSeconds(0.05f);
        gameObject.GetComponent<SpriteRenderer>().color = new Color32(0, 255, 255, 255);
        yield return new WaitForSeconds(0.05f);
        gameObject.GetComponent<SpriteRenderer>().color = Color.white;
        yield return new WaitForSeconds(0.05f);
        gameObject.GetComponent<SpriteRenderer>().color = new Color32(0, 255, 255, 255);
        yield return new WaitForSeconds(0.05f);
        gameObject.GetComponent<SpriteRenderer>().color = Color.white;
    }
    public void ShowFloatingDamage(int Damage)
    {
        GameObject go=Instantiate(pfDamagePopup, transform.position+Vector3.up*0.5f, Quaternion.identity);
        go.GetComponent<Rigidbody2D>().velocity = rb.velocity;
        go.GetComponent<TextMeshPro>().text = Damage.ToString();
    }
    public void Heal(int heal)
    {
        if (health <= maxHealth)
        {
            health += heal;
            if (pfHealPopup)
            {
                ShowFloatingHeal(heal);
            }

        }
    }
    public void ShowFloatingHeal(int heal)
    {
        GameObject go = Instantiate(pfHealPopup, transform.position + Vector3.up * 0.5f, Quaternion.identity);
        go.GetComponent<Rigidbody2D>().velocity = rb.velocity;
        go.GetComponent<TextMeshPro>().text = heal.ToString();
    }
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.CompareTag("HealPowerup"))
        {
            Heal(10);
        }
    }
    void moveCharacter()
    {
        rb.MovePosition(rb.position + movement * speed * Time.deltaTime);
        rb.MovePosition(rb.position + movement1 * speed * Time.deltaTime);
    }
}

