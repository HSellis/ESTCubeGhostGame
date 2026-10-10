using UnityEngine;

public class MenuOpenButton : MonoBehaviour
{
    public GameObject Menu;
    private bool isMenuOpen;
    public void OpenMenu()
    {
        isMenuOpen = true;
        Menu.SetActive(isMenuOpen);
    }

    public void CloseMenu()
    {
        isMenuOpen=false;
        Menu.SetActive(isMenuOpen);
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        isMenuOpen = Menu.activeSelf;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
