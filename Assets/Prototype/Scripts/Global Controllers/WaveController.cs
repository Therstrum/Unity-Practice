using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveController : MonoBehaviour
{
    public int currentWave;
    public int maxWave;
    public static int enemiesRemaining;
    public int maxEnemySpawn;
    public enemyShooterController enemy;
    public GameObject credit;
    // Start is called before the first frame update
    void Start()
    {
        currentWave = 0;
        maxWave = 3;
        enemiesRemaining = 0;
        
    }

    // Update is called once per frame
    void Update()
    {
        if (enemiesRemaining <= 0)
        {
            if (currentWave < maxWave)
            {
                currentWave++;
                NextWave();
            }
            else if (currentWave == maxWave)
            {
                SceneController.GoToShop();
            }
        }

    }
    public void NextWave()
    {
        maxEnemySpawn = Random.Range(2, 7);
        for (int e = 0; e < maxEnemySpawn; e++)
        {
            Vector2 randomize = new Vector2(Random.Range(-9, 9), 5);
            Instantiate(enemy, randomize, Quaternion.identity);
            Debug.Log("Wave: " + currentWave + "Enemies Spawned: " + e);
        } 
    }
    public void DropLoot(float difficultyMod, Vector2 enemyRB)
    {
        float lootDropped = Random.Range(1 * PlayerStats.lootChance, 100);
        float lootAmount = Random.Range(5 * PlayerStats.lootMulti, 20 * PlayerStats.lootMulti);
        Debug.Log("loot chance:" + lootDropped + "-- loot amount: " + lootAmount);
        if (lootDropped >= 40)
        {
            for (int i = 1; i < lootAmount; i++)
            {
                int l = i;
                Vector2 randomize = new Vector2(Random.Range(-0.5f, 0.5f), Random.Range(-0.5f, 0.5f));
                GameObject drop = Instantiate(credit, (enemyRB) + randomize, Quaternion.identity);
            }
        }

        lootAmount = 0f;
        lootDropped = 0f;
        return;

    }
}
