using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PropPrompt : MonoBehaviour
{
    public CanvasGroup canvasGroup;
    public float maxTimer = 1;
    public float timer = 0;
    
    void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();
        EventManager.StartListening("Reset", Hide);
        EventManager.StartListening("Win", Hide);
        EventManager.StartListening("Lose", Hide);
        EventManager.StartListening("PropPrompt", ResetTimer);
    }

    // Update is called once per frame
    void Update()
    {
        if (timer > 0)
        {
            timer -= Time.deltaTime;
            Show();
        }
        else
        {
            Hide();
        }
    }

    void Hide()
    {
        timer = 0;
        canvasGroup.alpha = 0;
        canvasGroup.interactable = false;
        canvasGroup.blocksRaycasts = false;
    }

    void Show()
    {
        if (timer > 0.25f)
        {
            canvasGroup.alpha = 1;
        }
        else
        {
            canvasGroup.alpha = timer * 4;
        }
        canvasGroup.interactable = true;
        canvasGroup.blocksRaycasts = true;
    }

    void ResetTimer()
    {
        timer = maxTimer;
    }
}
