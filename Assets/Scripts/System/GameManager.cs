﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameState
{
    Start,
    Playing,
    Pause,
    Exit
}

public class GameManager : MonoSingleton<GameManager>
{
    [Header("Player Prefab")]
    public GameObject playerPrefab;
    
    [Header("Enemy Prefab")]
    public GameObject EnemyPrefab;

    public GameState gameState;



    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
        if (Input.GetKey(KeyCode.P))
        {
            Cursor.lockState = CursorLockMode.Confined;
            PauseGame();
        }

    }

    public void StartGame()
    {
        StartCoroutine(CR_RoundLoop());
    }

    IEnumerator CR_RoundLoop()
    {
        yield return StartCoroutine(CR_PrepareGame());
        yield return StartCoroutine(CR_RoundPlaying());
        yield return StartCoroutine(CR_PreFinish());
        yield return StartCoroutine(CR_Finish());
    }

    IEnumerator CR_PrepareGame()
    {
        Debug.Log(this.gameObject); 
        Instantiate(playerPrefab, new Vector3(0.0f, 3.25f, 0.0f), playerPrefab.transform.rotation);
        gameState = GameState.Playing;       
        yield return null;
    }

    IEnumerator CR_RoundPlaying()
    {
        UIManager.ShowingScreen(ScreenType.Playing);
        Cursor.visible = false;
        yield return new WaitForSeconds(5.0f);
        while (gameState == GameState.Playing)
        {
            Instantiate(EnemyPrefab, new Vector3(-12, -4.91f, -3.32f), Quaternion.identity);
            yield return new WaitForSeconds(5.0f);
        }
        yield return null;
    }


    IEnumerator CR_PreFinish()
    {

        yield return null;
    }

    IEnumerator CR_Finish()
    {

        yield return null;
    }

    void PauseGame()
    {
        Cursor.visible = true;
        Time.timeScale = 0f;
        UIManager.ShowingScreen(ScreenType.Pause);
    }


}
