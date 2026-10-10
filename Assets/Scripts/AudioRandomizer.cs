using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class AudioRandomizer : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip[] clips;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void Play()
    {
        if (clips == null || clips.Length == 0)
            return;

        audioSource.PlayOneShot(
            clips[Random.Range(0, clips.Length)]
        );
    }
}
