using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookPoint : MonoBehaviour
{
    private Transform playerPos;
    private Player player;
    private void Start()
    {
        playerPos = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        player = FindObjectOfType<Player>();
    }
    void Update()
    {
       
            transform.position = Vector2.MoveTowards(transform.position, (Vector2)playerPos.position + player.movement * 3, 50);
        
    }
}
