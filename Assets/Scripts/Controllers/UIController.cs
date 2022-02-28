using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    public GameObject GameOverScene;

    public GameObject o2Level;
    public GameObject massLevel;
    public Text score;

    private Slider O2Slider;
    private Slider MassSlider;

    private Image o2Image;
    private Image massImage;
    private void Start()
    {
        O2Slider = o2Level.GetComponent<Slider>();
        MassSlider = massLevel.GetComponent<Slider>();
        o2Image=o2Level.transform.GetChild(1).GetChild(0).GetComponent<Image>();
        massImage = massLevel.transform.GetChild(1).GetChild(0).GetComponent<Image>();
    }

    public void ChangeO2Level(float newHealth)
    {
        Color color = DetermineColor(newHealth);
        o2Image.color = color;
        O2Slider.value = newHealth/100f;
    }

    public void ChangeBoxLevel(float newBoxAmount)
    {
        Color color = DetermineColor(newBoxAmount);
        massImage.color = color;
        MassSlider.value = newBoxAmount / 100f;
    }

    private Color DetermineColor(float value)
    {
        Color color = Color.green;
        if (value >= 70f)
        {
            color = Color.green;
        }
        else if (value >= 30f)
        {
            color = Color.yellow;
        }
        else
        {
            color = Color.red;
        }
        return color;
    }

    public void ChangeScore(float newScore)
    {
        score.text = "Score: "+newScore.ToString();
    }

    public void ActivateGameOverScreen(int score,int highScore,bool isNewHighScore,bool isResumed)
    {
        GameOverScene.SetActive(true);
        GameObject resumeButton = GameOverScene.transform.GetChild(1).gameObject;
        resumeButton.SetActive(!isResumed);
        GameObject finalScore =GameOverScene.transform.GetChild(2).gameObject;
        finalScore.GetComponent<Text>().text = "Score: " + score.ToString();
        GameObject hgScore=GameOverScene.transform.GetChild(3).gameObject;
        hgScore.GetComponent<Text>().text = "Highscore: " + highScore.ToString();
        GameObject newHighScore = GameOverScene.transform.GetChild(4).gameObject;
        newHighScore.SetActive(isNewHighScore);
    }

    public void DeactivateGameOverScreen()
    {
        GameOverScene.SetActive(false);
    }
}
