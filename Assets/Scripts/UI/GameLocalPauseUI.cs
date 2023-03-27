using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.UI;

public class GameLocalPauseUI : MonoBehaviour
{
    [SerializeField]
    private Button resumeButton;
    [SerializeField]
    private Button optionsButton;
    [SerializeField]
    private Button mainMenuButton;
    [SerializeField]
    private OptionsUI optionsUI;

    private void Awake()
    {
        resumeButton.onClick.AddListener(() =>
        {
            GameManager.Instance.TogglePauseGame();
        });
        optionsButton.onClick.AddListener(() =>
        {
            Hide();

            optionsUI.Show(Show);
        });
        mainMenuButton.onClick.AddListener(() =>
        {
            NetworkManager.Singleton.Shutdown();

            Loader.Load(Loader.Scene.MainMenuScene);
        });
    }

    private void Start()
    {
        GameManager.Instance.OnLocalGamePaused += GameManager_OnLocalGamePaused;
        GameManager.Instance.OnLocalGameUnpaused += GameManager_OnLocalGameUnpaused;

        Hide();
    }

    private void GameManager_OnLocalGamePaused(object sender, System.EventArgs args)
    {
        Show();
    }

    private void GameManager_OnLocalGameUnpaused(object sender, System.EventArgs args)
    {
        Hide();
    }

    private void Show()
    {
        gameObject.SetActive(true);

        resumeButton.Select();
    }

    private void Hide()
    {
        gameObject.SetActive(false);
    }
}
