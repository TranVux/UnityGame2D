using UnityEngine;

public class CloudGeneratorScript : MonoBehaviour
{

    [SerializeField] public GameObject cloud;
    [SerializeField] private float spawnInterval;
    [SerializeField] private GameObject endPoint;

    Vector3 startPosition;

    // Start is called before the first frame update
    void Start()
    {
        startPosition = transform.position;

        Invoke("AttempSpawn", spawnInterval);
    }

    public void SpawnCloud()
    {
        // clone cloud from prefab
        GameObject cloneCloud = Instantiate(cloud);

        //random start position for cloud clone
        float startPosY = UnityEngine.Random.Range(startPosition.y - 2f, startPosition.y + 1f);

        cloneCloud.transform.position = new Vector3(startPosition.x, startPosY, startPosition.z);

        //random scale for cloud clone
        float scale = UnityEngine.Random.Range(0.8f, 1f);
        cloneCloud.transform.localScale = new Vector2(scale, scale);

        //
        cloneCloud.GetComponent<CloudScript>().StartFloating(UnityEngine.Random.Range(0.5f, 2f), endPoint.transform.position.x);
    }

    void AttempSpawn()
    {
        SpawnCloud();
        Invoke("AttempSpawn", spawnInterval);

    }
}
