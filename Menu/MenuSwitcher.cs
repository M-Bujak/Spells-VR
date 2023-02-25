using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuSwitcher : MonoBehaviour
{
    [field: SerializeField]
    private GameObject MainMenu { get; set; }

    [field: SerializeField]
    private GameObject YouLoseMenu { get; set; }

    void Start()
    {
        GameStateManager.Instance.OnInMainMenu += EnableMainMenu;

        GameStateManager.Instance.OnPlaying += DisableMainMenu;

        GameStateManager.Instance.OnGameLost += EnableYouLoseMenu;
    }

    private void EnableMainMenu()
    {
        MainMenu.SetActive(true);
        YouLoseMenu.SetActive(false);
    }

    private void EnableYouLoseMenu()
    {
        MainMenu.SetActive(false);
        YouLoseMenu.SetActive(true);
    }

    private void DisableMainMenu()
    {
        MainMenu.SetActive(false);
    }

    private void OnDestroy()
    {
        GameStateManager.Instance.OnInMainMenu -= EnableMainMenu;
        GameStateManager.Instance.OnPlaying -= DisableMainMenu;
        GameStateManager.Instance.OnGameLost -= EnableYouLoseMenu;
    }
}
