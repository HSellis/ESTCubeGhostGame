using System.Collections;
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
    private bool isRepairing = false;

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

    protected virtual void Start()
    {
        gameController = GameObject.Find("Main Camera")
            .GetComponent<GameController>();

        estCube = GameObject.Find("ESTCube")
            .GetComponent<ESTCube>();
    }

    private void OnCubeFaceDetach()
    {
        isHit = true;
    }

    private void PlayElectricityEffect(Vector3 position)
    {
        if (electricityEffect != null)
        {
            ParticleSystem effectInstance = Instantiate(
                electricityEffect,
                position,
                Quaternion.identity
            );

            effectInstance.Play();

            Destroy(
                effectInstance.gameObject,
                effectInstance.main.duration
            );
        }
    }

    private void Update()
    {
        if (isMiniGameActive || !isHit)
            return;

        electricityEffectCD -= Time.deltaTime;

        if (electricityEffectCD <= 0)
        {
            PlayElectricityEffect(transform.position);
            electricityEffectCD = 4.0f;
        }
    }

    protected void RepairComplete()
    {
        if (isRepairing)
            return;

        isRepairing = true;

        cubeFaceCover.OnRepair();
        ResetMinigame();

        StartCoroutine(RepairCompleteRoutine());
    }

    private IEnumerator RepairCompleteRoutine()
    {
        yield return new WaitForSecondsRealtime(1f);

        gameController.ZoomOutFromMiniGame();

        isHit = false;
        isRepairing = false;
        isMiniGameActive = false;
    }

    protected abstract void ResetMinigame();

    private void OnCollisionEnter(Collision collision)
    {
        Meteorite meteorite = collision.gameObject.GetComponent<Meteorite>();

        if (meteorite != null &&
            !meteorite.HasCollided &&
            cubeFaceCover.gameObject.activeSelf)
        {
            meteorite.OnCollisionWithESTCube();
            estCube.OnCollideWithAsteroid();
        }
    }

    public void ZoomToMinigame()
    {
        isMiniGameActive = true;
        cubeFaceCover.HideRepairIndicator();
    }
}