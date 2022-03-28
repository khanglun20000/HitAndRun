using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class countN : ScoreManager
{
    // Update is called once per frame
    void Update()
    {
        gameObject.GetComponent<Text>().text = ab.countN.ToString();
    }
}
