using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameScreen : MonoBehaviour
{

    public ScreenType type;
    [SerializeField]
    protected CanvasGroup canvasGroup;

    protected virtual void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();
    }

    // Start is called before the first frame update
    public void Showing()
    {
        this.gameObject.SetActive(true);
        StartCoroutine(CR_FadeScreen(0.3f, 1.0f, () => {
            canvasGroup.blocksRaycasts = true;
        }));
    }

    // Update is called once per frame
    public void Hiding()
    {
        if (!gameObject.activeSelf)
            return;
        StartCoroutine(CR_FadeScreen(1.0f , 0.3f, () => {
            this.gameObject.SetActive(false);
        }));
    }

    IEnumerator CR_Callback(Action onComplete)
    {
        onComplete();
        yield return null;
    }

    IEnumerator CR_FadeScreen(float from, float to, Action onComplete)
    {
        if (from < to)
        {
            Debug.Log(from);
            canvasGroup.alpha = 1;
        }
        else
        {
            while (from > to)
            {
                this.canvasGroup.alpha = from;
                from -= 0.2f;
                yield return new WaitForSeconds(0.05f);
            }
        }
        
        yield return CR_Callback(onComplete);
    }



}
