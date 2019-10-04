using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveController : MonoBehaviour
{
   
    //Wave Variables
    public int currentWave;
    public int maxWave;
    public static int enemiesRemaining;
    public static float levelEndTimer = 5f;
    public static bool levelFinished = false;
    public static bool gameWon = false;

    //Spawning Variables
    public float maxEnemySpawn;
    public enemyShooterController enemyShooter;
    public EnemyFollower enemyFollower;
    public EnemyMultiShooter enemyMultiShooter;
    public EnemyLaser enemyLaser;
    public GameObject creditLow;
    static GameObject creditDrop;
    

    //Difficulty Variables
    public static int loot;
    public static int difficulty = 1;
    public static bool waveCooldown = false;
    

    //Attached GameObjects
    public GameObject levelCompleteText;

    // Start is called before the first frame update
    void Awake()
    {       

        PlayerStats.totalDifficulty += difficulty;
        creditDrop = creditLow;
        currentWave = 0;
        //TO DO: Get the difficulty modifier of the current scene and add it here. Add to maxWave.
        maxWave = 5;
        enemiesRemaining = 0;
    }

    // Update is called once per frame
    void Update()
    {
        //if theres no more enemies of this wave
        if (enemiesRemaining <= 0)
        {
            //if we haven't finished the last wave
            if (currentWave < maxWave && !waveCooldown)
            {
                //increment to the next wave and do the stuff for that 
                waveCooldown = true;
                NextWave();
            }
            //if the last wave was the last wave
            else if (currentWave == maxWave)
            {
                levelFinished = true;
                levelEndTimer -= Time.deltaTime;
                
                if (PlayerStats.levelsCompleted >= PlayerStats.levelsTotal)
                {
                    gameWon = true;
                }

                if (levelEndTimer <= 0)
                { 
                    levelFinished = false;
                    levelEndTimer = 5f;
                    PlayerStats.levelsCompleted++;
                    if (gameWon)
                    {
                        SceneController.GoToWinScreen();
                    }
                    else
                    {
                        SceneController.GoToShop();
                    }

                }
            }
        }

    }
    public void NextWave()
    {
        
        if (currentWave == 3)
        {
            currentWave++;
            RandomEventManager.RandomEvent();
        }
        else
        {
            StartCoroutine("FirstWaveCycleSpawn");
        }
    }
    IEnumerator LevelEvent()
    {
        Debug.Log("spawning random event");
        yield return new WaitForSeconds (4);
        Debug.Log("Going to next wave");
        NextWave();
    }

    IEnumerator FirstWaveCycleSpawn()
    {
        //make sure the event only runs once
        if (waveCooldown) 
        {
            yield return new WaitForSeconds(3);
            //Pick a random spawn number modified by difficulty
            maxEnemySpawn = Mathf.Floor(Random.Range(1 + (difficulty / 2 + difficulty), 3 + (2 * difficulty)));
            //for each enemy, choose a random one.
            for (int i = 0; i < maxEnemySpawn; i++)
            {
                float enemyTypeChance = Random.Range(0f, 100f);
                if (enemyTypeChance >= 0 && enemyTypeChance <= 55f)
                {
                    SpawnShooter();
                }
                else if (enemyTypeChance > 55 && enemyTypeChance <= 70)
                {
                    SpawnFollower();
                }
                else if (enemyTypeChance > 70 && enemyTypeChance <= 85)
                {
                    SpawnLaser();
                }
                else
                {
                    SpawnMulti();
                }
                //EXPLORE: Instead of instantiating random enemies per wave, make a set array of enemies. Run a random range and choose from the array which group of enemies to spawn

            }
            //increment the wave counter. This needs to happen after enemies are spawned so enemiesRemaining > 0
            currentWave++;
            //start the second part of the wave
            StartCoroutine("SecondWaveCycleSpawn");
        }
    }
    IEnumerator SecondWaveCycleSpawn()
    {
        yield return new WaitForSeconds(4);
        //set another random range for enemy type
        float spawnType = Random.Range(0, 100f);
        if (spawnType >=0 && spawnType <=50)
        {
            for (int i = 0; i <= 2*difficulty; i++)
            {
                SpawnShooter();
            }
        }
        else if (spawnType > 50 && spawnType <= 75)
        {
            for (int i = 0; i <= difficulty; i++)
            {
                SpawnFollower();
            }
        }
        else
        {
            for (int i = 0; i < difficulty-1; i++)
            {
                SpawnMulti();
                SpawnLaser();
            }
            
        }
        //tell the controller another wave can be spawned when no more enemies are alive
        waveCooldown = false;
    }

    public static void DropLoot(float difficultyMod, Vector2 enemyRB)
    {
        //Set the amount of loot that drops based on difficuly multipliers
        float lootMulti = (PlayerStats.lootMulti * difficulty * difficultyMod);
        float lootAmount = Random.Range(1 * lootMulti, 10 * lootMulti);
        //Roll 1-100 to see if loot drops
        float lootChance = Random.Range(1, 100);

        //Check if the roll meets a threshold that lowers based on difficulty;
        if (lootChance >= (50 - (5 * (difficulty + difficultyMod))))
        {
            for (int i = 1; i < lootAmount; i++)
            {

                //pick a random vector close to the origin
                Vector2 randomize = new Vector2(Random.Range(-0.5f, 0.5f), Random.Range(-0.5f, 0.5f));
                //spawn credits at the location where the enemy died and add some randomness to the location
                GameObject drop = Instantiate(creditDrop, (enemyRB) + randomize, Quaternion.identity);
            }
        }
        //dunno if this is necessary but reset the values afterwards
        lootAmount = 0f;
        lootChance = 0f;
    }

    void SpawnShooter()
    {
        Vector2 randomize = new Vector2(Random.Range(-9, 9), Random.Range(5.25f, 8));
        Instantiate(enemyShooter, randomize, Quaternion.identity);
    }
    void SpawnFollower()
    {
        Vector2 randomize = new Vector2(Random.Range(-9, 9), Random.Range(5.25f, 8));
        Instantiate(enemyFollower, randomize, Quaternion.identity);
    }
    void SpawnLaser()
    {
        Vector2 randomize = new Vector2(Random.Range(-9, 9), Random.Range(5.25f, 8));
        Instantiate(enemyLaser, randomize, Quaternion.identity);
    }
    void SpawnMulti()
    {
        Vector2 randomize = new Vector2(Random.Range(-9, 9), Random.Range(5.25f, 8));
        Instantiate(enemyMultiShooter, randomize, Quaternion.identity);
    }

}
