using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyF : BehaviourController
{
    // Start is called before the first frame update
    public Transform bulletPrefab2;
    public Transform bulletPrefab1;

    public Transform firePoint;
    private float timeBwShots;
    public float timeBwBuls=1;
    public float startTimeBwShots = 1f;
 
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
        if (hc.currentHealth <= 0 && canAddScore == true)
        {
            canAddScore = false;
            ab.countF += 1;
        }
        if (isChasing == true && isFacing == true)
        {
            //shoot
            DoubleShoot();
            
            //chase
            Chase();
            LookPlayer();
        }
        else if (isFacing == false)
        {
            isPatrolling = true;
        }
        //facing player
        FacePlayer();
    }
    
    public void DoubleShoot()
    {
        if (timeBwShots+timeBwBuls <= 0)
        {
            float spreadAngle = Random.Range(-30,30) ;
            Vector3 offserRotation = new Vector3(0, 0, spreadAngle);
            Transform BulletPf=Instantiate(bulletPrefab1, firePoint.position, firePoint.rotation);
            BulletPf.transform.Rotate(offserRotation);
            Invoke("Shoot",timeBwBuls);
            timeBwShots = startTimeBwShots;
        }
        else
        {
            timeBwShots -= Time.deltaTime;
        }

    }
    public void Shoot()
    {
        float spreadAngle = Random.Range(-30,30);
        Vector3 offserRotation = new Vector3(0, 0, spreadAngle);
        Transform BulletPf = Instantiate(bulletPrefab2, firePoint.position, firePoint.rotation);
        BulletPf.transform.Rotate(offserRotation);
    }
}
