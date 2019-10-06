using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    public static void GoToLevelSelect()
    {
        SceneManager.LoadScene(3);
    }
    public static void ChangeScene(int difficulty, int loot)
    {
        //back to original scene
        WaveController.difficulty = difficulty;
        WaveController.loot = loot;
        SceneManager.LoadScene(1);
    }
    public static void GoToShop()
    {
        //go to shop scene
        SceneManager.LoadScene(2);
    }
    public static void GoToMainScreen()
    {
        SceneManager.LoadScene(0);
    }
    public static void StartGame()
    {
        ResetStats();
        SceneManager.LoadScene(1);
    }
    public static void GoToWinScreen()
    {
        SceneManager.LoadScene(4);
    }
    public static void Lose()
    {
        SceneManager.LoadScene(5);
    }
    private static void ResetStats()
    {
        PlayerStats.playerMaxHealth = 100;
        PlayerStats.playerSpeed = 7.0f;
        PlayerStats.playerDamage = 10;
        PlayerStats.playerShotSpeed = 700;
        PlayerStats.playerShotRate = 0.50f;
        PlayerStats.lootMulti = 1;
        PlayerStats.lootChance = 1;
        PlayerStats.credits = 25;
        PlayerStats.levelsCompleted = 0;
        PlayerStats.levelsTotal = 5;
        PlayerStats.totalDifficulty = 0;
        WaveController.levelEndTimer = 5f;
        WaveController.levelFinished = false;
        WaveController.gameWon = false;
        WaveController.difficulty = 1;
        playerController.playerCurrentHealth = 100;
    }
    public static void QuitGame()
    {
        Application.Quit();
    }
    public static void AboutPage()
    {
        SceneManager.LoadScene(6);
    }
}



