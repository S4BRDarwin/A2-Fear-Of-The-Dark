using UnityEngine;
using System;

[CreateAssetMenu(fileName = "PlayerRespawnSO")]
public class PlayerRespawnSO : ScriptableObject
{
    public Action PlayerDeath;

    public void RaiseEventPlayerDeath()
    {
        PlayerDeath?.Invoke();
    }
}
