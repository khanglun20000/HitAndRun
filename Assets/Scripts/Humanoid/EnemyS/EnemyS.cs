using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyS : BehaviourController
{ 
    // Start is called before the first frame update
    public override void Awake()
    {
        base.Awake();
    }
    
    public float chargeSpeed;
    public int chargeDamage=2;
    public bool isCharging=false;
   
    public float startTimeCharging = 3;
    public float timeCharging;
   
    // Update is called once per frame
    public override void FixedUpdate()
    {
        base.FixedUpdate();
        //Die condition
        if (hc.currentHealth <= 0&&canAddScore==true)
        {
            isCharging = false;
            canAddScore = false;
            ab.countS += 1;
        }
        if (isCharging == true)
        {
            Charge();
        }
        if (isPatrolling == true)
        {
            Rotate();
        }
       
        if (isChasing == true && isFacing == true)
        {
            int maskCharge = LayerMask.GetMask("Player", "Wall");
            Debug.DrawRay(transform.position,transform.up* lookRadius,Color.green);
            RaycastHit2D hitInfo = Physics2D.Raycast(transform.position, transform.up, lookRadius,maskCharge);
            if (hitInfo.transform != null)
            {
                if (hitInfo.transform.CompareTag("Wall") && isCharging == false)
                {
                    Rotate();
                }
                if (hitInfo.transform.CompareTag("Player"))
                {
                    Invoke("Charge",0.5f);
                    isPatrolling = false;
                }
                
               
            }
            if (hitInfo.transform == null)
            {
                isPatrolling = true;
            }
        }

        else if (isFacing == false)
        {
            isPatrolling = true;
            
        }
        //facing player
        FacePlayer();
        
    }
    public void OnCollisionEnter2D(Collision2D collision)
    {
        rb.angularVelocity = 0;
    }
    
    public void Charge()
    {
        gameObject.GetComponent<TrailRenderer>().enabled = true;
        timeCharging += Time.deltaTime;
        isCharging = true;
        if (timeCharging < startTimeCharging)
        {
            rb.AddForce(transform.up * chargeSpeed, ForceMode2D.Impulse);
            isPatrolling = false;
            isCharging = true;
        }
        else
        {
            gameObject.GetComponent<TrailRenderer>().enabled = false;
            timeCharging = 0;
            isCharging = false;
            isPatrolling = true;
        }
    }
    

}