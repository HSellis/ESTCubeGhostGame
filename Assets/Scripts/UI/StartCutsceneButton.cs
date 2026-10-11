using Unity.AppUI.UI;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Video;

public class StartCutsceneButton : MonoBehaviour
{
    public GameObject ButtonPanel;
    public VideoPlayer videoPlayer;
    public AudioSource audioSource;
    public AudioClip introCutsceneClip;

    private void ActivateVideo()
    {
        videoPlayer.Play();
        ButtonPanel.SetActive(false);
        audioSource.PlayOneShot(introCutsceneClip);
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
