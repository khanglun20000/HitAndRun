using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootPoint : MonoBehaviour
{
    public float lookDistance = 30f;
    private Transform playerPos;
    private Player player;
    public bool enemySeen;
    public LayerMask whatisenemy;
    private void Start()
    {
        playerPos = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        player = FindObjectOfType<Player>();
    }
    void Update()
    {
        int EnemyMask = LayerMask.GetMask("Wall", "Enemy");
        transform.position = Vector2.MoveTowards(transform.position, (Vector2)playerPos.position + player.movement * 3, 50);
        if (Vector2.Distance(transform.position, playerPos.position) < 5f)
        {
            transform.position = transform.position;
        }
        Collider2D[] hitColliders = Physics2D.OverlapCircleAll(playerPos.position, lookDistance, whatisenemy);
        if (hitColliders.Length == 0)
        {
            enemySeen = false;
        }
        foreach (Collider2D enemy in hitColliders)
        {
            if (enemy != null)
            {
                RaycastHit2D hit2D = Physics2D.Raycast(playerPos.position, enemy.transform.position - playerPos.position, Mathf.Infinity, EnemyMask);
                Debug.DrawRay(playerPos.position, enemy.transform.position - playerPos.position,Color.yellow);
                if (hit2D.transform != null)
                {
                    if (!hit2D.transform.CompareTag("Wall"))
                    {
                        float distanceToClosestEnemy = Mathf.Infinity;
                        HealthController closestEnemy = null;
                        HealthController[] allEnemies = FindObjectsOfType<HealthController>();

                        foreach (HealthController currentEnemy in allEnemies)
                        {
                            float distanceToEnemy = (currentEnemy.transform.position -transform.position).sqrMagnitude;
                            if (distanceToEnemy < distanceToClosestEnemy)
                            {
                                distanceToClosestEnemy = distanceToEnemy;
                                closestEnemy = currentEnemy;
                            }
                        }
                        transform.position = closestEnemy.transform.position+(closestEnemy.transform.position-playerPos.transform.position);
                        enemySeen = true;
                    }
                }
            }
        }
    }
}
