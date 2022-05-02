using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class HideNPC : MonoBehaviour
{
    public GameObject player;
    public GameObject npcPrefab;
    public GameObject[] propPrefabs;

    void Awake()
    {
        EventManager.StartListening("Reset", hideNPC);
        EventManager.StartListening("Win", revealNPC);
    }

    void Start()
    {
        player = GameObject.Find("PlayerMale");
        if (player == null)
        {
            player = GameObject.Find("PlayerFemale");
        }

        hideNPC();
    }
    Vector3 randomPoint(Vector3 center, float range = 15)
    {
        NavMeshHit hit;
        while (Vector3.Distance(Vector3.zero, center) < 7)
        {
            for (int i = 0; i < 10; i++)
            {
                Vector3 randPt = center + Random.insideUnitSphere * range;
                if (NavMesh.SamplePosition(randPt, out hit, 3f, NavMesh.AllAreas))
                {
                    center = hit.position;
                    center.y = 0;
                }
            }
        }
        return center;
    }

    public void hideNPC()
    {
        if (this.transform.childCount > 0)
        {
            Destroy(this.transform.GetChild(0).gameObject);
        }
        Instantiate(propPrefabs[Random.Range(0, propPrefabs.Length)],
                    this.transform).transform.localPosition = Vector3.zero;
        // this.transform.position = randomPoint(Vector3.zero);
    }
    public void revealNPC()
    {
        Destroy(this.transform.GetChild(0).gameObject);
        npcPrefab = GameObject.FindGameObjectWithTag("npc"); 
        Instantiate(npcPrefab, this.transform).transform.localPosition = new Vector3(0, 0.5f, 0);
        npcPrefab.GetComponent<Animator>().SetBool("losing", true);
    }
}
