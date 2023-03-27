using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMultiplayerPauseUI : MonoBehaviour
{
    private void Start()
    {
        GameManager.Instance.OnMultiplayerGamePaused += GameManager_OnMultiplayerGamePaused;
        GameManager.Instance.OnMultiplayerGameUnpaused += GameManager_OnMultiplayerGameUnpaused;

        Hide();
    }

    private void GameManager_OnMultiplayerGamePaused(object sender, EventArgs args)
    {
        Show();
    }

    private void GameManager_OnMultiplayerGameUnpaused(object sender, EventArgs args)
    {
        Hide();
    }

    private void Show()
    {
        gameObject.SetActive(true);
    }

    private void Hide()
    {
        gameObject.SetActive(false);
    }
}
