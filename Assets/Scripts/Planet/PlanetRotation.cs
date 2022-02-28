using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetRotation : MonoBehaviour
{
    private float speed;

    private void Start()
    {
        speed = Random.Range(0.5f, 2f);
    }

    void Update()
    {
        transform.Rotate(new Vector3(0, 0, 45) * Time.deltaTime*speed);
    }
}
