using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TagControl : MonoBehaviour
{
    public TagNPC tagNPC;
    public GameObject player;
    public Timer timer;

    bool paused = false;
    bool gameOver = false;
    CharacterInput cinput;
    PlayerControlScript pcontrol;

    void Awake()
    {
        EventManager.StartListening("Reset", ResetTag);
        EventManager.StartListening("Start", StartTag);
        EventManager.StartListening("Pause", Pause);
        EventManager.StartListening("Win", Win);
        EventManager.StartListening("Lose", Lose);
    }

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        cinput = player.GetComponent<CharacterInput>();
        pcontrol = player.GetComponent<PlayerControlScript>();
        EventManager.TriggerEvent("Reset");
    }

    void Update()
    {
        float dist = Vector3.Distance(player.transform.position, tagNPC.transform.position);
        if (dist < 2 && !gameOver)
        {
            EventManager.TriggerEvent("Lose");
        }
        if (paused)
        {
            Time.timeScale = 0;
        }
        else if (!timer.running || gameOver)
        {
            Time.timeScale = 1;
            cinput.enabled = false;
            tagNPC.executeTagState();
        }
        else
        {
            cinput.enabled = true;
            Time.timeScale = 1;
            tagNPC.executeTagState();
            if (timer.speedTimer > 0)
            {
                pcontrol.directionMaxSpeed = 125;
            }
            else
            {
                pcontrol.directionMaxSpeed = 100;
            }
            if (timer.timer == 0 && !gameOver)
            {
                EventManager.TriggerEvent("Win");
            }
        }
    }
    void StartTag()
    {
        paused = false;
    }
    void Pause()
    {
        paused = true;
    }
    void Lose()
    {
        gameOver = true;
    }
    void Win()
    {
        gameOver = true;
        pcontrol.increaseTotalWinCount();
        pcontrol.checkIfWin();
    }
    void ResetTag()
    {
        gameOver = false;
        player.transform.position = new Vector3(0, player.transform.position.y, 0);
        player.transform.rotation = Quaternion.identity;
    }
}
