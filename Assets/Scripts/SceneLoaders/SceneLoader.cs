using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneLoader : MonoBehaviour
{
    GameObject player;
    public GameObject playerStartingPos;
    void Start() {
        player = GameObject.FindGameObjectWithTag("Player");
        player.transform.position = playerStartingPos.transform.position;
    }

    public void DebugClick() {
        Debug.Log("Button Clicked");
    }
}
