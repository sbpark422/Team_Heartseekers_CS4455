using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    public bool running;
    public float maxTimer;
    public float timer;
    public float speedTimer;
    public Text timerText;
    public Text speedText;

    void Awake()
    {
        EventManager.StartListening("Reset", resetTimer);
        EventManager.StartListening("Start", startTimer);
        EventManager.StartListening("Pause", stopTimer);
        EventManager.StartListening("Win", stopTimer);
        EventManager.StartListening("Lose", stopTimer);
        EventManager.StartListening("SpeedBuff", startSpeedTimer);
    }

    void Update()
    {
        if (running)
        {
            if (timer > 0)
            {
                timer -= Time.deltaTime;
            }
            else
            {
                timer = 0;
            }
            if (speedTimer > 0)
            {
                speedTimer -= Time.deltaTime;
            }
            else
            {
                speedTimer = 0;
            }
        }

        timerText.text = string.Format("{0:0.#}", timer);
        if (speedTimer > 0 && timer > 0)
        {
            speedText.text = string.Format("{0:0.#}", speedTimer);
        }
        else
        {
            speedText.text = "";
        }
    }

    public void resetTimer()
    {
        stopTimer();
        timer = maxTimer;
        speedTimer = 0;
    }
    public void startTimer()
    {
        running = true;
    }
    public void stopTimer()
    {
        running = false;
    }
    public void startSpeedTimer()
    {
        speedTimer = 4;
    }
}
