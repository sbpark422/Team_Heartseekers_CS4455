using UnityEngine;
using UnityEngine.SceneManagement;

public class GroundCreater : MonoBehaviour
{
    [SerializeField] GameObject groundTile;
    public Vector3 nextGroundPoint;

    void Awake() 
    {
        EventManager.StartListening("Reset", TileCreator);
    }

    public void CreateTile(bool createCoins, bool createObstacles)
    {
        // what to spawn, where to spawn, rotation (identity means none)
        GameObject temp = Instantiate(groundTile, nextGroundPoint, Quaternion.identity);
        temp.transform.parent = GameObject.Find("TileParent").transform;
        nextGroundPoint = temp.transform.GetChild(1).transform.position;

        if (createCoins)
        {
            temp.GetComponent<GroundTile>().CreateCoins();
        }
        if (createObstacles)
        {
            temp.GetComponent<GroundTile>().CreateObstacles();
        }
    }

    void TileCreator()
    {
        GameObject[] tileRefs = GameObject.FindGameObjectsWithTag("obstacleTile");
        foreach (GameObject item in tileRefs) {
            Destroy(item);
        }
        nextGroundPoint = GameObject.Find("GroundStartPos").transform.position;
        Debug.Log("GroundCreater: nextGroundPoint");
        Debug.Log(nextGroundPoint);
        for (int i = 0; i < 15; i++)
        {
            if (i == 0)
            {
                CreateTile(false, false);
            }
            else if (i == 1)
            {
                CreateTile(true, false);
            }
            else
            {
                CreateTile(true, true);
            }
        }
    }
} 
