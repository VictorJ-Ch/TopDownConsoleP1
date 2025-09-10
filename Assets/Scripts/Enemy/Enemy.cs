using System;
using UnityEngine;

[Serializable]
public struct EnemySpawn
{
    public int Enemy;
    public float Time;
}

[Serializable]
public struct Wave
{
    public int wave;
    public EnemySpawn[] Enemies;
}

[Serializable]
public struct WaveData
{
    public Wave[] Waves;
}
