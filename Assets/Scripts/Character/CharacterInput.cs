using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterInput : MonoBehaviour
{

    public Vector3 DirectionInput
    {
        get;
        private set;
    }

    public float MagnitudeInput
    {
        get;
        private set;
    }

    public bool Jump
    {
        get;
        private set;
    }

    public bool Run
    {
        get;
        private set;
    }

    public Quaternion Turn
    {
        get;
        private set;
    }

    // Update is called once per frame
    void Update()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical"); 

        Vector3 input = new Vector3(h, 0, v);

        float inMag = Mathf.Clamp01(input.magnitude);

        if (!Input.GetKey(KeyCode.LeftShift)) {
            inMag *= 0.75f;
        }
        /*
        if (input != Vector3.zero)
        {
            Turn = Quaternion.LookRotation(input, Vector3.up);
        }
        */

        DirectionInput = input;
        MagnitudeInput = inMag;

        
        Jump = Input.GetKeyDown(KeyCode.Space);

    }
}
