using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HideGeneration : MonoBehaviour
{
    public int num; // number of props
    public GameObject props; // parent GameObject of all props
    public GameObject[] propPrefabs; // array of prefabs for props
    public GameObject npc;

    void Awake()
    {
        EventManager.StartListening("Reset", Generate);
    }

    void Generate()
    {
        // destroy current props
        foreach (Transform child in props.transform)
        {
            Destroy(child.gameObject);
        }

        // generate new props
        List<Vector3> pts = new List<Vector3>();
        while (pts.Count < num)
        {
            Vector3 pt = randomPoint(5, 15);
            bool instantiate = true;
            if (pts.Count > 0)
            {
                foreach (Vector3 t in pts)
                {
                    if (Vector3.Distance(pt, t) < 3)
                    {
                        instantiate = false;
                        break;
                    }
                }
            }
            if (instantiate)
            {
                Transform temp = Instantiate(propPrefabs[Random.Range(0, propPrefabs.Length)], props.transform).transform;
                pt.y = temp.localPosition.y;
                temp.localPosition = pt;
                pts.Add(pt);
            }
        }

        while (true)
        {
            Vector3 pt = randomPoint(7, 15);
            bool instantiate = true;
            foreach (Vector3 t in pts)
            {
                if (Vector3.Distance(pt, t) < 3)
                {
                    instantiate = false;
                    break;
                }
            }
            if (instantiate)
            {
                npc.transform.position = pt;
                break;
            }
        }
    }

    Vector3 randomPoint(float minRange, float maxRange)
    {
        Vector3 pt = Vector3.zero;
        while (Vector3.Distance(pt, Vector3.zero) < minRange)
        {
            pt = new Vector3(maxRange * (2 * Random.value - 1), 0, maxRange * (2 * Random.value - 1));
        }
        return pt;
    }
}
