using System.IO;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    public WaveData waveData;

    public GameObject enemyPrefab;

    [ContextMenu("Create Json")]
    public void CreateWavesData()
    {
        string wavesData = JsonUtility.ToJson(waveData, true);

        string path = Path.Combine(Application.persistentDataPath, "waves");
        File.WriteAllText(path, wavesData);

        Debug.Log("File created in: " + path);
    }

    [ContextMenu("Load Json")]
    public void LoadWaves()
    {
        string path = Path.Combine(Application.persistentDataPath, "waves");

        if (File.Exists(path))
        {
            string jsonWavesFile = File.ReadAllText(path);
            waveData = JsonUtility.FromJson<WaveData>(jsonWavesFile);
        }
    }
}
