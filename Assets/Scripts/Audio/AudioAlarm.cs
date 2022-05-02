using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioAlarm : MonoBehaviour
{
    public AudioClip audio;
    AudioSource myAudio;
    bool playAudio = true;

    public Timer timer;
    // Start is called before the first frame update
    void Start()
    {
        myAudio = GetComponent<AudioSource>();
        //myAudio.PlayDelayed(10.0f);
        timer = GameObject.Find("Timer").GetComponent<Timer>();
    }

    // Update is called once per frame
    void Update()
    {

        if (timer.timer <= 10 && playAudio)
        {
            myAudio.PlayOneShot(audio);
            playAudio = false;
        }
        else if (timer.timer <= 0.0)
        {
            myAudio.Stop();
        }
        //Debug.Log(timer.timer);
    }
}