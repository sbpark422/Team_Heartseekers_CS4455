using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WinLose : MonoBehaviour
{
    public CanvasGroup canvasGroup;
    public Text text;
    public PlayerControlScript player;

    void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();
        text = GetComponentInChildren<Text>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerControlScript>();
        EventManager.StartListening("Reset", Hide);
        EventManager.StartListening("Win", Win);
        EventManager.StartListening("Lose", Lose);
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

    void Win()
    {
        text.text = "Win!!";
        Show();
        player.increaseTotalWinCount();
        player.checkIfWin();
    }

    void Lose()
    {
        text.text = "Lose...";
        Show();
    }
}
