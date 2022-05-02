using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CollectableItem : MonoBehaviour
{

	public GameObject selectedObjects;
	public RelationshipMeterController relationship;
    //public GameObject player;

    void Start()
    {
		relationship = GameObject.Find("Slider").GetComponent<RelationshipMeterController>();

	}

    void OnTriggerEnter(Collider c)
	{
		if (c.attachedRigidbody != null)
		{
			BallCollector bc = c.attachedRigidbody.gameObject.GetComponent<BallCollector>();

			//checking if what entered the trigger zone was a BallCollector
			if (bc != null)
			{

				Color color = selectedObjects.GetComponent<Image>().color;
				color.a = 255; //0f
							   //color.a = 0f;
				//print(color);
				selectedObjects.GetComponent<Image>().color = color;

				relationship.updateRelationshipStrength(33);

				bc.ReceiveBall();
				Destroy(this.gameObject);
			}
		}
	}

}
