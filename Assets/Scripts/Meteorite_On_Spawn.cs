using UnityEngine;

public class Meteorite_On_Spawn : MonoBehaviour
{
    public Rigidbody rb;
    public float RotationSpeed = 1f;
    public float MoveSpeed = 5f;
    public float scale = 20f;
    public Vector3 RotationAxis = new Vector3(0f, 0f, 1f);
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        scale = Random.Range(15f, 20f);
        MoveSpeed = Random.Range(2f, 7f);
        RotationSpeed = Random.Range(20f, 300f);
        transform.localScale = new Vector3(scale, scale, scale);
        RotationAxis = new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized;
        rb.linearVelocity = new Vector3(-1f, 0f, 0f) * MoveSpeed;
    }
    void Update()
    {
        //transform.position += new Vector3(1f, 0f, 0f) * MoveSpeed * Time.deltaTime;
        transform.Rotate(RotationAxis * RotationSpeed * Time.deltaTime);
    }
}
