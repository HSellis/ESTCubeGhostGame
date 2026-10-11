using UnityEngine;
using UnityEngine.Audio;

public class ButtonSoundPlayer : MonoBehaviour
{

    public AudioSource audioSource;
    public AudioClip buttonSound;
    
    public void Play()
    {
        audioSource.generator = buttonSound;
        audioSource.pitch = Random.Range(0.9f,1.1f);
        audioSource.Play();
    }
}
