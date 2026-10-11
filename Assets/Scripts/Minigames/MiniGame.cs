using UnityEngine;

public abstract class MiniGame : MonoBehaviour
{
    public Transform cameraFocusTransform;
    public GameObject cubeFaceCover;
    public ESTCube estCube;

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

    private void OnCollisionEnter(Collision collision)
    {
        Meteorite meteorite = collision.gameObject.GetComponent<Meteorite>();
        if (meteorite != null && !meteorite.HasCollided && cubeFaceCover.activeSelf)
        {
            meteorite.OnCollisionWithESTCube();
            estCube.OnCollideWithAsteroid();
        }
    }
}
