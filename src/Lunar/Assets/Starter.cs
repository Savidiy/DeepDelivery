using System;
using UnityEngine;

public class Starter : MonoBehaviour
{
    private readonly Player _player = new();

    public int StartHp = 10;

    private void Start()
    {
        _player.HealthPoints = StartHp;
    }

    private void Update()
    {
        
    }
}

internal class Player
{
    public int HealthPoints;
}

[Serializable]
internal class RoomData
{
    public Color Background;
    public GateData UpGate;
    public GateData DownGate;
    public GateData RightGate;
    public GateData LeftGate;
}

[Serializable]
internal class GateData
{
}