using DG.Tweening;
using System;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;
using System.Collections;

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
    public int cutsceneWaitTimeMilliseconds = 4000;
    private bool isEnding = false;

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

    public void EndGame(bool isWin)
    {
        if (isEnding)
            return;

        isEnding = true;

        // Stop meteorite spawning
        NONBGMeteoriteSpawner.gameObject.SetActive(false);
        BGMeteoriteSpawner.gameObject.SetActive(false);

        StartCoroutine(EndGameRoutine(isWin));
    }

    private IEnumerator EndGameRoutine(bool isWin)
    {
        string sceneToLoad;
        float waitTime;

        if (isWin)
        {
            sceneToLoad = winCutsceneScene;
            waitTime = waitTimeBeforeWinCutsceneMs / 1000f;
        }
        else
        {
            sceneToLoad = loseCutsceneScene;
            waitTime = cutsceneWaitTimeMilliseconds / 1000f;
        }

        // Wait using real time, even if Time.timeScale is 0
        yield return new WaitForSecondsRealtime(waitTime);

        Debug.Log("Loading cutscene: " + sceneToLoad);

        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneToLoad);

        if (operation == null)
        {
            Debug.LogError("Failed to start loading scene: " + sceneToLoad);
        }
    }
}
