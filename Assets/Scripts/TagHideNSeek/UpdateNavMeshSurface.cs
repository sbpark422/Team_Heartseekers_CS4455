using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class UpdateNavMeshSurface : MonoBehaviour
{
    public GameObject npc;

    void Start()
    {
        Bake();
    }

    void Update()
    {
        if (Vector3.Distance(this.transform.position, npc.transform.position) > 5)
        {
            Bake();
        }
    }

    public void Bake()
    {
        this.transform.position = npc.transform.position;
        this.GetComponent<NavMeshSurface>().BuildNavMesh();
    }
}
