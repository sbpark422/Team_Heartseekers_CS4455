using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetBall : MonoBehaviour
{
    public GameObject ball;
    public GameObject ballStartPos;
    public Rigidbody rb;

    void Start() {
        rb = ball.GetComponent<Rigidbody>();
    }

    void OnTriggerEnter(Collider other) {
        if (other.tag == "Player") {
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
            ball.transform.position = ballStartPos.transform.position;
        }
    }
}
