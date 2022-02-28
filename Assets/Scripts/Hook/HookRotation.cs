using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HookRotation : MonoBehaviour
{
    private float speed;
    private Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        speed = Random.Range(0.5f, 2f);
    }

    private void FixedUpdate()
    {
        transform.Rotate(new Vector3(0, 0, 45) * Time.deltaTime * speed);
    }

    public float GetSpeed()
    {
        return this.speed;
    }
}
