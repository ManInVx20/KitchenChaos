using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameStartCountdownUI : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI countdownText;

    private Animator animator;
    private int numberPopupHash;
    private int prevCountdownValue;

    private void Awake()
    {
        animator = GetComponent<Animator>();

        numberPopupHash = Animator.StringToHash("NumberPopup");
    }

    private void Start()
    {
        GameManager.Instance.OnStateChanged += GameManager_OnStateChanged;

        Hide();
    }

    private void Update()
    {
        int countdownValue = Mathf.CeilToInt(GameManager.Instance.GetCountdownToStartTimer());
        countdownText.text = countdownValue.ToString();

        if (prevCountdownValue != countdownValue)
        {
            prevCountdownValue = countdownValue;
            animator.SetTrigger(numberPopupHash);
            SoundManager.Instance.PlayCountdownSound();
        }
    }

    private void GameManager_OnStateChanged(object sender, EventArgs args)
    {
        if (GameManager.Instance.IsCountdownToStart())
        {
            Show();
        }
        else
        {
            Hide();
        }
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
