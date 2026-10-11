using UnityEngine;
using System.Collections.Generic;
using UnityEngine.InputSystem;

public class TextGenerator : MiniGame
{
    public TextMesh GeneratedText;
    public TextMesh TypedText;

    private Keyboard keyboard;

    void OnEnable()
    {
        keyboard = Keyboard.current;

        if (keyboard != null)
        {
            keyboard.onTextInput += OnTextInput;
        }
    }

    void OnDisable()
    {
        if (keyboard != null)
        {
            keyboard.onTextInput -= OnTextInput;
        }
    }

    void Start()
    {
        SetRandomCode();
    }

    void SetRandomCode()
    {
        List<string> words = new List<string>()
        {
            "9w744d", "abd123", "xhz789", "hi2d73",
            "pqr456", "stu789", "vwx012",
            "abc678", "def901", "ghi234",
            "jkl567", "mno890", "pqr123", "stu456",
            "vwx789", "yz012d", "abc345", "def678"
        };

        GeneratedText.text = words[Random.Range(0, words.Count)];
        TypedText.text = "";
    }

    void OnTextInput(char character)
    {
        if (TypedText.text.Length >= 6)
        {
            return;
        }

        TypedText.text += character;

        if (TypedText.text.Length == 6)
        {
            if (TypedText.text == GeneratedText.text)
            {
                RepairComplete();
                
            }
            else
            {
                TypedText.text = "";
            }
        }
    }

    protected override void ResetMinigame()
    {
        
    }
}