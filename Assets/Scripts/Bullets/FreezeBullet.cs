using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FreezeBullet : EnemyBullet
{
    public float FreezeTime;
    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();
        debuff.timeFrozen = FreezeTime;
    }

    // Update is called once per frame
    public override void FixedUpdate()
    {
        base.FixedUpdate();
    }
    public override void OnTriggerEnter2D(Collider2D collision)
    { 
        if (collision.CompareTag("Player"))
        {
            int FreezeChane = Random.Range(0, 101);
            if (FreezeChane <= 50)
            {
                debuff.isFrozen = true;
                debuff.isIced = true;
            }
        }
        base.OnTriggerEnter2D(collision);
    }
}
