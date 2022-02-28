using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxBehavior : MonoBehaviour
{
    public void DestroyBox(float delay)
    {
        Destroy(gameObject, delay);
    }
}
