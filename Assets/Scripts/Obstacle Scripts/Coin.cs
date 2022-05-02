using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    public float rotationSpeed = 90f;

    private void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.GetComponent<Obstacle>() != null)
        {
            Destroy(gameObject);
            return;
        }

        // check if object we collided with is the player
        if (other.gameObject.tag != "Player")
        {
            return;
        }

        // increase player score
        GameManager.inst.IncrementScore();
        // destroy coin
        Destroy(gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0, 0, rotationSpeed * Time.deltaTime);   
    }
}
