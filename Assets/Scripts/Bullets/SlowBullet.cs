using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlowBullet : EnemyBullet
{
    public float SlowTime;
    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();
        debuff.timeSlowed = SlowTime;
    }

    // Update is called once per frame
    public override void FixedUpdate()
    {
        base.FixedUpdate();
    }
    public override void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && debuff.isSlowed == false)
        {
            debuff.isSlowed = true;
            debuff.isClocked = true;
        }
        base.OnTriggerEnter2D(collision);
    }
}
