using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalMananger : MonoBehaviour
{
    public static GoalMananger singleton; //Mengubah script ini menjadi singgelton dimana fungsi dan variabel dari script ini bisa dipanggil discript lain.

    //Untuk goal condition
    public int holyWaterNeeded;
    public int holyWaterColected;
    public bool canEnter; //Goalnya



    private void Awake()
    {
        singleton = this;
    }

    public void CollectHollyWater()         //Fungsi agar kita bisa mendapatkan holly water
    {
        holyWaterColected++;
        if(holyWaterColected >= holyWaterNeeded)
        {
            canEnter = true;
        }

    }
}
