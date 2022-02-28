using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour
{
    private float speed = 20;

    private void Start()
    {
        float xForce = Random.Range(-1, 1);
        float yForce = Random.Range(-10, -1);
        Vector2 pos = new Vector2(xForce, yForce);
        GetComponent<Rigidbody2D>().AddForce(pos * speed);
    }
    void FixedUpdate()
    {
        transform.Rotate(new Vector3(0, 0, 45) * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (!collision.GetComponent<SpaceShip>().isAttached())
            {
                collision.gameObject.SetActive(false);
                Destroy(gameObject);

            }
        }
    }
}
