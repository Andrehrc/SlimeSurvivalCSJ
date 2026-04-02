using UnityEngine;

[CreateAssetMenu(fileName = "EnemySpawnData", menuName = "Enemies/New EnemySpawnData")]
public class EnemySpawnData : ScriptableObject
{
    public GameObject enemyPrefab;

    public float initialCooldown;
    public float minCooldown;
    public float cooldownDecreaseRate;

    public int enemiesPerSpawn;
    public int maxEnemiesPerSpawn;


}
