using UnityEngine;

public class GroundTile : MonoBehaviour
{
    [SerializeField] GameObject obstaclePrefab;

    [SerializeField] GameObject tallObstaclePrefab;
    [SerializeField] float tallObstacleChance = 0.2f;

    GroundCreater groundCreater;

    public GameObject coinPrefab;

    // Start is called before the first frame update
    void Start()
    {
        groundCreater = GameObject.FindObjectOfType<GroundCreater>();
    }

    private void OnTriggerExit(Collider other)
    {
        groundCreater.CreateTile(true, true);
        // destory 2 seconds after object leaves trigger
        Destroy(gameObject, 2);
    }

    public void CreateObstacles()
    {
        // choose which obstacle to pick
        GameObject obstacleToCreate = obstaclePrefab;
        float random = Random.Range(0f, 1f);
        if (random < tallObstacleChance)
        {
            obstacleToCreate = tallObstaclePrefab;
        }
        int obstacleIndex = Random.Range(2, 5);
        Transform spawnPoint = transform.GetChild(obstacleIndex).transform;
        Instantiate(obstacleToCreate, spawnPoint.position, Quaternion.identity, transform);
    }

    public void CreateCoins()
    {
        int coinsToCreate = 10;

        for (int i = 0; i < coinsToCreate; i++)
        {
            GameObject temp = Instantiate(coinPrefab, transform);
            temp.transform.position = GetRandomPointInCollider(GetComponent<Collider>());
        }
    }

    Vector3 GetRandomPointInCollider (Collider collider)
    {
        Vector3 point = new Vector3(
            Random.Range(collider.bounds.min.x, collider.bounds.max.x),
            Random.Range(collider.bounds.min.y, collider.bounds.max.y),
            Random.Range(collider.bounds.min.z, collider.bounds.max.z));

        if (point != collider.ClosestPoint(point))
        {
            point = GetRandomPointInCollider(collider);
        }

        point.y = 1;
        return point;
    }
}
