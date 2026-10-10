using UnityEngine;
using UnityEngine.UI;

public class Settings : MonoBehaviour
{
    [SerializeField] private Slider volumeSlider;
    [SerializeField] private Toggle muteToggle;

    private float volume = 1f;
    private bool isMuted = false;

    private void Start()
    {
        volumeSlider.value = volume;
        muteToggle.isOn = isMuted;

        volumeSlider.onValueChanged.AddListener(SetVolume);
        muteToggle.onValueChanged.AddListener(SetMute);

        ApplyAudioSettings();
    }

    private void SetVolume(float value)
    {
        volume = value*2;

        // If we're muted, don't change the actual volume yet.
        if (!isMuted)
        {
            ApplyAudioSettings();
        }
    }

    private void SetMute(bool muted)
    {
        isMuted = muted;
        ApplyAudioSettings();
    }

    private void ApplyAudioSettings()
    {
        AudioListener.volume = isMuted ? 0f : volume;
    }

    private void OnDestroy()
    {
        volumeSlider.onValueChanged.RemoveListener(SetVolume);
        muteToggle.onValueChanged.RemoveListener(SetMute);
    }
}