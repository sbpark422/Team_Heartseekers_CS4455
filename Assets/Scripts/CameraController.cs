using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CameraController : MonoBehaviour
{
    public GameObject target;
    public static Vector3 camOffset = new Vector3(0, 8, -6);
    public static bool DEBUG = true;

    void OnEnable() 
    {
        target = GameObject.FindGameObjectWithTag("Player");
        if (DEBUG && target == null)
        {
            target = Instantiate(Resources.Load("HotkeyPlayerMale") as GameObject);
        }

        if (target.GetComponent<BallCollector>() == null)
        {
            target.AddComponent<BallCollector>();
        }
        if (target.GetComponent<PlayerControlScript>() == null)
        {
            target.AddComponent<PlayerControlScript>();
        }
        if (target.GetComponent<SeekManager>() == null)
        {
            target.AddComponent<SeekManager>();
        }
        if (target.GetComponent<VelocityReporter>() == null)
        {
            target.AddComponent<VelocityReporter>();
        }
        if (target.GetComponent<WalkSound>() == null)
        {
            target.AddComponent<WalkSound>();
        }
    }

    void LateUpdate()
    {
        if (target != null)
        {
            this.transform.position = target.transform.position + camOffset;
        }
        if (SceneManager.GetActiveScene().name != "BackToMain" && Input.GetKeyDown(KeyCode.Backspace))
        {
            StartCoroutine(LoadAsyncScene("BackToMain"));
        }
        if (SceneManager.GetActiveScene().name != "BackToMain" && Input.GetKeyDown(KeyCode.P))
        {
            CustomSceneManager.LoadNextScene("PauseScreen");
        }
    }

    IEnumerator LoadAsyncScene(string sceneName)
    {
        Scene currentScene = SceneManager.GetActiveScene();

        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);

        while (!asyncLoad.isDone)
        {
            yield return null;
        }

        target.GetComponent<CharacterInput>().enabled = true;
        target.GetComponent<PlayerControlScript>().enabled = true;
        SceneManager.MoveGameObjectToScene(target, SceneManager.GetSceneByName(sceneName));
        SceneManager.UnloadSceneAsync(currentScene);

    }
}
