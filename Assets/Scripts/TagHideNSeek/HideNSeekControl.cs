using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HideNSeekControl : MonoBehaviour
{
    public HideNPC hideNPC;
    public GameObject player;
    public Timer timer;

    bool paused = false;
    bool gameOver = false;
    SeekManager seekMng;
    CharacterInput cinput;

    void Awake()
    {
        EventManager.StartListening("Reset", ResetHNS);
        EventManager.StartListening("Start", StartHNS);
        EventManager.StartListening("Pause", Pause);
        EventManager.StartListening("Win", GameOver);
        EventManager.StartListening("Lose", GameOver);
    }

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        seekMng = player.GetComponent<SeekManager>();
        cinput = player.GetComponent<CharacterInput>();
        
        EventManager.TriggerEvent("Reset");

        SphereCollider sphere = player.GetComponent<SphereCollider>();
        if (!sphere)
        {
            sphere = player.AddComponent<SphereCollider>();
        }
        sphere.isTrigger = true;
        sphere.radius = 1.5f;
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
            seekMng.setNearestProp();
            if (Input.GetKeyDown(KeyCode.F) && seekMng.props.Count > 0)
            {
                playerInvestigate();
            }
            if (timer.timer == 0 && !gameOver)
            {
                EventManager.TriggerEvent("Lose");
            }
        }
    }

    void playerInvestigate()
    {
        GameObject parent = seekMng.nearest.transform.parent.gameObject;
        GameObject grandparent = parent.transform.parent.gameObject;
        if (grandparent.tag == "hideNPC")
        {
            EventManager.TriggerEvent("Win");
        }
        else
        {
            EventManager.TriggerEvent("PropPrompt");
        }
    }

    void StartHNS()
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

    void ResetHNS()
    {
        gameOver = false;
        player.transform.position = new Vector3(0, player.transform.position.y, 0);
        player.transform.rotation = Quaternion.identity;
    }
}
