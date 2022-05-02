using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator), typeof(Rigidbody), typeof(CapsuleCollider))]
[RequireComponent(typeof(CharacterInput))]
public class PlayerControlScript : MonoBehaviour
{
    private Rigidbody rbody;
    private Animator anim;	
    private CharacterInput cinput;
    public float directionMaxSpeed = 1f;
    public float turnMaxSpeed = 1f;
    public float sprintMultiplier = 1.5f;
    public float ySpeed = 0f;
    public float jumpSpeed = 20f;
    public float jumpForwardSpeed = 1f;

    public float animationSpeed = 1f;

    public int groundContactCount = 0;

    private Vector3 inputDirection;
    private float inputMag;
    private bool inputJump;

    private int winCount = 0;


    public float jumpableGroundNormalMaxAngle = 45f;
    public bool closeToJumpableGround;

    public bool IsGrounded
    {
        get
        {
            return groundContactCount > 0;
        }
    }

    void OnEnable() {
        if (anim != null) {
            anim.SetBool("obstacleRun", false);
        }
    }

    void Awake() 
    {
        anim = GetComponent<Animator>();

        if (anim == null)
        {
            Debug.Log("Animator is null");
        }

        rbody = GetComponent<Rigidbody>();

        if (rbody == null)
        {
            Debug.Log("Rigid body is null");
        }

        cinput = GetComponent<CharacterInput>();

        if (cinput == null)
        {
            Debug.Log("CharacterInput is null");
        }

    }

    void Start()
    {

    }

    void Update()
    {
        anim.speed = animationSpeed;
        
        if (cinput.enabled)
        {
            inputDirection = cinput.DirectionInput;
            inputMag = cinput.MagnitudeInput;
            inputJump = cinput.Jump;
            //inputTurn = cinput.Turn;
        }
        
    }

    void FixedUpdate() {
        bool isGrounded = IsGrounded;
        anim.SetFloat("inputMagnitude", inputMag, 0.05f, Time.deltaTime);
        //inputDirection = Quaternion.AngleAxis(Camera.main.transform.rotation.eulerAngles.y, Vector3.up) * inputDirection;
        inputDirection.Normalize();

        //ySpeed += Physics.gravity.y * Time.deltaTime;
        ySpeed = rbody.velocity.y;
        if (isGrounded && cinput.enabled && cinput.Jump && !anim.GetBool("jumping"))
        {
            rbody.AddForce(jumpSpeed * Vector3.up, ForceMode.Impulse);
            anim.SetBool("jumping", true);
            anim.SetBool("grounded", false);
        } 
        else if (!isGrounded && ySpeed < 0 && anim.GetBool("jumping")) 
        {
            anim.SetBool("jumping", false);
            anim.SetBool("falling", true);
        } else if (ySpeed <= 0 && (anim.GetBool("falling") || anim.GetBool("jumping")) && isGrounded) {
            anim.SetBool("grounded", true);
            anim.SetBool("jumping",false);
            anim.SetBool("falling", false);
            rbody.velocity = Vector3.zero;
            rbody.angularVelocity = Vector3.zero;

        }
        if (cinput.enabled && inputDirection != Vector3.zero)
        {
            anim.SetBool("moving", true);
            //rbody.velocity = (input * directionMaxSpeed * Time.deltaTime * sprintMultiplier);
            Quaternion rotateTowards = Quaternion.LookRotation(inputDirection, Vector3.up);
            rotateTowards = Quaternion.LerpUnclamped(this.transform.rotation, rotateTowards, turnMaxSpeed);
            rbody.MoveRotation(rotateTowards); 
            //= Quaternion.RotateTowards(rbody.rotation, rotateTowards, turnMaxSpeed * Time.deltaTime);
        } else {
            anim.SetBool("moving", false);
        }
        if (!isGrounded) 
        {
            Vector3 jumpMoveVelocity = inputDirection * inputMag;
            jumpMoveVelocity = Vector3.LerpUnclamped(this.transform.position, this.transform.position + jumpMoveVelocity, jumpForwardSpeed * Time.deltaTime);
            rbody.MovePosition(jumpMoveVelocity);
            //rbody.MovePosition();
        }
    }

    void OnAnimatorMove() 
    {
        bool isGrounded = IsGrounded;
        Vector3 newPosition;

        if (isGrounded)
        {
            newPosition = anim.rootPosition;
            newPosition = Vector3.LerpUnclamped(this.transform.position, newPosition, directionMaxSpeed * Time.deltaTime);
            rbody.MovePosition(newPosition);
        }
        else
        {
            newPosition = new Vector3(anim.rootPosition.x, this.transform.position.y, anim.rootPosition.z);
        }
        
        
    }

    void OnCollisionEnter(Collision collision)
    {

        if (collision.transform.gameObject.tag == "ground")
        {  
            ++groundContactCount;
            if (anim.GetBool("jumping")) {
                anim.SetBool("jumping", false);
                anim.SetBool("falling",true);
            }
            anim.SetBool("falling", false);
            anim.SetBool("grounded", true);
        }
						
    }

    void OnCollisionExit(Collision collision)
    {
        if (collision.transform.gameObject.tag == "ground")
        {
            --groundContactCount;
        }
    }

    public void increaseTotalWinCount()
    {
        winCount += 1;
    }

    public void checkIfWin()
    {
        if (winCount >= 5)
        {
            CustomSceneManager.LoadNextScene("CreditsScreen");
        }
    }
}
