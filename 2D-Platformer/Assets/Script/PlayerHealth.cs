using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{

public int health;
    public GameObject[] healthUI;           //Untuk UI healthnya
    public PlayerControler playerControler; // Mengambil variabel dan fungs script player COntroler

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
            playerControler.KBCounter = playerControler.KBTotalTime;
            if (collision.transform.position.x > transform.position.x)          //Ketika enemy berada di kanan maka menghit dari kanan
            {
                playerControler.KnockFromRight = true;
            }
            if(collision.transform.position.x <= transform.position.x)          // Bila enemy berda di kiri maka hit dari kiri
            {
                playerControler.KnockFromRight = false;
            }
            TakeDemage();
        }
        else if(collision.CompareTag("Spike"))  //Bila bersentuhan dengan obstacle
        {
            TakeDemage();

            transform.position = playerControler.respawnPoint;  //Ketika player terkena spike maka akan berpindah ke checkpoin yang ada

        }
        else if (collision.CompareTag("CheckPoint"))            //Player meneyntuh tag checkpoint maka respawn positionnya berubahan menjadi posisi checkpoint
        {
            playerControler.respawnPoint = transform.position;
        }



    }




}
