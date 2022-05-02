using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CollectItem : MonoBehaviour
{
    public GameObject inventoryImg;

    void OnTriggerEnter(Collider other)
    {
        if (other.attachedRigidbody != null)
        {
            if (other.gameObject.tag == "Player")
            {
                Color color = inventoryImg.GetComponent<Image>().color;
                color.a = 255;
                inventoryImg.GetComponent<Image>().color = color;
                EventManager.TriggerEvent("CollectItem");
                Destroy(this.gameObject);
            }
        }
    }
}
