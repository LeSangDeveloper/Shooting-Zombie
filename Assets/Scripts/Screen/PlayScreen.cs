using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayScreen : GameScreen
{
    [SerializeField]
    protected override void Awake()
    {
        base.Awake();
        type = ScreenType.Playing;
    }

}
