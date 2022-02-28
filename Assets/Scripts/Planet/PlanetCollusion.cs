using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetCollusion : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            bool isAttached = collision.gameObject.GetComponent<SpaceShip>().isAttached();
            if (!isAttached)
            {
                collision.gameObject.SetActive(false);
            }
        }
    }
}
