using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class O2 : MonoBehaviour
{
    private const float decreaseLevel=0.1f;

    private float o2Level = 100;
    private float currentTime;

    private UIController uIController;
    private SpaceShip spaceShip;

    private void Start()
    {
        GameObject UI = GameObject.FindGameObjectWithTag("UIControl");
        uIController = UI.GetComponent<UIController>();

        spaceShip = gameObject.GetComponent<SpaceShip>();
    }

    void Update()
    {
        if (Time.time > currentTime && !spaceShip.isAttached())
        {
            o2Level -= 1;
            uIController.ChangeO2Level(o2Level);
            currentTime = Time.time + decreaseLevel;
            if (!hasO2())
            {
                gameObject.SetActive(false);
                GameObject gameController = GameObject.FindGameObjectWithTag("GameController");
                if (gameController)
                {
                    GameController controller = gameController.GetComponent<GameController>();
                    controller.GameOver();
                }
            }
        }
    }

    public void IncreaseO2(float amount)
    {
        o2Level = Mathf.Min(100, o2Level + amount);
        uIController.ChangeO2Level(o2Level);
    } 

    private bool hasO2()
    {
        return o2Level > 0;
    }
}
