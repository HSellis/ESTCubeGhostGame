using UnityEngine;

public abstract class MiniGame : MonoBehaviour
{
    public Transform cameraFocusTransform;
    public GameObject cubeFaceCover;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    protected void RepairComplete()
    {
        cubeFaceCover.SetActive(true);
        ResetMinigame();
    }

    protected abstract void ResetMinigame();
}
