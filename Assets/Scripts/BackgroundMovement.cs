using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundMovement : MonoBehaviour
{
    private MeshRenderer meshRenderer;
    private float offsetY;

    private GameObject player;
    private float nextYPosLimit = -2f;

    void Start()
    {
        meshRenderer = gameObject.GetComponent<MeshRenderer>();
        player = GameObject.FindGameObjectWithTag("Player");
    }

    private void Update()
    {
        MoveSceneAndCamera();
    }

    public void MoveTheScene()
    {
        float y = 0.1f*Time.deltaTime;
        offsetY = meshRenderer.material.GetTextureOffset("_MainTex").y;
        meshRenderer.material.SetTextureOffset("_MainTex", new Vector2(0, offsetY + y));
    }

    float smoothTime = 1f;
    float yVelocity = 0.0f;
    private void MoveSceneAndCamera()
    {
        if (player)
        {
            float yPos = player.transform.position.y;
            if (yPos >= nextYPosLimit && !player.GetComponent<SpaceShip>().isAttached())
            {
                float inc = yPos - nextYPosLimit;
                transform.position = new Vector2(transform.position.x, transform.position.y + inc);
                nextYPosLimit = yPos;
                MoveTheScene();
            }
            if (player.GetComponent<SpaceShip>().isAttached())
            {
                nextYPosLimit = yPos;
                if (player.transform.position.y > transform.position.y)
                {
                    float newPosition = Mathf.SmoothDamp(transform.position.y, player.transform.position.y, ref yVelocity, smoothTime);
                    transform.position = new Vector3(transform.position.x, newPosition, transform.position.z);
                }
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            SpaceShip ship = collision.GetComponent<SpaceShip>();
            if (!ship.isAttached())
            {
                collision.gameObject.SetActive(false);
            }
        }
    }
}
