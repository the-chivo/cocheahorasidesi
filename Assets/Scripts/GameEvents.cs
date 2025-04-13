using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameEvents
{
    public static UnityEvent EndGame = new UnityEvent();
    public static UnityEvent ResetGame = new UnityEvent();
    public static UnityEvent CoinAdded = new UnityEvent();
}
