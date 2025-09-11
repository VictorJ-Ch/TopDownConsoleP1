using UnityEngine;
using System.IO;
using System.Collections.Generic;
using System.Collections;
using UnityEngine.Networking;
using System.Net;

public class GameManager : MonoBehaviour
{
    public bool canStartGame = false;

    [Header("Wave Data")]
    public WaveData waveData;
    public string url;
    private int currentWaveIndex = 0;

    [Header("Enemy Prefab")]
    public GameObject[] enemyPrefabs;

    [Header("Spawn Point")]
    public Transform spawnPoint;

    private List<GameObject> activeEnemies = new List<GameObject>();

    private List<ScheduledEnemy> scheduledEnemies = new List<ScheduledEnemy>();
    private float waveTimer = 0f;
    private bool waveFullySpawned = false;

    [SerializeField] private UIManager uIManager;

    private void Start()
    {
        Time.timeScale = 1;
        //LoadWaveData();
        StartCoroutine(JsonData());
        StartCoroutine(LateStartGame());
    }

    private void Update()
    {
        waveTimer += Time.deltaTime;

        for (int enemy = 0; enemy < scheduledEnemies.Count; enemy++)
        {
            var scheduled = scheduledEnemies[enemy];

            if (!scheduled.spawned && waveTimer >= scheduled.spawnTime)
            {
                if (scheduled.prefab != null)
                {
                    GameObject instance = Instantiate(scheduled.prefab, spawnPoint.position, Quaternion.identity);
                    instance.name = $"Enemy_Wave{currentWaveIndex + 1}_Time{scheduled.spawnTime}";
                    activeEnemies.Add(instance);
                }

                scheduled.spawned = true;
                scheduledEnemies[enemy] = scheduled;
            }
        }

        if (!waveFullySpawned && scheduledEnemies.TrueForAll(e => e.spawned))
        {
            waveFullySpawned = true;
        }

        activeEnemies.RemoveAll(e => e == null || !e.activeInHierarchy);

        if (waveFullySpawned && activeEnemies.Count == 0 && canStartGame) 
        {
            currentWaveIndex++;
            if (currentWaveIndex < waveData.Waves.Length)
            {
                StartWave(currentWaveIndex);
            }
            else
            {
                Debug.Log("Todas las oleadas completadas.");
                uIManager?.WinScreen();
            }
        }
    }

    private void LateUpdate()
    {
        
    }


    public IEnumerator LateStartGame()
    {
        yield return new WaitForSeconds(0.5f);
        canStartGame = true;
        StartWave(currentWaveIndex);
    }

    private void LoadWaveData()
    {
        string path = Path.Combine(Application.persistentDataPath, "waves");
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            waveData = JsonUtility.FromJson<WaveData>(json);
        }
    }

    private IEnumerator JsonData()
    {
        WWWForm form = new WWWForm();
        UnityWebRequest webRequest = UnityWebRequest.Get(url);
        yield return webRequest.SendWebRequest();

        if (webRequest.result.Equals(UnityWebRequest.Result.Success))
        {
            waveData = JsonUtility.FromJson<WaveData>(webRequest.downloadHandler.text);
        }
    }

    public void StartWave(int waveIndex)
    {
        uIManager?.UpdateWaveCounter(waveIndex + 1);
        waveFullySpawned = false;

        Wave wave = waveData.Waves[waveIndex];
        Debug.Log($"Iniciando Wave {wave.wave}");

        scheduledEnemies.Clear();
        activeEnemies.Clear();
        waveTimer = 0f;

        foreach (var enemy in wave.Enemies)
        {
            int prefabIndex = Mathf.Clamp(enemy.Enemy - 1, 0, enemyPrefabs.Length - 1);
            GameObject prefab = enemyPrefabs[prefabIndex];

            scheduledEnemies.Add(new ScheduledEnemy
            {
                prefab = prefab,
                spawnTime = enemy.Time,
                spawned = false
            });
        }
    }
}

public class ScheduledEnemy
    {
        public GameObject prefab;
        public float spawnTime;
        public bool spawned;
    }