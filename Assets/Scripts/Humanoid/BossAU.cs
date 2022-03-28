using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAU : BehaviourController
{
    // Start is called before the first frame update
    public override void Awake()
    {
        base.Awake();
    }

    // Update is called once per frame
    public override void FixedUpdate()
    {
        base.FixedUpdate();
        AIshooting.RoundShoot(AIshooting.NumBul);
        if (isPatrolling == true)
        {
            Rotate();
        }
    }
}
