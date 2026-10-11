using UnityEngine;

public abstract class MiniGame : MonoBehaviour
{
    public Transform cameraFocusTransform;
    public GameObject cubeFaceCover;
    public ESTCube estCube;
    protected GameController gameController;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        gameController = GameObject.Find("Main Camera").GetComponent<GameController>();
        estCube = GameObject.Find("ESTCube").GetComponent<ESTCube>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    protected void RepairComplete()
    {
        cubeFaceCover.SetActive(true);
        ResetMinigame();
        gameController.ZoomOutFromMiniGame();
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
