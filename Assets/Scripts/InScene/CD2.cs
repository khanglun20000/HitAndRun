using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CD2 : MonoBehaviour
{
    private AbilitiesController ab;
    private float S2;
   
    void Start()
    {
        ab = FindObjectOfType<AbilitiesController>();
        S2 = ab.CurseCD;
    }

    // Update is called once per frame
    void Update()
    {
        if (ab.isCurseCD == true)
        {
            S2 -= Time.deltaTime;
            if (S2 < 6.5f && S2 > 0.3f)
            { 
                gameObject.GetComponent<TextMeshProUGUI>().text = S2.ToString("0");
            }
            else
            {
                gameObject.GetComponent<TextMeshProUGUI>().text = "";
            }
        }
        else
        {
            S2 = ab.CurseCD;
            gameObject.GetComponent<TextMeshProUGUI>().text = "";
        }
       
    }
}
