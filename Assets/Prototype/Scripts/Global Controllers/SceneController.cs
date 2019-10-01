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
        SceneManager.LoadScene(2);
    }
    public static void ChangeScene(int difficulty, int loot)
    {
        //back to original scene
        WaveController.difficulty = difficulty;
        WaveController.loot = loot;
        SceneManager.LoadScene(0);
    }
    public static void GoToShop()
    {
        //go to shop scene
        SceneManager.LoadScene(1);
    }
    public static void GoToMainScreen()
        {
        SceneManager.LoadScene(3);
        }
    public static void StartGame()
    {
        SceneManager.LoadScene(0);
    }
    public static void GoToWinScreen()
    {
        SceneManager.LoadScene(4);
    }
    public static void Lose()
    {
        SceneManager.LoadScene(5);
    }
}
