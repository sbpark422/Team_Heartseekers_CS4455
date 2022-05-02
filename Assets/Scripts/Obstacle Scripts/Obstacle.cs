using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    ObstaclePlayerMovement obsPlayerMovement;
    // Start is called before the first frame update
    void Start()
    {
        obsPlayerMovement = GameObject.FindObjectOfType<ObstaclePlayerMovement>();
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        // kill player if collides with obj
        if (collision.gameObject.tag == "Player")
        {
            obsPlayerMovement.Die();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
