using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private float spawnInterval = 1f;
    [SerializeField] private GameObject spawnPositionLeft;
    [SerializeField] private GameObject spawnPositionRight;
    [SerializeField] private GameObject[] enemyPrefabs;

    private SpriteRenderer[] spriteRenderers;

    private void Awake()
    {
        spriteRenderers = new SpriteRenderer[enemyPrefabs.Length];
        for (int i = 0; i < enemyPrefabs.Length; i++)
        {
            spriteRenderers[i] = enemyPrefabs[i].GetComponent<SpriteRenderer>();
        }
    }
    void Start()
    {
        InvokeRepeating("SpawnEnemy", 0f, spawnInterval);
    }

    void SpawnEnemy()
    {
        bool spawnOnLeft = Random.Range(0, 2) == 0;
        int enemyIndex = Random.Range(0, enemyPrefabs.Length);

        spriteRenderers[enemyIndex].flipX = spawnOnLeft;

        Vector3 spawnPosition = spawnOnLeft ? spawnPositionLeft.transform.position : spawnPositionRight.transform.position;
        Instantiate(enemyPrefabs[enemyIndex], spawnPosition, Quaternion.identity);
    }

    public void Pause()
    {
        CancelInvoke("SpawnEnemy");
    }
}
