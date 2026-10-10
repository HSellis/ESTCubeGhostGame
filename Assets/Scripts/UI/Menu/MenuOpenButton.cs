using UnityEngine;

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
}