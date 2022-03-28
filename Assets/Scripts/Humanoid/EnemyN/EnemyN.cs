using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyN : BehaviourController
{
    // Start is called before the first frame update

    public override void Awake()
    {
        base.Awake();
    }
    public void Start()
    {
        waitTime = startWaitTime;
       
    }
    
    // Update is called once per frame
    public override void FixedUpdate()
    {
        base.FixedUpdate();
        if (hc.currentHealth <= 0&&canAddScore==true)
        {
            canAddScore = false;
            ab.countN += 1;
        }
        if (isChasing == true && isFacing == true)
        {
            //shoot
            AIshooting.Shoot(new Vector3(0f, 0f, 0f));
            //chase
            Chase();
            LookPlayer();
        }
        else if(isFacing==false)
        {
            isPatrolling = true;
        }
        //facing player
        FacePlayer();
    }
    
}