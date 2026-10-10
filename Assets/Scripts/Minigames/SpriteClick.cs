using UnityEngine;

public class SpriteClick : MonoBehaviour
{
    [SerializeField] public WireDragSpot wireStartPoint;
    [SerializeField] public GameObject wirePrefab;
    [SerializeField] private Camera targetCamera;

    private GameObject activeWire;

    private WireMiniGame minigame;

    private void Awake()
    {
        minigame = this.gameObject.GetComponentInParent<WireMiniGame>();
        if (targetCamera == null)
            targetCamera = FindAnyObjectByType<Camera>();
            Debug.Log(targetCamera.name);

        if (targetCamera == null)
            Debug.Log("No camera found");
    }

    private void OnMouseDown()
    {
        if (!wireStartPoint.canDraw) return;
        if (wireStartPoint == null || wirePrefab == null || targetCamera == null)
        {
            Debug.LogError("Missing wire start point, prefab, or camera!", this);
            return;
        }

        activeWire = Instantiate(wirePrefab, this.transform.parent);

        WireDrag wire = activeWire.GetComponent<WireDrag>();

        wire.SetColor(wireStartPoint.wireColor);
        minigame.SetCurrentWire(wire);

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

    private void OnMouseEnter()
    {
        WireDrag wire = minigame.GetCurrentWire();
        if (wire != null)
        {
            if (wire.startPoint != this.wireStartPoint)
            {
                wire.SetWireCompleted(wireStartPoint);
            }
        }
    }

    private void OnMouseUp()
    {
        if (activeWire != null && !activeWire.GetComponent<WireDrag>().isComplete)
        {
            Destroy(activeWire);
            activeWire = null;
        }
    }
}