using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AbilitiesController :MonoBehaviour
{
    public int countS;
    public int countN;
    public int countZ;
    public int countF;

    public float DashTime=0.5f;
    public float DashCD = 4f;
    public float DashSpeed=5;
    public bool canDash = false;
    public bool isDashCD = false;
    private bool onClickShoot;
    public bool onClickSkill1;

    private int Skill1;
    private int Skill2;

    private Player player;
    private Rigidbody2D playerBod;
    private Transform playerPos;
    private GameObject dp;
    private ShootPoint sp;
    private GameObject slay;

    public Transform firePoint;
    public float SpreadAngel;
    public float timeBwShot = 0.5f;
    private float nextShot = 0;

    public bool canShoot = true;
    public bool stopMove = false;
    public bool canLearnSkill = true;
    public bool ThreeBullet = false;
    public bool DoubleShot = false;
    public bool canCrit = false;
    public bool BounceBullet = false;
    public bool isShooting=false;

    public bool canCurse=false;
    public bool isCurseCD = false;
    public LayerMask whatIsEnemy;
    public bool onClickSkill2;
    public float curseRadius = 10;
    public float CurseCD=7f;
    public float nextCurse=0;
    private GameObject eff;
    public GameObject CurseEffect;

    public bool isCritical;
    private readonly List<int> list = new List<int>();   //  Declare list
 
 
    public Transform bulletPrefab;
    private void Start()
    {
        sp = FindObjectOfType<ShootPoint>();
        player = FindObjectOfType<Player>();
        playerBod= GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody2D>();
        playerPos = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        slay = FindObjectOfType<Slay>().gameObject;
        slay.SetActive(false);

        for (int n = 0; n < 4; n++)    //  Populate list
        {
            list.Add(n);
        }

       
    }
    void Update()
    {
        // Dash condition
        if (countS >= 10)
        {
            canDash = true;
        }
        if (onClickSkill1 == true && isDashCD == false && canDash == true)
        {
            Skill1 = 1;
        }
        else
        {
            Skill1 = 0;
        }
        if (Skill1 == 1 && isDashCD == false && canDash == true&&sp.enemySeen==true)
        {
            player.isMoving = false;
            StartCoroutine(Dash());
        }

        #region Shoot
        //Shoot condition
        if (countN % 5 == 0 && countN >= 5 && canLearnSkill == true)
        {
            canLearnSkill = false;
            LearnRandom();
        }
        else if (countN % 5 != 0)
        {
            canLearnSkill = true;
        }

        if (onClickShoot == true && Time.time > nextShot)
        {
            nextShot = Time.time + timeBwShot;
            StartCoroutine(Shoot(new Vector3(0f, 0f, 0f)));
            if (ThreeBullet == true)
            {
                StartCoroutine(Shoot(new Vector3(0f, 0f, SpreadAngel)));
                StartCoroutine(Shoot(new Vector3(0f, 0f, -SpreadAngel)));
            }
        }
        #endregion
        if (countZ >= 10)
        {
            canCurse = true;
        }
        if (onClickSkill2==true && isCurseCD == false && canCurse == true)
        {
            Skill2 = 1;
        }
        if (Skill2 == 1 && isCurseCD == false && canCurse == true)
        {
            StartCoroutine(Curse());
        }
        if (Skill2 == 1 && nextCurse < Time.time)
        {
            eff = Instantiate(CurseEffect, transform.position, Quaternion.identity, transform);
            Destroy(eff, 3);
            nextCurse = Time.time + CurseCD;
        }
    }
    IEnumerator Curse()
    {
        Collider2D[] hitColliders = Physics2D.OverlapCircleAll(playerPos.position, curseRadius, whatIsEnemy);
        for (int i = 0; i < hitColliders.Length; i++)
        {
            if (hitColliders[i].GetComponent<DOT>() == null)
            {
                hitColliders[i].gameObject.AddComponent<DOT>();
                hitColliders[i].gameObject.GetComponent<DOT>().damage=1;
            }
        }
        yield return new WaitForSeconds(3);
        //stop calling coroutine
        Skill2 = 0;
        //cooling down
        isCurseCD = true;
        yield return new WaitForSeconds(CurseCD);
        isCurseCD = false;
    }
    IEnumerator Dash()
    {
        player.enabled = false;
        slay.SetActive(true);
        player.isLooking = false;
        playerPos.position+=transform.up*Time.deltaTime*DashTime*DashSpeed;
        player.isInvulnerable = true;
        canShoot = false;
        gameObject.GetComponent<Collider2D>().enabled = false;
        yield return new WaitForSeconds(DashTime);
        gameObject.GetComponent<Collider2D>().enabled = true ;
        slay.SetActive(false);
        player.isMoving = true;
        player.isLooking = true;
        player.enabled = true;
        player.isInvulnerable = false;
        isDashCD = true;
        canShoot = true;
        Skill1 = 0;
        yield return new WaitForSeconds(DashCD);
        isDashCD = false;
    }
    IEnumerator Shoot(Vector3 offsetRotation)
    {
        if (canShoot == true)
        {
            if (canCrit == true)
            {
                int critChance = Random.Range(0, 101);
                if (critChance < 30)
                {
                    isCritical = true;
                }
                else {isCritical = false; }
            }
            Transform BulletPrefab = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
            BulletPrefab.transform.Rotate(offsetRotation);
            if (DoubleShot == true)
            {
                isShooting = true;
                yield return new WaitForSeconds(0.1f);
                BulletPrefab = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
                BulletPrefab.transform.Rotate(offsetRotation);
                yield return new WaitForSeconds(0.05f);
                isShooting = false;
            }
        }
    }
    public void LearnRandom()
    {
        if (list.Count != 0)
        {
            int index = Random.Range(0, list.Count);    //  Pick random element from the list
            int i = list[index];    //  i = the number that was randomly picked
            Debug.Log(i);
            list.RemoveAt(index);   //  Remove chosen element

            switch (i)
            {
                case 0:
                    DoubleShot = true;
                    break;
                case 1:
                    ThreeBullet = true;
                    break;
                case 2:
                    BounceBullet = true;
                    break;
                case 3:
                    canCrit = true;
                    break;
            }
        }
    }
    #region Button
    public void PointerUp()
    {
        onClickShoot = false;
    }
    public void PointerDown()
    {
        onClickShoot = true;
        stopMove = true;
    }
    public void PointerUp1()
    {
        onClickSkill1 = false;
    }
    public void PointerDown1()
    {
        onClickSkill1 = true;
    }
    public void PointerUp2()
    {
        onClickSkill2 = false;
    }
    public void PointerDown2()
    {
        onClickSkill2 = true;
    }
    #endregion
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.grey;
        Gizmos.DrawWireSphere(transform.position, curseRadius);
    }
}