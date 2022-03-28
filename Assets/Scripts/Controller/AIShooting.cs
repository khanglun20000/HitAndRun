using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIShooting : MonoBehaviour
{
    public Transform firePoint;
    public float timeBwShots;
    public float startTimeBwShots = 1f;
    public int NumBul=0;
    private float SpreadAngel;
    public Transform bulletPrefab;
    private Transform BulletPf;
    // Start is called before the first frame update
    void Start()
    {
        timeBwShots = startTimeBwShots;
    }

    // Update is called once per frame
    public void Shoot(Vector3 offsetRotation)
    {
        if (timeBwShots <= 0)
        {
            BulletPf =Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
            BulletPf.transform.Rotate(offsetRotation);
            timeBwShots = startTimeBwShots;
        }
        else
        {
            timeBwShots -= Time.deltaTime;
        }

    }
    public void RoundShoot(int NumShot)
    {
        if (timeBwShots <= 0)
        {
            for (SpreadAngel = 0; SpreadAngel <= 360; SpreadAngel += 360 / NumShot)
            {
                BulletPf = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
                BulletPf.transform.Rotate(new Vector3(0f, 0f, SpreadAngel));
            }
            timeBwShots = startTimeBwShots;
        }
        else
        {
            timeBwShots -= Time.deltaTime;
        }
    }
    
}
