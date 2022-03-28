using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DOT : MonoBehaviour
{
    public int damage;
    public GameObject hiteffect;
    private GameObject effect;
    void Start()
    {
        hiteffect = GameObject.Find("DieEffectZ 1");
        InvokeRepeating("Dps", 0f, 1.5f);
        Destroy(this, 4.6f);
        effect = Instantiate(hiteffect, transform.position, Quaternion.identity, transform);
        Destroy(effect, 4.6f);
    }
    public void Dps()
    {
        gameObject.GetComponent<HealthController>().TakeDamage(damage);
    }
}
