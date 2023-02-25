using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class EnemyManager : MonoBehaviour
{
    [field: SerializeField]
    private float SpawnRadius { get; set; }

    [field: SerializeField]
    private List<Transform> EnemyPrefabs { get; set; }

    [field: SerializeField]
    private float TimeBetweenSpawns { get; set; }

    private float TimeOfLastSpawn { get; set; }
    private bool IsSpawningBlocked { get; set; } = true;

    private void Start()
    {
        GameStateManager.Instance.OnInMainMenu += BlockSpawning;
        GameStateManager.Instance.OnGameLost += BlockSpawning;

        GameStateManager.Instance.OnPlaying += UnblockSpawning;

        GameStateManager.Instance.OnInMainMenu += DestroyAllChildren;
    }

    void Update()
    {
        TrySpawnEnemy();
    }

    private void BlockSpawning()
    {
        IsSpawningBlocked = true;
    }

    private void UnblockSpawning()
    {
        IsSpawningBlocked = false;
    }

    private void DestroyAllChildren()
    {
        for(int i = 0; i < transform.childCount; i++)
        {
            Destroy(transform.GetChild(i).gameObject);
        }
    }

    private void TrySpawnEnemy()
    {
        if (IsSpawningBlocked == false)
        {
            if (TimeOfLastSpawn + TimeBetweenSpawns < Time.time)
            {
                float angle = GetRandomAngle();
                Vector3 spawnPosition = GetPositionFromAngle(angle);
                int prefabIndex = GetRandomPrefabIndex();
                SpawnEnemy(EnemyPrefabs[prefabIndex], spawnPosition);

                TimeOfLastSpawn = Time.time;
            }
        }
    }

    private void SpawnEnemy(Transform EnemyPrefab, Vector3 position)
    {
        Instantiate(EnemyPrefab, position, Quaternion.identity, this.transform);
    }

    private int GetRandomPrefabIndex()
    {
        return Random.Range(0, EnemyPrefabs.Count);
    }

    private float GetRandomAngle()
    {
        return DegreesToRadian(Random.Range(0, 360));
    }

    private float DegreesToRadian(float degrees)
    {
        return Mathf.Deg2Rad * degrees;
    }

    private Vector3 GetPositionFromAngle(float angle)
    {
        Vector3 position = new Vector3();

        position.x = SpawnRadius * Mathf.Sin((float)angle);
        position.y = 0;
        position.z = SpawnRadius * Mathf.Cos((float)angle);

        return position;
    }

    private void OnDestroy()
    {
        GameStateManager.Instance.OnInMainMenu -= BlockSpawning;
        GameStateManager.Instance.OnGameLost -= BlockSpawning;

        GameStateManager.Instance.OnPlaying -= UnblockSpawning;

        GameStateManager.Instance.OnInMainMenu -= DestroyAllChildren;
    }
}
