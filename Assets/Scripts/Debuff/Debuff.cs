using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Debuff : MonoBehaviour
{
    protected Player player;
    protected AbilitiesController ab;

    public bool isFrozen=false;
    public bool isIced = false;
    public bool isSlowed = false;
    public bool isClocked = false;
    public float timeFrozen;
    public float timeSlowed;

    private GameObject Clocked;
    public GameObject clocked;
    private GameObject Iced;
    public GameObject iced;

    
    void Start()
    {
        ab = FindObjectOfType<AbilitiesController>();
        player = FindObjectOfType<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        if( isFrozen == true)
        {
            StartCoroutine(Frozen());
            if (isIced == true)
            {
                Iced = Instantiate(iced, transform.position, Quaternion.identity);
                isIced = false;
            }
        }
        if (isSlowed == true)
        {
            StartCoroutine(Slowed());
            if (isClocked == true)
            {
                Clocked = Instantiate(clocked, transform.position+new Vector3(0.5f,-0.75f,0), Quaternion.identity);
                isClocked = false;
            }
        }
    }
    public IEnumerator Frozen()
    {
        player.isMoving = false;
        player.isLooking = false;
        ab.canShoot = false;
        ab.canDash = false;
        timeFrozen -= Time.deltaTime;
        Destroy(Iced,timeFrozen);
        yield return new WaitForSeconds(timeFrozen);
        player.isMoving = true;
        player.isLooking = true;
        ab.canDash = true;
        ab.canShoot = true;
        isFrozen = false;
    }
    public IEnumerator Slowed()
    {
        player.speed /= 2f;
        isSlowed = false;
        yield return new WaitForSeconds(timeSlowed);
        player.speed *= 2f;
    }
}
