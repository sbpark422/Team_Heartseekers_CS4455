using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public int score;
    public Text scoreText;
    [SerializeField] ObstaclePlayerMovement playerMovement;

    public static GameManager inst;

    void Awake()
    {
        inst = this;
        EventManager.StartListening("Reset", ResetManager);
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player)
        {
            Debug.Log("player found");
            Debug.Log(player.name);
        }
        else
        {
            player = Instantiate(Resources.Load("HotkeyPlayerMale") as GameObject);
            playerMovement = player.AddComponent<ObstaclePlayerMovement>();
            Debug.Log("created player");
        }
        player.GetComponent<PlayerControlScript>().enabled = false;
        player.GetComponent<ObstaclePlayerMovement>().enabled = true;
    }

    void OnDestroy()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        player.GetComponent<PlayerControlScript>().enabled = true;
        player.GetComponent<ObstaclePlayerMovement>().enabled = false;
    }

    void Start()
    {
        playerMovement = GameObject.FindGameObjectWithTag("Player").GetComponent<ObstaclePlayerMovement>();
        EventManager.TriggerEvent("Reset");
    }

    public void IncrementScore()
    {
        score++;
        scoreText.text = "SCORE: " + score;

        playerMovement.speed += ObstaclePlayerMovement.speedIncreasePerPoint;
        
    }

    void ResetManager() {
        score = 0;
        scoreText.text = "SCORE: " + score; 
    }
}
