using UnityEngine;

public class Meteorite_Spawner : MonoBehaviour
{
    public GameObject meteoritePrefab1;

    public GameObject meteoritePrefab2;
    public GameObject meteoritePrefab3;
    private float spawnInterval = 2f;
    public float spawnTimeAdded = 2f;
    private Vector3 SpawnPosition = new Vector3(0f, 0f, 0f);
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        spawnInterval -= Time.deltaTime;
        if (spawnInterval <= 0)
        {
            SpawnPosition = transform.position + new Vector3(0f, Random.Range(-1f, 1f), 0f);
            SpawnMeteorite();
            spawnInterval = Time.deltaTime + spawnTimeAdded;
        }
    }
    void SpawnMeteorite()
    {
        int randomNum = Random.Range(0, 3);
        if (randomNum == 0)
        {
            Instantiate(meteoritePrefab1, SpawnPosition, Quaternion.identity);
        }
        else if (randomNum == 1)
        {
            Instantiate(meteoritePrefab2, SpawnPosition, Quaternion.identity);
        }
        else
        {
            Instantiate(meteoritePrefab3, SpawnPosition, Quaternion.identity);
        }
    }
}
