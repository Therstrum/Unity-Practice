using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class LevelSelector : MonoBehaviour
{
    public static string[] text = new string[3];
    public LevelGeneration[] levelChoice = new LevelGeneration[3];
    string[] adjective = { "Haunted", "Eldritch", "Unknown", "Colorful", "Nebulous" };
    string[] noun = {"Expanse", "Galaxy", "System", "Depths", "Asteroid Field"};

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < 3; i++)
        {
            int e = i + 1;

            string thisAdjective = adjective[(Random.Range(0, 5))];
            string thisNoun = noun[(Random.Range(0, 5))];
            string thisLevelName = ($"{thisAdjective} {thisNoun}");
            levelChoice[i] = new LevelGeneration(thisLevelName,e, e);
            text[i] = $"{thisLevelName} \n \n \n \n Difficulty: {e} \n \nLoot: {e}";
        }
        
    }
    public void LevelChoice1()
    {
        SceneController.ChangeScene(levelChoice[0].difficultyScale, levelChoice[0].lootScale);
    }
    public void LevelChoice2()
    {
        SceneController.ChangeScene(levelChoice[1].difficultyScale, levelChoice[1].lootScale);
    }
    public void LevelChoice3()
    {
        SceneController.ChangeScene(levelChoice[2].difficultyScale, levelChoice[2].lootScale);
    }

}
