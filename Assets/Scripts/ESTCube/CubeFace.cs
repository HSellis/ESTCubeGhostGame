using System.Collections.Generic;
using System.Threading.Tasks;
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

    public async void OnHitMeteorite()
    {
        await Task.Delay(500);
        // TODO: instead disable isKinematic
        gameObject.SetActive(false);
    }
}
