using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Rigidbody))]
public class ESTCube : MonoBehaviour
{
    public Vector3 startAngularVelocity;

    [Header("Cable Settings")]
    public Transform cableTransform;
    public float cableChangeSpeed = 5f;
    public float minTipDistance = 0.1f;
    public float maxTipDistance = 1.5f;
    public float velocityChangeSpeed = 5f;
    public float maxAngularVelocity = 4f;

    public GameObject cubetip;
    public AudioSource cableAudioSource;
    public AudioSource generalAudioSource;
    public string winCutsceneScene = "WinCutscene";
    public string loseCutsceneScene = "LoseCutscene";
    public int cutsceneWaitTimeMilliseconds = 4000;

    private Rigidbody rb;
    private LineRenderer lineRenderer;
    private CubeFace[] cubeFaces;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.angularVelocity = startAngularVelocity;
        lineRenderer = cubetip.GetComponent<LineRenderer>();
        lineRenderer.SetPosition(0, transform.position);
        lineRenderer.SetPosition(1, cubetip.transform.position);
        cubeFaces = GetComponentsInChildren<CubeFace>();
    }

    // Update is called once per frame
    void Update()
    {
        lineRenderer.SetPosition(0, transform.position);
        lineRenderer.SetPosition(1, cubetip.transform.position);
    }

    public void MoveCable(float changeDirection)
    {
        Vector3 fromCenterToTip = cubetip.transform.position - transform.position;
        Vector3 tipDirection = fromCenterToTip.sqrMagnitude > Mathf.Epsilon
            ? fromCenterToTip.normalized
            : transform.forward;
        float currentTipDistance = fromCenterToTip.magnitude;
        float newTipDistance = Mathf.Clamp(
            currentTipDistance + changeDirection * cableChangeSpeed * Time.deltaTime,
            minTipDistance,
            maxTipDistance);

        if (!Mathf.Approximately(currentTipDistance, newTipDistance))
        {
            if (cableAudioSource != null && !cableAudioSource.isPlaying)
            {
                cableAudioSource.Play();
            }
            cubetip.transform.position = transform.position + tipDirection * newTipDistance;

            AdjustAngularVelocity(changeDirection);
        }
    }

    private void AdjustAngularVelocity(float change)
    {
        float changeValue = velocityChangeSpeed + Time.deltaTime;
        if (change < 0)
        {
            if (rb.angularVelocity.magnitude < maxAngularVelocity)
            {
                rb.angularVelocity = new Vector3(rb.angularVelocity.x * changeValue, rb.angularVelocity.y * changeValue, rb.angularVelocity.z);
            }
        }
        else
        {
            rb.angularVelocity = new Vector3(rb.angularVelocity.x / changeValue, rb.angularVelocity.y / changeValue, rb.angularVelocity.z);
        }
    }

    public async Task OnCollideWithAsteroid()
    {
        generalAudioSource.Play();
        foreach (CubeFace cubeFace in cubeFaces)
        {
            cubeFace.OnHitAsteroid();
        }

        await Task.Delay(cutsceneWaitTimeMilliseconds);
        SceneManager.LoadScene(loseCutsceneScene);
    }
}
