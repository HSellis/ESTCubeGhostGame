using System;
using UnityEditor.Rendering;
using UnityEngine;
using DG.Tweening;

public class GameController : MonoBehaviour
{
    public Camera camera;
    public float cameraMovementSpeed = 1.5f;
    private bool isZoomedIn = false;
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
        
    }

    public bool IsZoomedIn
    {
        get
        {
            return isZoomedIn;
        }
    }

    public void ZoomToCubeFace(CubeFace cubeFace)
    {
        isZoomedIn = true;
        //cubeFace.Click();
        DOTweenCustomUtils.DOMoveInTargetLocalSpace(camera.transform, cubeFace.cameraFocusTransform, Vector3.zero, cameraMovementSpeed);
        DOTweenCustomUtils.DORotateInTargetLocalSpace(camera.transform, cubeFace.cameraFocusTransform, cameraMovementSpeed).OnComplete(() =>
        {
            camera.transform.parent = cubeFace.transform;
        });
    }

    public void OnCompleteMinigame()
    {
        camera.transform.parent = null;
        camera.transform.DOMove(cameraZoomedOutPosition, cameraMovementSpeed);
        camera.transform.DORotate(cameraZoomedOutRotation.eulerAngles, cameraMovementSpeed).OnComplete(() =>
        {
            isZoomedIn = false;
        });
    }
}
