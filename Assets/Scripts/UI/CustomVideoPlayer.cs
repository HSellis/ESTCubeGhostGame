using UnityEngine;
using UnityEngine.Video;

public class CustomVideoPlayer : MonoBehaviour
{
    public VideoPlayer player;
    public string FilePath;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        player.url = System.IO.Path.Combine(Application.streamingAssetsPath, FilePath); 
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
