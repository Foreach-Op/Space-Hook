using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceShip : MonoBehaviour
{
    Rigidbody2D rb;
    public float speed;
    Vector2 reverseForce = new Vector2(25,25);
    private int score = 0;
    private float locChange = 0.5f;
    private float recordedYPos = 0;
    private UIController uIController;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        recordedYPos = transform.position.y;

        GameObject UI = GameObject.FindGameObjectWithTag("UIControl");
        uIController = UI.GetComponent<UIController>();
    }

    private void Update()
    {
        if (!isAttached())
        {
            float currentY = transform.position.y;
            if (currentY > recordedYPos)
            {
                recordedYPos = currentY + locChange;
                score += 5;
                uIController.ChangeScore(score);
            }
        }
    }

    public bool isAttached()
    {
        return gameObject.transform.parent != null;
    }

    public void Deattach()
    {
        Transform hook = gameObject.transform.parent;
        if (hook != null)
        {
            Transform planet = hook.transform.parent;
            float planetRotationZ = planet.localEulerAngles.z;
            float hookSpeed = hook.gameObject.GetComponent<HookRotation>().GetSpeed();
            hook.gameObject.GetComponent<HookCapture>().Release();

            transform.parent = null;
            float zRot = (planetRotationZ+hook.localEulerAngles.z-90)*Mathf.Deg2Rad;
            float sinZ = Mathf.Sin(zRot);
            float cosZ = Mathf.Cos(zRot);
            transform.localScale = new Vector3(0.2f,0.2f,1);
            Vector2 facing = new Vector2(cosZ, sinZ);
            Move(facing, hookSpeed);
            GameObject.FindGameObjectWithTag("SoundControl").GetComponent<SoundController>().PlayLeaveHook();
        }
    }

    public void ReleaseMass(Vector2 pos)
    {
        float newx = transform.position.x - pos.x;
        float newy = transform.position.y - pos.y;
        float d = Mathf.Sqrt(Mathf.Pow(newx, 2) + Mathf.Pow(newy, 2));
        if (d != 0)
        {
            float unitX = newx / d;
            float unitY = newy / d;
            Vector2 unitVector = new Vector2(unitX, unitY);
            Vector2 force = unitVector * reverseForce * new Vector2(1,1);
            Vector2 boxForce = unitVector * reverseForce * new Vector2(-10, -10);
            gameObject.GetComponent<BoxUse>().ThrowBox(boxForce);
            rb.AddForce(force);
            GameObject.FindGameObjectWithTag("SoundControl").GetComponent<SoundController>().PlayThrowBox();
        }
    }

    private void Move(Vector2 face,float hookSpeed)
    {
        if (hookSpeed == 0)
            hookSpeed = 1;
        Vector2 force = new Vector2(face.x,face.y);
        rb.AddForce(force*speed*hookSpeed);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Hook"))
        {
            score += 100;
            uIController.ChangeScore(score);
        }
        else
        {
            if (!isAttached())
            {
                GameObject gameController = GameObject.FindGameObjectWithTag("GameController");
                if (gameController)
                {
                    GameController controller = gameController.GetComponent<GameController>();
                    controller.GameOver();
                }
            }
        }
    }

    public int GetScore()
    {
        return this.score;
    }
}
