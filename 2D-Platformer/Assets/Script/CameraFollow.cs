using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform playerPos;
    public Vector3 offset;
    public float CameraSpeed;



    private void Update()
    {
        transform.position= Vector3.Lerp(transform.position, playerPos.position + offset, CameraSpeed*Time.deltaTime);
    }
}
