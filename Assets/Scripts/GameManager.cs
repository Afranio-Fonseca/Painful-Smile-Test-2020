using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [Header("Prefab Association")]
    [SerializeField]
    GameObject chaser = null;
    [SerializeField]
    GameObject shooter = null;

    [Header("UI association")]
    [SerializeField]
    GameObject endGamePanel = null;
    [SerializeField]
    Text timerText = null;
    [SerializeField]
    Text scoreText = null;

    [Header("Settings")]
    [SerializeField]
    float spawnDistance = 10;
    [Range(0, 100)]
    float shooterFrequency = 50;

    float sessionTime = 60;
    float enemySpawnTime = 10;
    float lastSpawnTime = 0;
    int score = 0;
    public static GameManager instance;

    // Start is called before the first frame update
    void Start()
    {
        if (instance) Destroy(this.gameObject);
        else instance = this;
        sessionTime = PlayerPrefs.GetInt("sessionTime", 60);
        enemySpawnTime = PlayerPrefs.GetInt("spawnTime", 10);
    }

    // Update is called once per frame
    void Update()
    {
        sessionTime = Mathf.Max(0, sessionTime - Time.deltaTime);
        timerText.text = "Time left: " + System.String.Format("{0:0.00}", sessionTime) + "s";
        if (PlayerManager.instance == null || sessionTime <= 0 || PlayerManager.instance.health < 0)
        {
            endGamePanel.SetActive(true);
        }
        else
        {
            lastSpawnTime += Time.deltaTime;
            if (lastSpawnTime >= enemySpawnTime)
            {
                LayerMask islandLayer = LayerMask.GetMask("Island");
                Vector3 spawnDirection = new Vector3(Random.value, Random.value, 0).normalized;
                int tries = 0;
                while (Physics2D.Raycast(PlayerManager.instance.transform.position + spawnDirection * 5, spawnDirection, spawnDistance - 3, islandLayer) && tries < 100)
                {
                    tries++;
                    spawnDirection = new Vector3(Random.value, Random.value, 0).normalized;
                }
                GameObject enemy;
                if (Random.value > shooterFrequency / 100.0f) enemy = shooter;
                else enemy = chaser;
                Instantiate(enemy, PlayerManager.instance.transform.position + spawnDirection * spawnDistance, Quaternion.Euler(0, 0, Random.Range(0, 360)));
                lastSpawnTime = 0;
            }
        }
    }

    public void ShipDestroyed(ShipBehaviour ship, ShipBehaviour killer)
    {
        if (ship.GetComponent<PlayerManager>()) EndSession();
        else if (killer.GetComponent<PlayerManager>()) GetPoint();
    }

    public void GetPoint()
    {
        score += 1;
        scoreText.text = "Score: " + score;
    }

    public void EndSession()
    {
        endGamePanel.SetActive(true);
        endGamePanel.transform.GetChild(0).GetComponent<Text>().text = "Score: " + score;
    }

    public void PlayAgain()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Game");
    }

    public void MainMenu()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Main Menu");
    }
}
