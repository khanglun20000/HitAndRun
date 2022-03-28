using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Clock : MonoBehaviour
{
    private Transform playerPos;
    private Debuff debuff;
    private void Start()
    {
        debuff = FindObjectOfType<Debuff>();
        playerPos = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        Destroy(gameObject, debuff.timeSlowed);
    }
    void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, playerPos.position + new Vector3(0.5f, -0.75f, 0), 50);

    }
}
