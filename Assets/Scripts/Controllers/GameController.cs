using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public GameObject[] planets;
    public GameObject asteroid;
    private GameObject player;
    private GameObject background;

    private ArrayList _asteroids;

    private float player_y_limit=-3.5f;
    private Queue<GameObject> planetQueue;

    public GameObject explosion;

    private bool isResumed = false;
    void Start()
    {
        _asteroids = new ArrayList();
        planetQueue = new Queue<GameObject>(); 
        player = GameObject.FindGameObjectWithTag("Player");
        background = GameObject.FindGameObjectWithTag("Scene");
        InstantiatePlanets();
        StartCoroutine(CreateAsteroid());
    }

    void Update()
    {
        if (player&&player.transform.position.y >= player_y_limit)
        {
            player_y_limit += 10;
            InstantiatePlanets();
            if (player_y_limit >= 20)
            {
                GameObject deleted = planetQueue.Dequeue();
                Destroy(deleted);
                GameObject deleted2 = planetQueue.Dequeue();
                Destroy(deleted2);
            }
        }
    }

    IEnumerator CreateAsteroid()
    {
        yield return new WaitForSeconds(3);
        while (true)
        {
            float xPos = Random.Range(-11,11);
            float yPos = background.transform.position.y+10;
            Vector2 pos = new Vector2(xPos, yPos);

            for(int i = 0; i < _asteroids.Count; i++)
            {
                GameObject obj = _asteroids[i] as GameObject;
                if (isOutOfScene(obj))
                {
                    Destroy(obj);
                    _asteroids.Remove(obj);
                }
            }
            
            GameObject created=Instantiate(asteroid,pos, Quaternion.identity);
            _asteroids.Add(created);
            yield return new WaitForSeconds(4);
        }
    }
    
    private void InstantiatePlanets()
    {
        // x
        // -10 -6
        // 4 8
        // y 
        // -3.5 -2.5
        // 2.5 3.5
        for(int i = 0; i < 2; i++)
        {
            GameObject planet = CreateRandomPlanet();
            if (planet)
            {
                float x_pos = Random.Range(-10, -6);
                float y_pos = Random.Range(player_y_limit, player_y_limit-1f);
                if (i==1)
                {
                    x_pos += 14;
                    y_pos += 10f;
                }
                Vector2 pos = new Vector2(x_pos, y_pos);
                GameObject created = Instantiate(planet, pos, Quaternion.identity);
                planetQueue.Enqueue(created);
            }
        }
    }

    private GameObject CreateRandomPlanet()
    {
        GameObject planet = null;
        int arrayLength = planets.Length;
        if (arrayLength > 0)
        {
            int rand = Random.Range(0, arrayLength);
            while (rand == arrayLength)
            {
                rand = Random.Range(0, arrayLength);
            }
            planet = planets[rand];
        }
        return planet;
    }

    public void GameOver()
    {
        GameObject created=Instantiate(explosion,player.transform.position,Quaternion.identity);
        Destroy(created, 1f);
        GameObject.FindGameObjectWithTag("SoundControl").GetComponent<SoundController>().PlayExplosion();
        bool isNewHighScore = false;
        SpaceShip ship = player.GetComponent<SpaceShip>();
        int score = ship.GetScore();
        int highScore=PlayerPrefs.GetInt("Highscore",0);
        if (score > highScore)
        {
            isNewHighScore = true;
            highScore = score;
            PlayerPrefs.SetInt("Highscore", score);
        }
        GameObject.FindGameObjectWithTag("UIControl").GetComponent<UIController>().ActivateGameOverScreen(score,highScore,isNewHighScore,isResumed);
    }

    public void RestartGame()
    {
        SceneManager.LoadScene("The Scene");
    }

    public void ResumeGame()
    {
        // Ads is shown
        Vector3 spaceShipPos = background.transform.position;
        spaceShipPos.Set(spaceShipPos.x, spaceShipPos.y-3f, 0);
        player.SetActive(true);
        player.transform.position = spaceShipPos;
        player.GetComponent<BoxUse>().IncreaseMass(100);
        player.GetComponent<O2>().IncreaseO2(100);
        GameObject.FindGameObjectWithTag("UIControl").GetComponent<UIController>().DeactivateGameOverScreen();
        isResumed = true;
    }

    private bool isOutOfScene(GameObject gameObject)
    {
        if(gameObject)
            return gameObject.transform.position.y < background.transform.position.y-5;
        return false;
    }
}
