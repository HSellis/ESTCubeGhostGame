using UnityEngine;
using UnityEngine.InputSystem;

public class WireDrag : MonoBehaviour
{
    private Transform startPoint;
    private Camera targetCamera;
    private WireColor wireColor;
    public SpriteRenderer wireSprite;

    public void Initialize(Transform startPoint, Camera camera)
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
        if (startPoint == null || targetCamera == null)
            return;

        Vector3 start = startPoint.position;
        Vector3 end = GetMouseWorldPosition();

        Vector3 direction = end - start;

        transform.position = (start + end) / 2f;

        transform.localScale = new Vector3(
            direction.magnitude,
            transform.localScale.y,
            transform.localScale.z
        );

        Vector3 localDirection =
            startPoint.InverseTransformDirection(direction);

        float angle = Mathf.Atan2(
            localDirection.y,
            localDirection.x
        ) * Mathf.Rad2Deg;

        transform.rotation =
            startPoint.rotation *
            Quaternion.Euler(0f, 0f, angle);
    }

    private Vector3 GetMouseWorldPosition()
    {
        if (Mouse.current == null)
            return startPoint.position;

        Ray ray = targetCamera.ScreenPointToRay(
            Mouse.current.position.ReadValue()
        );

        Plane plane = new Plane(
            startPoint.forward,
            startPoint.position
        );

        if (plane.Raycast(ray, out float distance))
            return ray.GetPoint(distance);

        return startPoint.position;
    }
}