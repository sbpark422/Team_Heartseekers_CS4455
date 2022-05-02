using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseButton : MonoBehaviour
{
    public CanvasGroup canvasGroup;
    void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();
        EventManager.StartListening("Reset", Hide);
        EventManager.StartListening("Start", Show);
        EventManager.StartListening("Pause", Hide);
        EventManager.StartListening("Win", Hide);
        EventManager.StartListening("Lose", Hide);
    }

    void Hide()
    {
        canvasGroup.alpha = 0;
        canvasGroup.interactable = false;
        canvasGroup.blocksRaycasts = false;
    }

    void Show()
    {
        canvasGroup.alpha = 1;
        canvasGroup.interactable = true;
        canvasGroup.blocksRaycasts = true;
    }
}
