using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class countS : ScoreManager
{
    // Update is called once per frame
    void Update()
    {
        gameObject.GetComponent<Text>().text =ab.countS.ToString();
    }
}
