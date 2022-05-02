using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TagGeneration : MonoBehaviour
{
    public int num;
    public GameObject props;
    public GameObject[] propPrefabs;
    public GameObject buffs;
    public GameObject buffPrefab;
    
    void Awake()
    {
        EventManager.StartListening("Reset", generate);
    }

    void generate()
    {
        generateProps();
        generateBuffs();
    }

    void generateProps()
    {
        foreach (Transform child in props.transform)
        {
            Destroy(child.gameObject);
        }
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
                float scale = Random.Range(1f, 1.8f);
                temp.localScale = new Vector3(scale, scale, scale);
                pts.Add(pt);
            }
        }
    }

    void generateBuffs()
    {
        foreach (Transform child in buffs.transform)
        {
            Destroy(child.gameObject);
        }
        for (int i = 0; i < 5; i++)
        {
            Instantiate(buffPrefab, buffs.transform).transform.localPosition = randomPoint(8, 15);
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
