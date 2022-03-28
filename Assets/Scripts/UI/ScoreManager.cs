using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class ScoreManager : MonoBehaviour
{
    protected AbilitiesController ab;
    void Start()
    {
        ab = FindObjectOfType<AbilitiesController>();

    }
}