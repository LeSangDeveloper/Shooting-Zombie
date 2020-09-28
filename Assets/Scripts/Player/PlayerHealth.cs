using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    private int health = 100;
    public Text HealthText; 
    public void SubtractHealth(int dame)
    {
        Debug.Log("test dame");
        health -= dame;
        HealthText.text = health.ToString();
    }

}
