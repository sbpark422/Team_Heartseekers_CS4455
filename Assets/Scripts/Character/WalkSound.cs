using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkSound : MonoBehaviour
{
    public AudioClip walk;
    public AudioClip jump;
    Rigidbody rb;
    AudioSource footsound;
    bool isMoving = false;
    bool isJumping = false;
    bool playAudio = true;
    Animator anim;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        footsound = GetComponent<AudioSource>();
        anim = rb.GetComponent<Animator>();
        walk = Resources.Load<AudioClip>("Audio/footstep");
        jump = Resources.Load("jump") as AudioClip;
        footsound.clip = walk;
    }

    void Update()
    {
        MoveSFX();
    }

    void MoveSFX()
    {
        if (anim.GetBool("moving") || anim.GetBool("obstacleRun")) // if (anim.GetCurrentAnimatorClipInfo(0)[0].clip.name == "walking")
        {
            isMoving = true;
        }
        else
        {
            isMoving = false;
        }

        if (anim.GetBool("jumping"))
        {
            isJumping = true;
        }
        else
        {
            isJumping = false;
        }

        if (isJumping)
        {
            if (footsound.clip == walk && playAudio)
            {
                footsound.Stop();
                footsound.PlayOneShot(jump);
                playAudio = false;
            }
            else
            {
                if (!footsound.isPlaying && playAudio)
                {
                    footsound.PlayOneShot(jump);
                    playAudio = false;
                }
            }
        }
        else if (isMoving)
        {
            if (!footsound.isPlaying)
            {
                footsound.Play();
            }
            playAudio = true;
        }
        else
        {
            footsound.Stop();
            playAudio = true;
        }

    }
}