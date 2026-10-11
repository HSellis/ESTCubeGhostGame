using UnityEngine;
using TMPro;
public class GameTimer : MonoBehaviour
{
    public GameController gameController;
    public TMP_Text timerText;
    private float timer = 0f;
    private float maxTimer = 0f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        timer = gameController.GameDuration;
        maxTimer = timer;
    }

    // Update is called once per frame
    void Update()
    {
        timer = gameController.GameDuration;
        timerText.text = Mathf.Clamp(Mathf.Round(timer), 0, maxTimer).ToString();
    }
}
