using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class ESTCube : MonoBehaviour
{
    public Vector3 startAngularVelocity;

    [Header("Cable Settings")]
    public Transform cableTransform;
    public float cableChangeSpeed = 5f;
    public float minCableLength = 0.1f;
    public float maxCableLength = 1.5f;
    public float velocityChangeSpeed = 5f;

    private Rigidbody rb;

    public static List<AudioSource> audioSources;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.angularVelocity = startAngularVelocity;
        audioSources = new List<AudioSource>(GetComponents<AudioSource>());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void MoveCable(float changeDirection)
    {   
        Vector3 scale = cableTransform.localScale;
        float cableLength = scale.z + changeDirection * cableChangeSpeed * Time.deltaTime;
        if (cableLength >= minCableLength && cableLength <= maxCableLength)
        {
            AudioSource cableAudioSource = audioSources[2];
            if (cableAudioSource != null && !cableAudioSource.isPlaying)
            {
                cableAudioSource.Play();
            }
            scale.z = cableLength;
            cableTransform.localScale = scale;

            AdjustAngularVelocity(changeDirection);
        }
    }

    private void AdjustAngularVelocity(float change)
    {
        float changeValue = velocityChangeSpeed + Time.deltaTime;

        if (change < 0)
        {
            rb.angularVelocity = new Vector3(rb.angularVelocity.x * changeValue, rb.angularVelocity.y * changeValue, rb.angularVelocity.z);
        }
        else
        {
            rb.angularVelocity = new Vector3(rb.angularVelocity.x / changeValue, rb.angularVelocity.y / changeValue, rb.angularVelocity.z);
        }
    }
    public static void PlayImpactSound()
    {   
        foreach (AudioSource audioSource in audioSources)
        {
            if (audioSource.isPlaying)
            {
                return;
            }
        }
        AudioSource randomAudioSource = audioSources[Random.Range(0, audioSources.Count-1)];
        if (randomAudioSource != null && !randomAudioSource.isPlaying)
        {
            randomAudioSource.Play();
        }
    }
}
