using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseScreen : GameScreen
{
    [SerializeField]
    Button btnQuit;
    protected override void Awake()
    {
        base.Awake();
        type = ScreenType.Pause;
        btnQuit.onClick.AddListener(QuitGame);
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
