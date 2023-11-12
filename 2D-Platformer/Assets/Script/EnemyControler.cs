using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyControler : MonoBehaviour
{

    //Movement
    public float movementSpedd;
    public bool isFacingRight;


    //Checker
    public Transform groundCheker;
    public float radius;
    public LayerMask whatIsGround;

    //HealtBar Ui
    public Transform HealtBarHUD;


    // Update is called once per frame                                             //Menggerakkan enemy secara otomatis
    void Update()
    {
        transform.Translate(Vector2.right * movementSpedd * Time.deltaTime);
        if (!ThereIsGround())
        {
            if (isFacingRight)
            {
                HealtBarHUD.localEulerAngles = Vector2.up * 180;
                transform.eulerAngles = Vector2.up * 180;
                isFacingRight = false;
            }
            else
            {
                HealtBarHUD.localEulerAngles = Vector2.zero;
                transform.eulerAngles = Vector2.zero;
                isFacingRight = true;

            }

        }
        
    }

    bool ThereIsGround()
    {
        return Physics2D.OverlapCircle(groundCheker.position, radius,whatIsGround);             //Mengecek apakah enemy tetap berada di tanah
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(groundCheker.position, radius);
    }



}
