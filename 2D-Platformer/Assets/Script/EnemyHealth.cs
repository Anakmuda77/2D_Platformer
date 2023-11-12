using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealth : MonoBehaviour
{
    // Start is called before the first frame update
    public float health;
    public Image healthUI;  //Untuk UI healthbar
    private float maxHealth;

    private void Start()
    {
        maxHealth = health;
    }

    public void TakeDemage(float demange)           //Fungsi untuk mengurangi darah enemy
    {   
        health -=  demange;
        healthUI.fillAmount = health/maxHealth;
        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }

}
