using System.Collections.Generic;
using UnityEngine;

public class CubeFace : MonoBehaviour
{
    public static List<AudioSource> audioSources;
    private void Awake()
    {
        audioSources = new List<AudioSource>(GetComponents<AudioSource>());
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
        PlayImpactSound();
        gameObject.SetActive(false);
    }

    private void PlayImpactSound()
    {
        foreach (AudioSource audioSource in audioSources)
        {
            if (audioSource.isPlaying)
            {
                return;
            }
        }
        AudioSource randomAudioSource = audioSources[Random.Range(0, audioSources.Count - 1)];
        if (randomAudioSource != null && !randomAudioSource.isPlaying)
        {
            randomAudioSource.Play();
        }
    }
}
