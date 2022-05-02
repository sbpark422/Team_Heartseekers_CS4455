using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileMatching : MonoBehaviour
{
    public GameObject player;
    [SerializeField] int tilesLeft;
    public Timer timer;
    bool gameOver = false;
    bool paused = false;
    public GameObject ball;
    public HashSet<string> tblNames;
    PlayerControlScript p;

    void Awake()
    {
        Time.timeScale = 1;
        EventManager.StartListening("Reset", ResetTile);
        EventManager.StartListening("Start", StartTile);
        EventManager.StartListening("Pause", Pause);
        EventManager.StartListening("Win", Win);
        EventManager.StartListening("Lose", Lose);
        EventManager.StartListening("TileDecrease", DecreaseTile);
        EventManager.StartListening("Generate", Generate);
        EventManager.TriggerEvent("Generate");
    }
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        timer = GameObject.Find("Timer").GetComponent<Timer>();
        EventManager.TriggerEvent("Reset");
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            EventManager.TriggerEvent("Win");
            p.increaseTotalWinCount();
            p.checkIfWin();

        }
        if (Input.GetKeyDown(KeyCode.Delete))
        {
            EventManager.TriggerEvent("Lose");
        }
        if (paused)
        {
            Time.timeScale = 0;
        }
        else if (!timer.running || gameOver)
        {
            //Time.timeScale = 1;
            //cinput.enabled = false;
        }
        else
        {
            //cinput.enabled = true;
            Time.timeScale = 1;
            if (timer.timer == 0 && !gameOver)
            {
                EventManager.TriggerEvent("Lose");
            }
        }
    }

    void DecreaseTile () {
        tilesLeft--;
        if (tilesLeft <= 0) 
        {
            EventManager.TriggerEvent("Win");
        }
    }

    void ResetTile() {
        Time.timeScale = 1;
        gameOver = false;
        tilesLeft = tblNames.Count;
        player = GameObject.FindGameObjectWithTag("Player");
        player.transform.position = GameObject.Find("StartingPlayerPos").transform.position;
        ball.transform.position = GameObject.Find("SphereStartPos").transform.position;
        player.GetComponent<Animator>().SetBool("celebrate", false);
        player.GetComponent<Animator>().SetBool("losing", false);
        EventManager.TriggerEvent("ResetEachTile");
    }

    void StartTile() {
        paused = false;
    }

    void Pause() {
        paused = true;
    }

    void Win() {
        gameOver = true;
        player.GetComponent<Animator>().SetBool("celebrate", true);
        EventManager.TriggerEvent("Generate");
    }

    void Lose() {
        gameOver = true;
        player.GetComponent<Animator>().SetBool("losing", true);
    }

    void Generate()
    {
        tblNames = TilePatterns.GeneratePattern();
        tilesLeft = tblNames.Count;
        Debug.Log("Generate");
    }
}
