using UnityEngine;

public class Menu : MonoBehaviour
{
    public GameObject MainMenuButtons;
    public GameObject SettingsMenu;

    public GameObject CreditsMenu;

    public GameObject ControlsMenu;

    private GameObject[] closablePanels = new GameObject[4];

    private void Start()
    {
        closablePanels = new GameObject[]
        {
            MainMenuButtons,
            SettingsMenu,
            CreditsMenu,
            ControlsMenu
        };
    }
    private void OpenTab(GameObject panel) 
    {
        foreach (GameObject c in closablePanels) {
            if (c != panel) c.SetActive(false);
            else c.SetActive(true);
        }
    }
    public void OpenMainMenuButtons()
    {
        OpenTab(MainMenuButtons);
    }
    public void OpenSettings()
    {
        OpenTab(SettingsMenu);
    }
    public void OpenCredits()
    {
        OpenTab(CreditsMenu);
    }
    public void OpenControls()
    {
        OpenTab(ControlsMenu);
    }
    public void CloseGame()
    {
        #if UNITY_EDITOR
                // Exits Play Mode inside the Unity Editor
                UnityEditor.EditorApplication.isPlaying = false;
        #else
                    // Closes the actual built application
                Application.Quit();
        #endif
    }

}
