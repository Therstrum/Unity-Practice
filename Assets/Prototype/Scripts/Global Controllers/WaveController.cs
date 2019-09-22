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
    public static int difficulty;
    public static int loot;

    // Start is called before the first frame update
    void Start()
    {
        currentWave = 0;
        //TO DO: Get the difficulty modifier of the current scene and add it here. Add to maxWave.
        maxWave = 3;
        enemiesRemaining = 0;
        
    }

    // Update is called once per frame
    void Update()
    {
        //if theres no more enemies of this wave
        if (enemiesRemaining <= 0)
        {
            //if we haven't finished the last wave
            if (currentWave < maxWave)
            {
                //increment to the next wave and do the stuff for that wave
                currentWave++;
                NextWave();
            }
            //if the last wave was the last wave
            else if (currentWave == maxWave)
            {
                //go to the shop
                //TO DO: Instead of going directly to shop, run method to choose random scene and go there
                SceneController.GoToShop();
            }
        }

    }
    public void NextWave()
    {
        //TO DO: add difficulty modifer to maxEnemySpawn lower and upper bound
        maxEnemySpawn = Random.Range(2, 7);
        for (int i = 0; i < maxEnemySpawn; i++)
        {
            //TO DO: Instead of spawning one type of enemy, for each instantiate choose a random enemy
            //EXPLORE: Instead of instantiating random enemies per wave, make a set array of enemies. Run a random range and choose from the array which group of enemies to spawn
            //Randomize a random x on the screen, at the top of the screen.
            Vector2 randomize = new Vector2(Random.Range(-9, 9), 5);
            Instantiate(enemy, randomize, Quaternion.identity);
            Debug.Log("Wave: " + currentWave + "Enemies Spawned: " + i);
        } 
    }
    public static void DropLoot(GameObject credit, float difficultyMod, Vector2 enemyRB)
    {
        //TO DO: Add current scene's difficulty modifer to lootDropped and lootAmount
        float lootDropped = Random.Range(1 * PlayerStats.lootChance, 100);
        float lootAmount = Random.Range(5 * PlayerStats.lootMulti, 20 * PlayerStats.lootMulti);
        Debug.Log("loot chance:" + lootDropped + "-- loot amount: " + lootAmount);
        if (lootDropped >= 40)
        {
            for (int i = 1; i < lootAmount; i++)
            {
                int l = i;

                //pick a random vector close to the origin
                Vector2 randomize = new Vector2(Random.Range(-0.5f, 0.5f), Random.Range(-0.5f, 0.5f));
                //spawn credits at the location where the enemy died and add some randomness to the location
                GameObject drop = Instantiate(credit, (enemyRB) + randomize, Quaternion.identity);
            }
        }
        //dunno if this is necessary but reset the values afterwards
        lootAmount = 0f;
        lootDropped = 0f;
        return;

    }
}
