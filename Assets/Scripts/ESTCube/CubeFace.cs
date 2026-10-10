using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioRandomizer))]
public class CubeFace : MonoBehaviour
{
    public static List<AudioSource> audioSources;
    private AudioRandomizer audioRandomizer;
    private void Awake()
    {
        audioSources = new List<AudioSource>(GetComponents<AudioSource>());
        audioRandomizer = GetComponent<AudioRandomizer>();
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
        Debug.Log("meteorite collision");
        audioRandomizer.Play();
        gameObject.SetActive(false);
    }
}
