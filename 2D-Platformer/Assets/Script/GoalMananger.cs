using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GoalMananger : MonoBehaviour
{
    public static GoalMananger singleton; //Mengubah script ini menjadi singgelton dimana fungsi dan variabel dari script ini bisa dipanggil discript lain.

    //Untuk goal condition
    public int holyWaterNeeded;
    public int holyWaterColected = 0 ;
    public bool canEnter; //Goalnya

    //SFX Goal
    public AudioSource GoalSFX;


    //Text Counter Holly Water
    [SerializeField] private Text hollyWaterCounter;


    private void Awake()
    {
        singleton = this;
    }

    public void CollectHollyWater()         //Fungsi agar kita bisa mendapatkan holly water
    {
        holyWaterColected++;
        hollyWaterCounter.text = holyWaterColected +" / "+ holyWaterNeeded;
        if(holyWaterColected >= holyWaterNeeded)
        {
            GoalSFX.Play();
            canEnter = true;
        }

    }
}
