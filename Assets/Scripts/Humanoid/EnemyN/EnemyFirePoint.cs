using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFirePoint : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject pivot;
    private GameObject player;
    public float lookSpeed=50;
    void Start()
    {
        player = FindObjectOfType<Player>().gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 direction = player.transform.position - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg+90f;
        Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, rotation, Time.deltaTime * lookSpeed);
    }
}
