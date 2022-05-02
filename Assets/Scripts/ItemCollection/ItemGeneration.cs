using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemGeneration : MonoBehaviour
{
    public GameObject props;
    public GameObject[] propPrefabs;
    public GameObject items;
    public GameObject[] itemPrefabs;
    public GameObject inventory;
    public GameObject[] imagePrefabs;

    void Awake()
    {
        EventManager.StartListening("Reset", Generate);
    }
    void Generate()
    {
        // destroy props, items, inventory images
        DestroyChildren(props);
        DestroyChildren(items);
        DestroyChildren(inventory);

        // generate new props, items, inventory images
        List<Vector3> pts = new List<Vector3>();
        while (pts.Count < 10)
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
                temp.rotation = Quaternion.AngleAxis(Random.Range(0, 360), Vector3.up);
                pts.Add(pt);
            }
        }
        int[] numSplit = NumPartition();
        for (int i = 0; i < numSplit.Length; i++)
        {
            int count = 0;
            while (count < numSplit[i])
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
                    Transform temp = Instantiate(itemPrefabs[i], items.transform).transform;
                    temp.GetComponent<CollectItem>().inventoryImg = Instantiate(imagePrefabs[i], inventory.transform);
                    pt.y = temp.localPosition.y;
                    temp.localPosition = pt;
                    temp.rotation = Quaternion.AngleAxis(Random.Range(0, 360), Vector3.up);
                    pts.Add(pt);
                    count += 1;
                }
            }
        }
    }

    void DestroyChildren(GameObject parent)
    {
        foreach (Transform child in parent.transform)
        {
            Destroy(child.gameObject);
        }
    }

    Vector3 randomPoint(float minRange, float maxRange)
    {
        Vector3 start = new Vector3(0, 0, -17);
        Vector3 pt = start;
        while (Vector3.Distance(pt, start) < minRange)
        {
            pt = new Vector3(maxRange * (2 * Random.value - 1), 0, maxRange * (2 * Random.value - 1));
        }
        return pt;
    }

    int[] NumPartition()
    {
        int num = GetComponent<ItemCollectControl>().maxNum;
        int[] numSplit = new int[itemPrefabs.Length];
        for (int i = 0; i < num; i++)
        {
            numSplit[Random.Range(0, itemPrefabs.Length)] += 1;
        }
        return numSplit;
    }
}
