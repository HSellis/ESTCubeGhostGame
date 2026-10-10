using UnityEngine;
using UnityEngine.InputSystem;

public class WireDrag : MonoBehaviour
{
    public WireDragSpot startPoint;
    private Camera targetCamera;
    private WireColor wireColor;
    public SpriteRenderer wireSprite;

    public bool isComplete = false;

    public void Initialize(WireDragSpot startPoint, Camera camera)
    {
        this.startPoint = startPoint;
        this.targetCamera = camera;
    }

    public void SetColor(WireColor color)
    {
        wireColor = color;
        switch (color) {
            case WireColor.Red:
                wireSprite.color = Color.red;
                break;
            case WireColor.Green:
                wireSprite.color = Color.green;
                break; 
            case WireColor.Blue:
                wireSprite.color = Color.blue;
                break;
            case WireColor.Yellow:
                wireSprite.color = Color.yellow;
                break;
            case WireColor.White:
                wireSprite.color = Color.white;
                break;
        
        }

    }

    private void Update()
    {
        if (!isComplete) { 
            if (startPoint == null || targetCamera == null)
                return;

            Vector3 start = startPoint.transform.position;
            Vector3 end = GetMouseWorldPosition();

            setWireTransform(start, end);
        }
    }
    private void setWireTransform(Vector3 start, Vector3 end)
    {
        Vector3 direction = end - start;

        transform.position = (start + end) / 2f;

        transform.localScale = new Vector3(
            direction.magnitude,
            transform.localScale.y,
            transform.localScale.z
        );

        Vector3 localDirection =
            startPoint.transform.InverseTransformDirection(direction);

        float angle = Mathf.Atan2(
            localDirection.y,
            localDirection.x
        ) * Mathf.Rad2Deg;

        transform.rotation =
            startPoint.transform.rotation *
            Quaternion.Euler(0f, 0f, angle);
    }

    public void SetWireCompleted(WireDragSpot EndPoint)
    {
        Vector3 start = startPoint.transform.position;
        Vector3 end = EndPoint.transform.position;
        
        isComplete = true;
        startPoint.canDraw = false;
        EndPoint.canDraw = false;
        setWireTransform(start, end);
        WireMiniGame minigame = this.gameObject.GetComponentInParent<WireMiniGame>();
        minigame.SetCurrentWire(null);
        minigame.CheckComplete();
    }

    private Vector3 GetMouseWorldPosition()
    {
        if (Mouse.current == null)
            return startPoint.transform.position;

        Ray ray = targetCamera.ScreenPointToRay(
            Mouse.current.position.ReadValue()
        );

        Plane plane = new Plane(
            startPoint.transform.forward,
            startPoint.transform.position
        );  

        if (plane.Raycast(ray, out float distance))
            return ray.GetPoint(distance);

        return startPoint.transform.position;
    }
}