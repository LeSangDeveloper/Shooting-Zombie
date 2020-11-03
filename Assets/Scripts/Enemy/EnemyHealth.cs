using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class EnemyHealth : MonoBehaviour
{
    private float health = 100f;
    public Slider healthSlider;
    public List<GameObject> EnemiesBlood; 
    public void SubtractHealth(int dame)
    {

        health -= dame;
        healthSlider.value = health / 100;

        if (health <= 0)
        {
            healthSlider.gameObject.SetActive(false);
            EnemyMover enemyMover = this.gameObject.GetComponent<EnemyMover>();
            enemyMover.Dead();
            foreach(GameObject temp in EnemiesBlood)
            {
                BoxCollider box = temp.GetComponent<BoxCollider>();
                box.enabled = false;
            }
        }
    }
}
