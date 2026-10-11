using DG.Tweening;
using System;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public Camera camera;
    public float cameraMovementSpeed = 1.5f;
    public ESTCube estCube;

    public MeteoriteSpawner NONBGMeteoriteSpawner;
    public MeteoriteSpawner BGMeteoriteSpawner;

    public float GameDuration = 300f;
    public string winCutsceneScene = "WinCutscene";
    public int waitTimeBeforeWinCutsceneMs = 10000;
    public string loseCutsceneScene = "LoseCutscene";
    public int waitTimeBeforeLoseCutsceneMs = 4000;

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
        if (GameDuration == 0)
        {
            GameDuration = -1;
            EndGame(true);
            // Handle game over logic here
        }
        else if (GameDuration != -1)
        {
            GameDuration = Mathf.Clamp(GameDuration - Time.deltaTime, 0, GameDuration);
        }
        float changeDirection = 0f;
        Keyboard keyboard = Keyboard.current;
        if (keyboard == null)
        {
            Debug.LogError("Keyboard is null!");
            return;
        }
        if (keyboard.rightArrowKey.isPressed)
        {
            changeDirection += 1f;
        }
        if (keyboard.leftArrowKey.isPressed)
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
                ZoomOutFromMiniGame();
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

    public void ZoomToMiniGame(MiniGame minigame)
    {
        viewState = 1;
        
        DOTweenCustomUtils.DOMoveInTargetLocalSpace(camera.transform, minigame.cameraFocusTransform, Vector3.zero, cameraMovementSpeed);
        DOTweenCustomUtils.DORotateInTargetLocalSpace(camera.transform, minigame.cameraFocusTransform, cameraMovementSpeed).OnComplete(() =>
        {
            camera.transform.parent = minigame.transform;
            viewState = 2;
            minigame.ZoomToMinigame();
        });
    }

    public void ZoomOutFromMiniGame()
    {
        viewState = 1;
        camera.transform.parent = null;
        camera.transform.DOMove(cameraZoomedOutPosition, cameraMovementSpeed);
        camera.transform.DORotate(cameraZoomedOutRotation.eulerAngles, cameraMovementSpeed).OnComplete(() =>
        {
            viewState = 0;
        });
    }

    public async void EndGame(bool isWin)
    {
        // Stop meteorite spawning
        NONBGMeteoriteSpawner.gameObject.SetActive(false);
        BGMeteoriteSpawner.gameObject.SetActive(false);

        // Show cutscene
        if (isWin)
        {
            await Task.Delay(waitTimeBeforeLoseCutsceneMs);
            SceneManager.LoadScene(loseCutsceneScene);
        } else
        {
            await Task.Delay(waitTimeBeforeWinCutsceneMs);
            SceneManager.LoadScene(winCutsceneScene);
        }
    }
}
