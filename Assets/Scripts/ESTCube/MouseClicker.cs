using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Camera))]
public class MouseClicker : MonoBehaviour
{
    [Header("Raycast Settings")]
    public Camera camera;
    public LayerMask hitLayers = ~0;
    public float maxDistance = 100;
    public GameController gameController;

    private void Awake()
    {
        if (camera == null)
        {
            camera = GetComponent<Camera>();
        }
        if (camera == null)
        {
            camera = Camera.main;
        }
    }

    private void Update()
    {
        if (gameController.ViewState == 0)
        {
            CheckMouseClick();
        }
    }

    private void CheckMouseClick()
    {
        Vector2 mousePosition = Vector2.zero;
        bool isClicked = false;

        if (Mouse.current != null)
        {
            if (Mouse.current.leftButton.wasPressedThisFrame)
            {
                isClicked = true;
                mousePosition = Mouse.current.position.ReadValue();
            }
        }

        if (isClicked)
        {
            HandleClick(mousePosition);
        }
    }

    public void HandleClick(Vector2 screenPosition)
    {
        Ray ray = camera.ScreenPointToRay(screenPosition);
        if (Physics.Raycast(ray, out RaycastHit hit, maxDistance, hitLayers))
        {
            CubeFace cubeFace = hit.collider.GetComponent<CubeFace>();
            if (cubeFace != null)
            {
                gameController.ZoomToCubeFace(cubeFace);
            }
        }
    }
}
