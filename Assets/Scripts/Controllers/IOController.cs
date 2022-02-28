using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IOController : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            if (player != null)
            {
                SpaceShip ship=player.GetComponent<SpaceShip>();
                if (ship.isAttached())
                {
                    player.GetComponent<SpaceShip>().Deattach();
                }
                else
                {
                    BoxUse boxUse = player.GetComponent<BoxUse>();
                    if (boxUse.DoesMassExist())
                    {
                        Vector3 mousePos = Input.mousePosition;
                        mousePos.z = 0;
                        Vector2 pos = Camera.main.ScreenToWorldPoint(mousePos);
                        player.GetComponent<SpaceShip>().ReleaseMass(pos);
                    }
                }
            }
        }
    }
}
