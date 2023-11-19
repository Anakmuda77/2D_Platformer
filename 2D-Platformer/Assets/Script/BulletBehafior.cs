using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBehafior : MonoBehaviour
{

    public float speed, demage, destroyTime; //Atribut untuk bullet

    // Update is called once per frame

    private void Awake()
    {
        Destroy(gameObject,destroyTime);
    }
    void Update()
    {
        transform.Translate(Vector2.right * speed * Time.deltaTime);
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            collision.transform.parent.GetComponent<EnemyHealth>().TakeDemage(demage);              //Mengambil script enemyhealth dari enemy parent untuk bisa mengurangi darah musuh
            print("Enemy took demange");
            Destroy(gameObject);
        }
        else if (collision.CompareTag("Environtment"))
        {
            Destroy(gameObject);
        }
        else if (collision.CompareTag("Spike"))
        {
            Destroy(gameObject);
        }
    }
}
