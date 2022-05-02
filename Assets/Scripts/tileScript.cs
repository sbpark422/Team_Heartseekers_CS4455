using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class tileScript : MonoBehaviour
{
    HashSet<string> tblNames;
    bool active = true;
    Color color = new Color(0.23f, 0.9f, 1.0f, 1.0f);
    Animator anim;

    //change to true to reveal solution path
    public static bool debug = false;

    void Awake() {
        EventManager.StartListening("ResetEachTile", ResetTile);
        anim = GetComponentInChildren<Animator>();
    }

    void OnTriggerEnter(Collider other)
    {
        var tileRenderer = gameObject.GetComponentInChildren<Renderer>();
        if (other.name != "Ball")
        {
            return;
        }
        anim.SetBool("depressed", true);
        if (tblNames.Contains(gameObject.name))
        {
            if (active)
            {
                tileRenderer.material.SetColor("_Color", Color.green);
                EventManager.TriggerEvent("TileDecrease");
                active = false;
            }
        }
        else
        {
            tileRenderer.material.SetColor("_Color", Color.red);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.name != "Ball")
        {
            return;
        }
        anim.SetBool("depressed", false);
    }

    void ResetTile() {
        tblNames = GameObject.Find("Tiles").GetComponent<TileMatching>().tblNames;
        var tileRenderer = gameObject.GetComponentInChildren<Renderer>();
        if (!debug || tblNames.Contains(gameObject.name))
        {
            tileRenderer.material.SetColor("_Color", color);
        }
        else
        {
            tileRenderer.material.SetColor("_Color", Color.red);
        }
        active = true;
    }

    

}
