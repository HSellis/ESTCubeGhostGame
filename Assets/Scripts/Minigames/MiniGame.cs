using System.Threading.Tasks;
using UnityEngine;

public abstract class MiniGame : MonoBehaviour
{
    public Transform cameraFocusTransform;
    public CubeFace cubeFaceCover;
    public ESTCube estCube;
    protected GameController gameController;

    public ParticleSystem electricityEffect;
    public float electricityEffectCD = 4.0f;
    public bool isMiniGameActive = false;
    private bool isHit = false;

    private void OnEnable()
    {
        if (cubeFaceCover != null)
        {
            cubeFaceCover.Detached += OnCubeFaceDetach;
        }
    }

    private void OnDisable()
    {
        if (cubeFaceCover != null)
        {
            cubeFaceCover.Detached -= OnCubeFaceDetach;
        }
    }

    private void OnCubeFaceDetach()
    {
        isHit = true;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    protected virtual void Start()
    {
        gameController = GameObject.Find("Main Camera").GetComponent<GameController>();
        estCube = GameObject.Find("ESTCube").GetComponent<ESTCube>();
    }


    void playElectricityEffect(Vector3 position)
    {
        if (electricityEffect != null)
        {
            ParticleSystem effectInstance = Instantiate(electricityEffect, position, Quaternion.identity);
            effectInstance.Play();
            Destroy(effectInstance.gameObject, effectInstance.main.duration);
        }
    }
    // Update is called once per frame
    void Update()
    {
        if (isMiniGameActive | !isHit)
        {
            Debug.Log("isMiniGameActive: " + isMiniGameActive + ", isHit: " + isHit);
            return;
        }
        Debug.Log("Something was hit, playing electricity effect.");
        electricityEffectCD -= Time.deltaTime;
        if (electricityEffectCD <= 0)
        {
            playElectricityEffect(transform.position);
            electricityEffectCD = 4.0f;
        }
    }

    protected async void RepairComplete()
    {
        cubeFaceCover.Attach();
        ResetMinigame();
        await Task.Delay(1000);
        gameController.ZoomOutFromMiniGame();
        isHit = false;
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
