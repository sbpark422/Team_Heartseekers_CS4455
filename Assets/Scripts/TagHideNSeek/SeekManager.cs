using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeekManager : MonoBehaviour
{
    public List<GameObject> props;
    public GameObject nearest;
    
    void Awake()
    {
        EventManager.StartListening("Reset", ResetProps);
        EventManager.StartListening("Win", ResetProps);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == "prop")
        {
            props.Add(other.transform.gameObject);
            
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.transform.tag == "prop")
        {
            props.Remove(other.transform.gameObject);
        }
    }

    public void setNearestProp()
    {
        if (nearest != null)
        {
            setActiveBubble(nearest, false);
        }
        if (props.Count == 0)
        {
            return;
        }
        float minDist = Mathf.Infinity;
        foreach (GameObject prop in props)
        {
            float dist = Vector3.Distance(this.transform.position, prop.transform.position);
            if (dist < minDist)
            {
                nearest = prop;
                minDist = dist;
            }

        }
        setActiveBubble(nearest, true);
    }
    void setActiveBubble(GameObject go, bool active)
    {
        GameObject sibling = go.transform.parent.GetChild(1).gameObject;
        sibling.SetActive(active);
    }

    void ResetProps()
    {
        props = new List<GameObject>();
        nearest = null;
    }
}
