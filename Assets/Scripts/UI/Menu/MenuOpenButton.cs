using UnityEngine;
using UnityEngine.InputSystem;

public class MenuOpenButton : MonoBehaviour
{
    [SerializeField] private Menu Menu;
    public static MenuOpenButton Instance { get; private set; }

    private void Start()
    {
        CloseMenu();
    }

    public void OpenMenu()
    {
        Menu.gameObject.SetActive(true);
        Menu.OpenMainMenuButtons();
        Time.timeScale = 0f;
    }

    public void CloseMenu()
    {
        Menu.gameObject.SetActive(false);
        Time.timeScale = 1f;
    }

    private void Update()
    {
        if (Keyboard.current.pKey.wasPressedThisFrame)
        {
            ToggleMenu();
        }
    }

    public void ToggleMenu()
    {
        if (Menu.gameObject.activeSelf)
        {
            CloseMenu();
        }
        else
        {
            OpenMenu();
        }
    }

}