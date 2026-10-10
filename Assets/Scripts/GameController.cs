using DG.Tweening;
using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameController : MonoBehaviour
{
    public Camera camera;
    public float cameraMovementSpeed = 1.5f;
    public ESTCube estCube;
    // 0 - zoomed out
    // 1 - zooming
    // 2 - zoomed in
    private int viewState = 0;
    private Vector3 cameraZoomedOutPosition;
    private Quaternion cameraZoomedOutRotation;

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

        cameraZoomedOutPosition = camera.transform.position;
        cameraZoomedOutRotation = camera.transform.rotation;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float changeDirection = 0f;
        Keyboard keyboard = Keyboard.current;
        if (keyboard == null)
        {
            Debug.LogError("Keyboard is null!");
            return;
        }
        if (keyboard.upArrowKey.isPressed)
        {
            changeDirection += 1f;
        }
        if (keyboard.downArrowKey.isPressed)
        {
            changeDirection -= 1f;
        }
        if (changeDirection != 0f && viewState == 0)
        {
            estCube.MoveCable(changeDirection);
        }

        if (keyboard.escapeKey.isPressed)
        {
            if (viewState == 2)
            {
                ZoomOutFromCube();
            }
        }
    }

    public int ViewState
    {
        get
        {
            return viewState;
        }
    }

    public void ZoomToCubeFace(CubeFace cubeFace)
    {
        viewState = 1;
        //cubeFace.Click();
        DOTweenCustomUtils.DOMoveInTargetLocalSpace(camera.transform, cubeFace.cameraFocusTransform, Vector3.zero, cameraMovementSpeed);
        DOTweenCustomUtils.DORotateInTargetLocalSpace(camera.transform, cubeFace.cameraFocusTransform, cameraMovementSpeed).OnComplete(() =>
        {
            camera.transform.parent = cubeFace.transform;
            viewState = 2;
        });
    }

    public void ZoomOutFromCube()
    {
        viewState = 1;
        camera.transform.parent = null;
        camera.transform.DOMove(cameraZoomedOutPosition, cameraMovementSpeed);
        camera.transform.DORotate(cameraZoomedOutRotation.eulerAngles, cameraMovementSpeed).OnComplete(() =>
        {
            viewState = 0;
        });
    }
}
