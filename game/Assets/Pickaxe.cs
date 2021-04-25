using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Pickaxe : MonoBehaviour
{
    public PlayerMovement player;
    public UnityEvent PickStrike;

    public void OnStrikeWithPick()
    {
        PickStrike.Invoke();
        player.HitPick();
    }
}
