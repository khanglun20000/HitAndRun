using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CD1 : MonoBehaviour
{
    private AbilitiesController ab;
    private float S1;

    void Start()
    {
        ab = FindObjectOfType<AbilitiesController>();
        S1 = ab.DashCD;
    }

    // Update is called once per frame
    void Update()
    {
        if (ab.isDashCD == true)
        {
            S1 -= Time.deltaTime;
            if (S1 < 6.5f && S1 > 0)
            {
                gameObject.GetComponent<TextMeshProUGUI>().text = S1.ToString("0");
            }
            else
            {
                gameObject.GetComponent<TextMeshProUGUI>().text = "";
            }
        }
        else
        {
            S1 = ab.DashCD;
            gameObject.GetComponent<TextMeshProUGUI>().text = "";
        }

    }
}
