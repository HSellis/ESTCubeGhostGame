using UnityEngine;

public class CubeFace : MonoBehaviour
{

    private void Awake()
    {
        
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
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
        ESTCube.PlayImpactSound();
        gameObject.SetActive(false);
    }
}
