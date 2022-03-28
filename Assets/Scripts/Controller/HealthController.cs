using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HealthController : MonoBehaviour
{
    public int maxHealth = 5;
    public int currentHealth = 5;

    protected Rigidbody2D rb;
    protected LevelController lc;
    public GameObject pfDamagePopup;
    public GameObject dieEffect;

    public bool isAlive = true;
    bool CanHit=true;
    private readonly float _hitTime = 0.1f;
    private float _hitTimer = 0;
    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        rb = GetComponent<Rigidbody2D>();
        lc = FindObjectOfType<LevelController>();
    }

    // Update is called once per frame
    void Update()
    {
        _hitTimer += Time.deltaTime;


        if (_hitTimer > _hitTime)
        {
            CanHit = true;
        }
        else
        {
            CanHit = false;
        }
        if (currentHealth >= maxHealth)
        {
            currentHealth = maxHealth;
        }
        //Die condition
        if (currentHealth <= 0 || lc.spawningBoss == true)
        {
            StartCoroutine(Disapearing());
            Invoke("Die",0.25f);
        }
    }
    public void TakeDamage(int damage)
    {
        if (CanHit == false)
        {
            return;
        }
        else
        {
            currentHealth -= damage;
            if (pfDamagePopup)
            {
                ShowFloatingText(damage);
            }
            _hitTimer = 0;
        }
    }
    public void ShowFloatingText(int NumText)
    {
        GameObject go = Instantiate(pfDamagePopup, transform.position + Vector3.up * 0.5f, Quaternion.identity);
        go.GetComponent<Rigidbody2D>().velocity = rb.velocity;
        go.GetComponent<TextMeshPro>().text = NumText.ToString();
    }
    public virtual void Die()
    {
        dieEffect = Instantiate(dieEffect, transform.position, Quaternion.identity);
        Destroy(dieEffect, 2f);
        Destroy(gameObject);
        isAlive = false;
        GameObject spawner = GameObject.Find("Spawner");
        WaveSpawner NumEnemies = spawner.GetComponent<WaveSpawner>();
        NumEnemies.numEnemies--;
    }
    public virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.CompareTag("HealPowerup"))
        {
            if (currentHealth < maxHealth)
            {
                currentHealth += 2;
            }
        }
    }
    IEnumerator Disapearing()
    {
        gameObject.layer = 16;
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
}
