using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public abstract class BehaviourController : MonoBehaviour
{
    // Start is called before the first frame update
    protected Rigidbody2D rb;
    protected LevelController lc;
    protected Collider2D E_collider;
    protected AbilitiesController ab;
    protected BulletController bc;
  
    protected Rigidbody2D player;
    protected Transform playerPos;
    public Transform moveSpot;
    protected Vector2 dirPlayer;
    public Animator animator;
    protected HealthController hc;
    protected AIShooting AIshooting;

    public float speed=4;
    public float lookSpeed = 50;
    public float patrolSpeed;
    public float minX = -20;
    public float maxX = 20;
    public float minY = -30;
    public float maxY = 30;

    public int lookRadius = 30;
    public float stoppingDistance;
    public float slowDowndistance=2;
    public float retreatDistance=1;

    protected float waitTime;
    public float startWaitTime=2;

    
    
    public bool isChasing = false;
    public bool isFacing = false;
    public bool isPatrolling = true;
    public bool canAddScore = true;

    

    public virtual void Awake()
    {
        lc = FindObjectOfType<LevelController>();
        AIshooting = GetComponent<AIShooting>();
        hc = FindObjectOfType<HealthController>();
        bc = FindObjectOfType<BulletController>();
        rb = GetComponent<Rigidbody2D>();
        E_collider = GetComponent<Collider2D>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody2D>();
        playerPos = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        ab = FindObjectOfType<AbilitiesController>();
        dirPlayer = new Vector2(playerPos.position.x - transform.position.x, playerPos.position.y - transform.position.y);
       
        waitTime = startWaitTime;

        moveSpot.position = new Vector2(Random.Range(minX, maxX), Random.Range(minY, maxY));
    }

    // Update is called once per frame
    public virtual void FixedUpdate()
    {
        if (hc.currentHealth <= 0||lc.spawningBoss==true)
        {
            isPatrolling = false;
            isChasing = false;
            isFacing = false;
        }
        //patrol condition
        if (isPatrolling == true)
        {
            Patrol();
        }

        //Chase condition
        if (Vector2.Distance(transform.position, player.transform.position) <= lookRadius)
        {
            isChasing = true;
        }
        else
        {
            isChasing = false;
            isFacing = false;
        }
    }

    public virtual void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, lookRadius);
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, stoppingDistance);
        Gizmos.color = Color.white;
        Gizmos.DrawWireSphere(transform.position, retreatDistance);
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, slowDowndistance);

    }

    public void Rotate()
    {
        transform.Rotate(Vector3.forward * lookSpeed * Time.deltaTime);
    }

    #region movementbehavour
    protected void Patrol()
    {
        transform.position = Vector2.MoveTowards(transform.position, moveSpot.position, patrolSpeed * Time.deltaTime);
        if (waitTime <= 0)
        {
            moveSpot.position = new Vector2(Random.Range(minX, maxX), Random.Range(minY, maxY));
            waitTime = startWaitTime;
        }
        else
        {
            waitTime -= Time.deltaTime;
        }
    }
    protected void FacePlayer()
    {
        int PlayerMask = LayerMask.GetMask("Player", "Wall");
        
        RaycastHit2D hit2D = Physics2D.Raycast(transform.position, playerPos.transform.position - transform.position, Mathf.Infinity, PlayerMask);
        if (hit2D.transform != null)
        {
            if (hit2D.transform.CompareTag("Player"))
            {
                isFacing = true;

            }
            else
            {
                isFacing = false;

            }
        }
        if (hit2D.transform == null)
        {
            isPatrolling = true;
        }
    }
    public void Chase()
    {
        isPatrolling = false;
        if (Vector2.Distance(transform.position, player.transform.position) > slowDowndistance)
        {
            transform.Translate(Vector2.up * speed * Time.deltaTime);
        }
        else if (Vector2.Distance(transform.position, player.transform.position) <=slowDowndistance&& (Vector2.Distance(transform.position, player.transform.position) > retreatDistance)&&(Vector2.Distance(transform.position, player.transform.position) > stoppingDistance))
        {
            transform.Translate(Vector2.up * speed/1.5f * Time.deltaTime);
        }
        else if (Vector2.Distance(transform.position, player.transform.position) <= stoppingDistance && (Vector2.Distance(transform.position, player.transform.position) > retreatDistance))
        {
            rb.velocity=Vector3.zero;
        }
        else if (Vector2.Distance(transform.position, player.transform.position) < retreatDistance)
        {
            transform.position = Vector2.MoveTowards(transform.position, playerPos.position, -speed * Time.deltaTime);
        }
    }
    public void LookPlayer()
    {
        Vector2 direction = (playerPos.position - transform.position).normalized;
        transform.up = direction;
    }
    #endregion
    
}       