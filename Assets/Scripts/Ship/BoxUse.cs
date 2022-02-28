using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxUse : MonoBehaviour
{
    public GameObject box;

    private int boxAmount = 100;
    private UIController uIController;

    private void Start()
    {
        GameObject UI = GameObject.FindGameObjectWithTag("UIControl");
        uIController = UI.GetComponent<UIController>();
    }

    public void ThrowBox(Vector2 boxForce)
    {
        GameObject newBox = Instantiate(box, new Vector2(transform.position.x, transform.position.y), Quaternion.identity);
        newBox.GetComponent<Rigidbody2D>().AddForce(boxForce);
        newBox.GetComponent<BoxBehavior>().DestroyBox(0.5f);
        boxAmount-=2;

        uIController.ChangeBoxLevel(boxAmount);
    }

    public void IncreaseMass(int amount)
    {
        boxAmount=Mathf.Min(100, boxAmount+amount);
        uIController.ChangeBoxLevel(boxAmount);
    }

    public bool DoesMassExist()
    {
        return boxAmount != 0;
    }
}
