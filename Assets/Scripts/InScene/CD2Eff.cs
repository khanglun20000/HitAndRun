using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class CD2Eff : MonoBehaviour
{
    [Header("Abitity1")]
    public Image AbiImg1;
    public bool isCD1=false;
    private float S1;

    [Header("Abitity2")]
    public Image AbiImg2;
    public bool isCD2 = false;
    private float S2;

    private AbilitiesController ab;
    // Start is called before the first frame update
    void Start()
    {
        ab = FindObjectOfType<AbilitiesController>();

        AbiImg1.fillAmount = 1;
        S1 = ab.DashCD;

        AbiImg2.fillAmount = 1;
        S2 = ab.CurseCD;
    }

    // Update is called once per frame
    void Update()
    {
        if (ab.isDashCD == true&&isCD1==false)
        {
            isCD1 = true;
            AbiImg1.fillAmount = 0;
        }

        if(isCD1)
        {
            AbiImg1.fillAmount += 1 / S1 * Time.deltaTime;
            if (AbiImg1.fillAmount >= 1)
            {
                AbiImg1.fillAmount = 1;
                isCD1 = false;
            }
            
        }

        if (ab.isCurseCD == true&&isCD2==false)
        {
            isCD2 = true;
            AbiImg2.fillAmount = 0;
        }

        if(isCD2)
        {
            AbiImg2.fillAmount += 1 / S2 * Time.deltaTime;
            if (AbiImg2.fillAmount >= 1)
            {
                AbiImg2.fillAmount = 1;
                isCD2 = false;
            }
            
        }
    }
}
