using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectedBox : MonoBehaviour
{
    private const float speed=0.5f;

    private void FixedUpdate()
    {
        transform.Rotate(new Vector3(0, 0, 45) * Time.deltaTime * speed);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<BoxUse>().IncreaseMass(10);
            Destroy(gameObject);
        }
    }
}
