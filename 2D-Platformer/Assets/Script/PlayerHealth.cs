using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{

public int health;
    public GameObject[] healthUI;           //Untuk UI healthnya
    void TakeDemage()
    {
        health--;

        if (health <= 0)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);   //Ketika health sudah habis maka scene akan direstart
        }
        healthUI[health].SetActive(false);
    }


    private void OnTriggerEnter2D(Collider2D collision)         //Ketika bersentuhan dengan colider yang bertipe tag "enemy" maka helat akan berkurang
    {

        if (collision.CompareTag("Enemy"))
        {
            TakeDemage();
        }
    }

}
