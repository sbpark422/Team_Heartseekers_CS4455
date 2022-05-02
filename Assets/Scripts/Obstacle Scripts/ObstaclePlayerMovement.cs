using UnityEngine;
using UnityEngine.SceneManagement;

public class ObstaclePlayerMovement : MonoBehaviour
{
    GameObject player;
    [SerializeField] Rigidbody rb;
    [SerializeField] Animator anim;

    float horizontalInput;
    float horizontalMultiplier = 1.1f;

    public float speed = 11f;
    float minSpeed = 11f;
    public static float speedIncreasePerPoint = 0.07f;

    public bool isGrounded = true;
    float jumpForce = 35000;
    [SerializeField] LayerMask groundMask;

    public bool paused = false;
    bool alive = true;

    PlayerControlScript p;

    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        rb = player.GetComponent<Rigidbody>();
        anim = player.GetComponent<Animator>();
        rb.angularVelocity = Vector3.zero;
        groundMask = ~0; // set layers to everything
    }

    void OnDisable() {
        EventManager.StopListening("Reset", ResetObs);
        EventManager.StopListening("Start", StartObs);
        EventManager.StopListening("Win", WinObs);
        EventManager.StopListening("Lose", LoseObs);
        EventManager.StopListening("Pause", Pause);
    }

    void OnEnable() {
        EventManager.StartListening("Reset", ResetObs);
        EventManager.StartListening("Start", StartObs);
        EventManager.StartListening("Win", WinObs);
        EventManager.StartListening("Lose", LoseObs);
        EventManager.StartListening("Pause", Pause);
    }
    void Start() 
    {
        player.transform.position = GameObject.Find("PlayerStartPos").transform.position;
        player.transform.rotation = Quaternion.identity;
        // anim.SetBool("running", true);
        anim.SetBool("obstacleRun", true);  
    }

    private void FixedUpdate()
    {
        if (!alive) return;
        if (paused)
        {
            return;
        }
        Vector3 forwardMove = transform.forward * speed * Time.fixedDeltaTime;
        Vector3 horizontalMove = transform.right * horizontalInput * minSpeed * Time.fixedDeltaTime * horizontalMultiplier;
        rb.MovePosition(rb.position + forwardMove + horizontalMove);
        
    }

    private void Update()
    {
        float height = GetComponent<Collider>().bounds.size.y;
        isGrounded = Physics.Raycast(transform.position, Vector3.down, (height / 2) + 0.1f, groundMask);
        if (paused)
        {
            return;
        }
        horizontalInput = Input.GetAxisRaw("Horizontal");
        if (Mathf.Abs(player.transform.position.x) > 50) 
        {
            EventManager.TriggerEvent("Reset");
        }
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            Jump();
        }
        if (!isGrounded && rb.velocity.y < 0)
        {
            anim.SetBool("jumping", false);
            anim.SetBool("falling", true);
        } else if (isGrounded && anim.GetBool("falling")) 
        {
            anim.SetBool("falling", false);
            anim.SetBool("grounded", true);
        }

        if (transform.position.y < -5) 
        {
            Die();
        }
        try 
        {
            if (GameManager.inst.score >= 200)
            {
                EventManager.TriggerEvent("Win");
            }
        } catch
        {

        }

    }

    public void Die()
    {
        alive = false;
        EventManager.TriggerEvent("Lose");
    } 

    void Jump()
    {
        // if we are grounded
        float height = GetComponent<Collider>().bounds.size.y;
        isGrounded = Physics.Raycast(transform.position, Vector3.down, (height / 2) + 0.1f, groundMask);
        if (isGrounded)
        {
            anim.SetBool("grounded", false);
            anim.SetBool("jumping",true);
            rb.AddForce(Vector3.up * jumpForce);  
        }
    }

    void ResetObs()
    {
        alive = true;
        paused = true;
        //anim.SetBool("running",true);
        anim.SetBool("celebrate", false);
        //paused = true;
        anim.SetBool("obstacleRun",true);
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
        player.transform.position = new Vector3(0, 1, 2);
        player.transform.rotation = Quaternion.identity;
        speed = minSpeed;
    }

    void StartObs()
    {
        Time.timeScale = 1;
        paused = false;
        rb.angularVelocity = Vector3.zero;
    }

    void WinObs()
    {
        if (anim.GetBool("jumping") || anim.GetBool("falling")) {
            anim.SetBool("jumping", false);
            anim.SetBool("falling", false);
            anim.SetBool("grounded", true);
        }
        paused = true;
        alive = false;
        anim.SetBool("celebrate", true);
        anim.SetBool("obstacleRun", false);
        p.increaseTotalWinCount();
        p.checkIfWin();
        
    }

    void LoseObs()
    {
        paused = true;   
        if (anim.GetBool("jumping") || anim.GetBool("falling")) {
            anim.SetBool("jumping", false);
            anim.SetBool("falling", false);
            anim.SetBool("grounded", true);
        }
        anim.Play("Base Layer.Land", -1, 0);
        anim.SetBool("obstacleRun", false);  
    }

    void Pause()
    {
        paused = true;
        anim.SetBool("obstacleRun", false);
    }
}

