using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class CutsceneManager : MonoBehaviour
{
    [SerializeField] public VideoPlayer videoPlayer;
    [SerializeField] public string sceneToLoad;
    private Keyboard keyboard;
    private void OnEnable()
    {
        videoPlayer.loopPointReached += OnVideoFinished;

    }

    private void OnDisable()
    {
        videoPlayer.loopPointReached -= OnVideoFinished;
    }

    public void OnVideoFinished(VideoPlayer vp)
    {
        SceneManager.LoadScene(sceneToLoad);
    }

    private void Update()
    {
        Keyboard keyboard = Keyboard.current;
        if (keyboard == null)
        {
            Debug.LogError("Keyboard is null!");
            return;
        }
        if (keyboard.escapeKey.isPressed)
        {
            OnVideoFinished(videoPlayer);
        }
    }

}