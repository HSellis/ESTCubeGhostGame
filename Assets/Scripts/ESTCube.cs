using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class ESTCube : MonoBehaviour
{
    public Vector3 startAngularVelocity;

    private Rigidbody rb;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.angularVelocity = startAngularVelocity;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
