using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyZ : BehaviourController
{
    public GameObject tempDieEffect;
    public Transform AttakSpot;
    public GameObject Corpse;
    public int attackDamage=2;
    public float attackRange;
    public float timeBwAttack=2f;
    private float nextAttack = 0;
    private bool dieeffect=true;
    // Start is called before the first frame update
    public override void Awake()
    {
        base.Awake();
    }

    // Update is called once per frame
    public override void FixedUpdate()
    {
        base.FixedUpdate();
        if (hc.currentHealth <= 0 && canAddScore == true)
        {
            canAddScore = false;
            ab.countZ += 1;
        }
        if (isChasing == true && isFacing == true)
        {
            //chase
            Chase();
            LookPlayer();
            if (Vector2.Distance(AttakSpot.position,playerPos.position)<attackRange&&Time.time>nextAttack)
            {
                Attack();
                nextAttack = Time.time + timeBwAttack;
            }
           
        }
        else if (isFacing == false)
        {
            isPatrolling = true;
        }
        //facing player
        FacePlayer();
        if (hc.currentHealth <= hc.maxHealth/2&& hc.currentHealth>0)
        {
            TemporarDie();
        }
    }

    public float timeTempDie = 4;
    public void TemporarDie()
    {
        if (timeTempDie > 0)
        {
            isChasing = false;
            isFacing = false;
            isPatrolling = false;
            if (dieeffect)
            {
                GameObject TempDieEffect = Instantiate(tempDieEffect, transform.position, Quaternion.identity);
                Destroy(TempDieEffect, timeTempDie);
                dieeffect = false;
            }
            gameObject.GetComponent<SpriteRenderer>().color = new Color32(217, 13, 13, 255);
            gameObject.GetComponent<Collider2D>().enabled = false;
            timeTempDie -= Time.deltaTime;
            Corpse.GetComponent<Collider2D>().enabled = true;

            Corpse.GetComponent<Collider2D>().isTrigger=true;
        }
        else
        {
            gameObject.GetComponent<Collider2D>().enabled = true;
            Corpse.GetComponent<Collider2D>().enabled=false;
            gameObject.GetComponent<SpriteRenderer>().color = Color.white;
        }
    }
    public override void OnDrawGizmos()
    {
        base.OnDrawGizmos();
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(AttakSpot.position, attackRange);
    }
    public void Attack()
    {
        int PlayerMask = LayerMask.GetMask("Player", "Wall");
        Collider2D[] hitPlayer = Physics2D.OverlapCircleAll(AttakSpot.position, attackRange, PlayerMask);
        foreach (Collider2D Player in hitPlayer)
        {
            if (Player.GetComponent<Player>()!=null)
            {
                Player.GetComponent<Player>().TakeDamage(attackDamage);
            }
        }
        
    }
}
