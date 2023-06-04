using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuGestureHandler : MonoBehaviour
{
    [field: SerializeField]
    private GameObject MainMenu { get; set; }

    [field: SerializeField]
    private GameObject YouLoseMenu { get; set; }

    [field: SerializeField]
    private PlayerBodyPositions PlayerBodyPositions { get; set; }

    public void HandleGestureDetected()
    {
        StartCoroutine(WaitForGestureAboveHand());
    }

    public void HandleGestureUndetected()
    {
        StopAllCoroutines();
    }

    IEnumerator WaitForGestureAboveHand()
    {
        while (PlayerBodyPositions.rightHand.position.y
               < PlayerBodyPositions.head.position.y)
        {
            yield return null;
        }

        ChangeMenuState();
    }

    private void ChangeMenuState()
    {
        if (MainMenu.activeSelf == true)
        {
            GameStateManager.Instance.StartGame();
        }
        else
        {
            if (YouLoseMenu.activeSelf == true)
            {
                GameStateManager.Instance.GoToMainMenu();
            }
        }
    }
}
