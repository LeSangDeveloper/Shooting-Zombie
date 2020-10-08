using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LaunchScreen : GameScreen
{
    [SerializeField]
    Button btnStart;

    [SerializeField]
    Button btnQuit;
    protected override void Awake()
    {
        base.Awake();
        type = ScreenType.Launch;
        btnStart.onClick.AddListener(StartGame);
        btnQuit.onClick.AddListener(QuitGame);
    }

    public void StartGame()
    {
        GameManager.Instance.StartGame();
    }

    public void QuitGame()
    {
    #if UNITY_STANDALONE
        //Quit the application
        Application.Quit();
    #endif
 
        //If we are running in the editor
    #if UNITY_EDITOR
        //Stop playing the scene
        UnityEditor.EditorApplication.isPlaying = false;
    #endif
    }
}
