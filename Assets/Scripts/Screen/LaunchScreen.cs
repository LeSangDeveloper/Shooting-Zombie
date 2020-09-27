using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LaunchScreen : GameScreen
{
    [SerializeField]
    Button btnStart;
    protected override void Awake()
    {
        base.Awake();
        type = ScreenType.Launch;
        btnStart.onClick.AddListener(StartGame);
    }

    public void StartGame()
    {
        GameManager.Instance.StartGame();
    }
}
