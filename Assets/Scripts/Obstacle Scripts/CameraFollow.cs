using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public GameObject player;
    public Vector3 offset;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        offset = new Vector3(0, 5, -5);
    }

    // Update is called once per frame
    void Update()
    {

        Vector3 targetPosition = player.transform.position + offset;
        targetPosition.x = 0;
        transform.position = targetPosition;

    }
}
