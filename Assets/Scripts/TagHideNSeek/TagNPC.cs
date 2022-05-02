using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class TagNPC : MonoBehaviour
{
    public GameObject player;
    public NavMeshAgent navAgent;
    public GameObject npc;
    public Rigidbody rb;
    public Animator anim;
    public Animator playerAnim;
    public TagState state;
    public GameObject[] npcRefArr;

    void Awake()
    {
        EventManager.StartListening("Reset", ResetState);
        EventManager.StartListening("Start", ChaseState);
        EventManager.StartListening("Pause", IdleState);
        EventManager.StartListening("Win", WinState);
        EventManager.StartListening("Lose", LoseState);
        EventManager.StartListening("DoneLoading", Start);
        npc = this.transform.GetChild(0).gameObject;
        rb = npc.GetComponent<Rigidbody>();
        anim = npc.GetComponent<Animator>();
        /*
        model = GameObject.FindGameObjectWithTag("npc");

        model.gameObject.transform.parent = this.gameObject.transform;
        model.gameObject.transform.position = this.gameObject.transform.position;
        model.gameObject.transform.rotation = Quaternion.identity;
        anim = model.GetComponent<Animator>();
        */
    }

    void Start()
    {
        npcRefArr = GameObject.FindGameObjectsWithTag("npc");
        foreach (GameObject item in npcRefArr) {
            if (item.name != "Preset" && (item.scene.name == "TagGame" || item.scene.name == "TagScene")) {
                if (npc.name == "Preset") {
                    Destroy(npc);
                }
                npc = item;
                rb = npc.GetComponent<Rigidbody>();
                anim = npc.GetComponent<Animator>();
                npc.transform.parent = this.gameObject.transform;
                npc.transform.localPosition = Vector3.zero;
                npc.transform.rotation = Quaternion.identity;
            }
        }
        //npcRef.transform.parent = this.gameObject.transform;
        player = GameObject.FindGameObjectWithTag("Player");
        playerAnim = player.GetComponent<Animator>();
        navAgent = GetComponent<NavMeshAgent>();
    }

    public void executeTagState()
    {
        switch (this.state)
        {
            case TagState.Reset:
                anim.SetBool("running", false);
                anim.SetBool("walking", false);
                this.transform.position = new Vector3(12, this.transform.position.y, 0);
                navAgent.Warp(new Vector3(12, this.transform.position.y, 0));
                (GameObject.FindObjectOfType<UpdateNavMeshSurface>() as UpdateNavMeshSurface).Bake();
                break;
            case TagState.Idle:
                anim.SetBool("running", false);
                anim.SetBool("walking", false);
                break;

            case TagState.Chase:
                setAgentDestination();
                Vector3 velocity = GetComponent<VelocityReporter>().velocity;
                anim.SetFloat("velx", velocity.x);
                anim.SetFloat("vely", velocity.y);
                anim.SetBool("running", true);
                anim.SetBool("walking", false);
                break;

            case TagState.Win:
                anim.SetBool("running", false);
                anim.SetBool("walking", false);
                break;
            
            case TagState.Lose:
                anim.SetBool("running", false);
                anim.SetBool("walking", false);
                break;

            default: break;
        }
    }

    public void changeTagState(TagState state)
    {
        // exit
        switch(this.state)
        {
            case TagState.Reset:
                navAgent.isStopped = false;
                break;
            case TagState.Idle:
                navAgent.isStopped = false;
                break;
            
            case TagState.Chase:
                navAgent.SetDestination(this.transform.position);
                navAgent.ResetPath();
                break;
            
            case TagState.Win:
                anim.SetBool("losing", false);
                playerAnim.SetBool("celebrate", false);
                break;

            case TagState.Lose:
                anim.SetBool("celebrate", false);
                playerAnim.SetBool("losing", false);
                break;
            
            default: break;
        }

        this.state = state;

        // enter
        switch(this.state)
        {
            case TagState.Reset:
                navAgent.isStopped = true;
                break;
            case TagState.Idle:
                navAgent.isStopped = true;
                break;

            case TagState.Chase:
                break;

            case TagState.Lose:
                anim.SetBool("celebrate", true);
                playerAnim.SetBool("losing", true);
                break;
            
            case TagState.Win:
                anim.SetBool("losing", true);
                playerAnim.SetBool("celebrate", true);
                break;

            default: break;
        }

        executeTagState();
    }

    void ResetState()
    {
        changeTagState(TagState.Reset);
    }
    
    void IdleState()
    {
        changeTagState(TagState.Idle);
    }

    void ChaseState()
    {
        changeTagState(TagState.Chase);
    }

    void WinState()
    {
        changeTagState(TagState.Win);
    }

    void LoseState()
    {
        changeTagState(TagState.Lose);
    }

    void setAgentDestination()
    {
        Vector3 target = player.transform.position;
        Vector3 velocity = player.GetComponent<VelocityReporter>().velocity;
        float dist = (target - navAgent.transform.position).magnitude;
        float futureTime = Mathf.Clamp(dist / navAgent.speed, 0, 0.9f);
        Vector3 futureTarget = target + futureTime * velocity;

        bool offMesh = NavMesh.Raycast(target, futureTarget, out var hit, NavMesh.AllAreas);
        if (offMesh)
        {
            futureTarget.x = hit.position.x;
            futureTarget.z = hit.position.z;
        }
        navAgent.SetDestination(futureTarget);
    }
}

public enum TagState
{
    Reset, Idle, Chase, Win, Lose
}
