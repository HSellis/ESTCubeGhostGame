using UnityEngine;
using UnityEngine.InputSystem;

public class MouseClicker : MonoBehaviour
{
    [Header("Raycast Settings")]
    public Camera targetCamera;
    public LayerMask hitLayers = ~0;
    public float maxDistance = 100;

    private void Awake()
    {
        if (targetCamera == null)
        {
            targetCamera = GetComponent<Camera>();
        }
        if (targetCamera == null)
        {
            targetCamera = Camera.main;
        }
    }

    private void Update()
    {
        CheckMouseClick();
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
        Ray ray = targetCamera.ScreenPointToRay(screenPosition);
        if (Physics.Raycast(ray, out RaycastHit hit, maxDistance, hitLayers))
        {
            CubeFace cubeFace = hit.collider.GetComponent<CubeFace>();
            if (cubeFace != null)
            {
                cubeFace.Click();
            }
        }
    }
}
