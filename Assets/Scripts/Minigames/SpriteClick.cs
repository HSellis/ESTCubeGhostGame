using UnityEngine;

public class SpriteClick : MonoBehaviour
{
    [SerializeField] public Transform wireStartPoint;
    [SerializeField] public GameObject wirePrefab;
    [SerializeField] private Camera targetCamera;

    private GameObject activeWire;

    private void Awake()
    {
        if (targetCamera == null)
            targetCamera = FindAnyObjectByType<Camera>();
            Debug.Log(targetCamera.name);

        if (targetCamera == null)
            Debug.Log("No camera found");
    }

    private void OnMouseDown()
    {
        if (wireStartPoint == null || wirePrefab == null || targetCamera == null)
        {
            Debug.LogError("Missing wire start point, prefab, or camera!", this);
            return;
        }

        activeWire = Instantiate(wirePrefab);

        WireDrag wire = activeWire.GetComponent<WireDrag>();

        if (wire != null)
        {
            wire.Initialize(wireStartPoint, targetCamera);
        }
        else
        {
            Debug.LogError("WirePrefab is missing the WireDrag component!");
            Destroy(activeWire);
            activeWire = null;
        }
    }


    private void OnMouseUp()
    {
        if (activeWire != null)
        {
            Destroy(activeWire);
            activeWire = null;
        }
    }
}