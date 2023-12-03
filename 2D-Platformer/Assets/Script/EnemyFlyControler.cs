using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFlyControler : MonoBehaviour
{
    public float speed;
    public Transform player;
    public float lineOfSite;
    private Vector2 originalPosition;


    //HealtBar Ui
    public Transform HealtBarHUD;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;      //Mengambil posisi player
        originalPosition = transform.position; // Store the original position
 

    }

    // Update is called once per frame
    void Update()
    {
        float distanceFromPlayer = Vector2.Distance(player.position,transform.position);  //Menghitung jarak player dan musuh;

        if(transform.position.x < player.position.x)
        {
            transform.eulerAngles = Vector2.up * 180;
            HealtBarHUD.localEulerAngles = Vector2.up * 180;
        }

        else
        {
            transform.eulerAngles = Vector2.zero;
            HealtBarHUD.localEulerAngles = Vector2.zero;

        }

        if (distanceFromPlayer < lineOfSite)                                               //Ketika jarak atar player dan musuh lebih kecil maka musuh akan bergerak
        {
            transform.position = Vector2.MoveTowards(this.transform.position, player.position, speed * Time.deltaTime);  //Menggerakkan object menuju posisi player.

        }
        else
        {
            transform.position = Vector2.MoveTowards(transform.position, originalPosition, speed * Time.deltaTime);
       



        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, lineOfSite); //Menggambar ukuran line offsite
    }
}
