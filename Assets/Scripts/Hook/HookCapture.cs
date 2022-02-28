using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HookCapture : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Transform ship = collision.gameObject.transform;
            collision.gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            collision.gameObject.transform.position = gameObject.transform.GetChild(0).position;
            collision.gameObject.transform.parent = gameObject.transform;
            ship.localEulerAngles = new Vector3(0, 0, -180);

            collision.GetComponent<O2>().IncreaseO2(100);
            collision.GetComponent<BoxUse>().IncreaseMass(10);
        }
    }

    public void Release()
    {
        GetComponent<BoxCollider2D>().enabled = false;
        Invoke("Activate", 0.5f);    
    }

    private void Activate()
    {
        GetComponent<BoxCollider2D>().enabled = true;
    }
}
