using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CustomSceneManager : MonoBehaviour
{
    Stack<string> sceneStack;
    Stack<Vector3> playerStack;

    public static CustomSceneManager Instance { get; private set; }

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            Instance.Init();
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Init()
    {
        sceneStack = new Stack<string>();
        playerStack = new Stack<Vector3>();
    }

    public static void LoadNextScene(string nextScene)
    {
        Instance.StartCoroutine(Instance.LoadNextSceneAsync(nextScene));
    }

    public static void LoadNextScene(string nextScene, GameObject objectRef)
    {
        Instance.StartCoroutine(Instance.LoadNextSceneAsync(nextScene, objectRef));
    }

    public static void LoadPrevScene()
    {
        Instance.StartCoroutine(Instance.LoadPrevSceneAsync());
    }

    IEnumerator LoadNextSceneAsync(string nextScene)
    {
        Scene currScene = SceneManager.GetActiveScene();
        sceneStack.Push(currScene.name);

        GameObject player = GameObject.FindGameObjectWithTag("Player");

        if (player)
        {
            player.GetComponent<CharacterInput>().enabled = true;
            player.GetComponent<PlayerControlScript>().enabled = true;
            playerStack.Push(player.transform.position);
        }
        else
        {
            playerStack.Push(Vector3.zero);
        }

        AsyncOperation async = SceneManager.LoadSceneAsync(nextScene, LoadSceneMode.Additive);
        while (!async.isDone)
        {
            yield return null;
        }
        if (player)
        {
            if (nextScene == "obstacleCourse")
            {
                player.GetComponent<PlayerControlScript>().enabled = false;
                player.GetComponent<ObstaclePlayerMovement>().enabled = true;
            }
            SceneManager.MoveGameObjectToScene(player, SceneManager.GetSceneByName(nextScene));
        }
        SceneManager.UnloadSceneAsync(currScene);
    }

    IEnumerator LoadNextSceneAsync(string nextScene, GameObject objectRef)
    {
        Scene currScene = SceneManager.GetActiveScene();
        sceneStack.Push(currScene.name);

        GameObject player = GameObject.FindGameObjectWithTag("Player");

        if (player)
        {
            player.GetComponent<CharacterInput>().enabled = true;
            player.GetComponent<PlayerControlScript>().enabled = true;
            playerStack.Push(player.transform.position);
            Debug.Log("push player stack");
            Debug.Log(player.transform.position);
        }
        else
        {
            playerStack.Push(Vector3.zero);
        }


        AsyncOperation async = SceneManager.LoadSceneAsync(nextScene, LoadSceneMode.Additive);
        while (!async.isDone)
        {
            yield return null;
        }
        if (player)
        {
            SceneManager.MoveGameObjectToScene(player, SceneManager.GetSceneByName(nextScene));
            
        }
        if (objectRef)
        {
            objectRef.GetComponent<DialogueManager>().enabled = false;
            SceneManager.MoveGameObjectToScene(objectRef, SceneManager.GetSceneByName(nextScene));
        }
        AsyncOperation asyncUnload = SceneManager.UnloadSceneAsync(currScene);
        while (!asyncUnload.isDone)
        {
            yield return null;
        }
        EventManager.TriggerEvent("DoneLoading");
    }

    IEnumerator LoadPrevSceneAsync()
    {
        Scene currScene = SceneManager.GetActiveScene();

        string prevScene;
        try
        {
            prevScene = sceneStack.Pop();
        }
        catch
        {
            prevScene = "BackToMain";
        }
        Vector3 position;
        try
        {
            position = playerStack.Pop();
            Debug.Log("pop player stack");
            Debug.Log(position);
        }
        catch
        {
            position = Vector3.zero;
        }

        AsyncOperation async = SceneManager.LoadSceneAsync(prevScene, LoadSceneMode.Additive);
        while (!async.isDone)
        {
            yield return null;
        }

        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player)
        {
            player.GetComponent<CharacterInput>().enabled = true;
            player.GetComponent<PlayerControlScript>().enabled = true;
            if (currScene.name == "obstacleCourse")
            {
                //Time.timeScale = 0;
                player.GetComponent<ObstaclePlayerMovement>().enabled = false;
            }
            Animator anim = player.GetComponent<Animator>();
            anim.SetBool("celebrate", false);
            anim.SetBool("losing", false);
            SceneManager.MoveGameObjectToScene(player, SceneManager.GetSceneByName(prevScene));
            player.transform.position = position;
        }
        SceneManager.UnloadSceneAsync(currScene);
    }
}
