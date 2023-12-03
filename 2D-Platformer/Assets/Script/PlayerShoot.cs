using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
   public GameObject bullet;    // Object spawan
   public Transform shootPoint; //Lokasi spawn

    //Sound Efect Peluru
    public AudioSource ShotSFX;


    public void Update()
    {
        Shoot();
    }

    void Shoot()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            ShotSFX.Play();
            Instantiate(bullet, shootPoint.position,transform.rotation);        //Melakukan spawn peluru
        }
    }
  
}
