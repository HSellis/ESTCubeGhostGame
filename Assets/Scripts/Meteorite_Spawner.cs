using UnityEngine;

public class Meteorite_Spawner : MonoBehaviour
{
    public GameObject meteoritePrefab;
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
            SpawnPosition = new Vector3(0f, Random.Range(-1f, 1f), 0f);
            SpawnMeteorite();
            spawnInterval = Time.deltaTime + spawnTimeAdded;
        }
    }
    void SpawnMeteorite()
    {
        Instantiate(meteoritePrefab, SpawnPosition, Quaternion.identity);
    }
}
