using UnityEngine;

public class CubeFace : MonoBehaviour
{
    public Transform cameraFocusTransform;
    private MeshRenderer meshRenderer;
    private Collider collider;

    private void Awake()
    {
        meshRenderer = GetComponent<MeshRenderer>();
        collider = GetComponent<Collider>();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void RepairComplete()
    {
        meshRenderer.enabled = true;
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("collided with " + collision.gameObject.name);
        Meteorite meteorite = collision.gameObject.GetComponent<Meteorite>();
        if (meteorite != null)
        {
            OnHitMeteorite();
        }
    }

    private void OnHitMeteorite()
    {
        meshRenderer.enabled = false;
    }

    
}
