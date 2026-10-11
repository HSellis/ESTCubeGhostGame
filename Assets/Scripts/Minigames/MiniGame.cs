using System.Threading.Tasks;
using UnityEngine;

public abstract class MiniGame : MonoBehaviour
{
    public Transform cameraFocusTransform;
    public CubeFace cubeFaceCover;
    public ESTCube estCube;
    protected GameController gameController;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    protected virtual void Start()
    {
        gameController = GameObject.Find("Main Camera").GetComponent<GameController>();
        estCube = GameObject.Find("ESTCube").GetComponent<ESTCube>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    protected async void RepairComplete()
    {
        cubeFaceCover.Attach();
        ResetMinigame();
        await Task.Delay(1000);
        gameController.ZoomOutFromMiniGame();
    }

    protected abstract void ResetMinigame();

    private void OnCollisionEnter(Collision collision)
    {
        Meteorite meteorite = collision.gameObject.GetComponent<Meteorite>();
        if (meteorite != null && !meteorite.HasCollided && cubeFaceCover.gameObject.activeSelf)
        {
            meteorite.OnCollisionWithESTCube();
            estCube.OnCollideWithAsteroid();
        }
    }
}
