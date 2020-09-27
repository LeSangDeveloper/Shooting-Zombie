using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.Events;

public enum ScreenType 
{
    Launch,
    Playing,
    Pause
}

public class UIManager : MonoSingleton<UIManager>
{
    [SerializeField] List<GameScreen> screens;

#if UNITY_EDITOR
    private void OnValidate()
    {
        screens.Clear();
        foreach (GameScreen go in Resources.FindObjectsOfTypeAll(typeof(GameScreen)) as GameScreen[])
        {
            GameScreen cGO = go as GameScreen;
            if (cGO != null && !EditorUtility.IsPersistent(cGO.transform.root.gameObject) && !(go.hideFlags == HideFlags.NotEditable || go.hideFlags == HideFlags.HideAndDontSave))
                screens.Add(go);
        }
        //screens = new List<GameScreen>(GameObject.FindObjectsOfType(typeof(GameScreen)) as GameScreen[]);
    }
#endif

    void _ShowingScreen(ScreenType type)
    {
        LoopAllScreens(type, (screen) =>
        {
            screen.gameObject.SetActive(true);
            screen.Showing();
        }, (screen) => screen.Hiding());
    }
    public static void ShowingScreen(ScreenType type) => Instance?._ShowingScreen(type);

    void LoopAllScreens(ScreenType type, UnityAction<GameScreen> ifTrueCallback, UnityAction<GameScreen> ifFalseCallback)
    {
        for (int i = 0; i < screens.Count; ++i)
            if (screens[i].type == type)
                ifTrueCallback?.Invoke(screens[i]);
            else
                ifFalseCallback?.Invoke(screens[i]);
    }

}
