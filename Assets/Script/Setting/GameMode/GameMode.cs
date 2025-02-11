using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public abstract class GameMode : MonoBehaviour
{
    public GameType type;
    public abstract void StartGameMode();



}
