using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.IO.Pipes;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class DialogueManager : MonoBehaviour
{
    public NPC npc;

    bool isTalking = false;

    float distance;
    float curResponseTracker = 0;

    public GameObject player;
    public GameObject dialogueUI;
    public GameObject npcObject;
    

    public Text npcName;
    public Text npcDialogueBox;
    public Text playerResponse;


    void Awake() 
    {
        npcObject = this.gameObject;
    }
    // Start is called before the first frame update
    void Start()
    {
        dialogueUI.SetActive(false);
        player = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
        distance = Vector3.Distance(player.transform.position, this.transform.position);
        if (distance <= 2.5f)
        {
            if (Input.GetAxis("Mouse ScrollWheel") < 0f)
            {
                curResponseTracker++;
                if (curResponseTracker >= npc.playerDialogue.Length - 1)
                {
                    curResponseTracker = npc.playerDialogue.Length - 1;
                }
            }
            else if (Input.GetAxis("Mouse ScrollWheel") > 0f)
            {
                curResponseTracker--;
                if (curResponseTracker < 0)
                {
                    curResponseTracker = 0;
                }
            }
            //trigger dialogue
            if (isTalking == false)
            {
                StartConversation();
            }

            if (curResponseTracker == 0 && npc.playerDialogue.Length >= 0)
            {
                playerResponse.text = npc.playerDialogue[0];
                if (Input.GetKeyDown(KeyCode.Return) || Input.GetMouseButtonDown(0))
                {
                    npcDialogueBox.text = npc.dialogue[1];
                    CustomSceneManager.LoadNextScene("ItemCollectionScene");
                }
            }
            else if (curResponseTracker == 1 && npc.playerDialogue.Length >= 1)
            {
                playerResponse.text = npc.playerDialogue[1];
                if (Input.GetKeyDown(KeyCode.Return) || Input.GetMouseButtonDown(0))
                {
                    npcDialogueBox.text = npc.dialogue[2];
                    CustomSceneManager.LoadNextScene("TagScene", npcObject);
                }
            }
            else if (curResponseTracker == 2 && npc.playerDialogue.Length >= 2)
            {
                playerResponse.text = npc.playerDialogue[2];
                if (Input.GetKeyDown(KeyCode.Return) || Input.GetMouseButtonDown(0))
                {
                    npcDialogueBox.text = npc.dialogue[3];
                    CustomSceneManager.LoadNextScene("HideAndSeekScene", npcObject);
                }
            }
            else if (curResponseTracker == 3 && npc.playerDialogue.Length >= 3)
            {
                playerResponse.text = npc.playerDialogue[3];
                if (Input.GetKeyDown(KeyCode.Return) || Input.GetMouseButtonDown(0))
                {
                    npcDialogueBox.text = npc.dialogue[4];
                    CustomSceneManager.LoadNextScene("RolyPoly");
                }
            }
            else if (curResponseTracker == 4 && npc.playerDialogue.Length >= 4)
            {
                playerResponse.text = npc.playerDialogue[4];
                if (Input.GetKeyDown(KeyCode.Return) || Input.GetMouseButtonDown(0))
                {
                    npcDialogueBox.text = npc.dialogue[5];
                    CustomSceneManager.LoadNextScene("GoldenRush");
                }
            }

        }
        else if (isTalking == true)
        {
            EndDialogue();
        }
    }

    void StartConversation()
    {
        isTalking = true;
        curResponseTracker = 0;
        dialogueUI.SetActive(true);
        npcName.text = npc.name;
        npcDialogueBox.text = npc.dialogue[0];
    }

    void EndDialogue()
    {
        isTalking = false;
        dialogueUI.SetActive(false);
    }

    IEnumerator LoadAsyncScene(string sceneName)
    {
        Scene currentScene = SceneManager.GetActiveScene();

        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);

        while (!asyncLoad.isDone)
        {
            yield return null;
        }

        player.GetComponent<CharacterInput>().enabled = true;
        player.GetComponent<PlayerControlScript>().enabled = true;
        SceneManager.MoveGameObjectToScene(player, SceneManager.GetSceneByName(sceneName));
        SceneManager.MoveGameObjectToScene(npcObject, SceneManager.GetSceneByName(sceneName));
        /*
        if (sceneName == "tagDemo" || sceneName == "HideNSeek") {
            SceneManager.MoveGameObjectToScene(npcObject, SceneManager.GetSceneByName(sceneName));
        }
        */
        Debug.Log("Unloading current scene: " + currentScene.path);
        SceneManager.UnloadSceneAsync(currentScene);

    }

}
