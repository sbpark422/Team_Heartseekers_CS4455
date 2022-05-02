using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemCollectControl : MonoBehaviour
{
    public int maxNum;
    public int num;
    public GameObject player;
    public Timer timer;

    bool paused = false;
    bool gameOver = false;
    CharacterInput cinput;
    PlayerControlScript p;

    void Awake()
    {
        EventManager.StartListening("Reset", ResetIC);
        EventManager.StartListening("Start", StartIC);
        EventManager.StartListening("Pause", Pause);
        EventManager.StartListening("Win", GameOver);
        EventManager.StartListening("Lose", GameOver);
        EventManager.StartListening("CollectItem", CollectedItem);
    }

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        cinput = player.GetComponent<CharacterInput>();
        EventManager.TriggerEvent("Reset");
    }

    void Update()
    {
        if (paused)
        {
            Time.timeScale = 0;
        }
        else if (!timer.running || gameOver)
        {
            cinput.enabled = false;
        }
        else
        {
            cinput.enabled = true;
            Time.timeScale = 1;
            // seekMng.setNearestProp();
            // if (Input.GetKeyDown(KeyCode.F) && seekMng.props.Count > 0)
            // {
                // playerInvestigate();
            // }
            if (timer.timer == 0 && !gameOver)
            {
                EventManager.TriggerEvent("Lose");
            }
        }
    }

    void StartIC()
    {
        paused = false;
    }

    void Pause()
    {
        paused = true;
    }

    void GameOver()
    {
        gameOver = true;
    }

    void ResetIC()
    {
        gameOver = false;
        num = 0;
        maxNum = Random.Range(10, 15);
        player.transform.position = new Vector3(0, player.transform.position.y, -17);
        player.transform.rotation = Quaternion.identity;
    }

    void CollectedItem()
    {
        num += 1;
        if (num == maxNum)
        {
            EventManager.TriggerEvent("Win");
            p.increaseTotalWinCount();
            p.checkIfWin();
        }
    }
}
