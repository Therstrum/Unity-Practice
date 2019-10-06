using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameWinScreen : MonoBehaviour
{
    float difficultyPercent;
    TextMeshProUGUI text;
    // Start is called before the first frame update
    void Start()
    {

        text = GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        float difficulty = PlayerStats.totalDifficulty;
        float complete = PlayerStats.levelsCompleted;
        int maxDiff = (PlayerStats.levelsCompleted * 3) - 2;
        difficultyPercent = Mathf.Floor(((difficulty / maxDiff)*100));
        //text.SetText(1/100 + "\n Difficulty Score........... " + difficultyPercent + "%" + "\n" + "Total diff: " + PlayerStats.totalDifficulty +  "\n" + "Max possible: " + PlayerStats.levelsCompleted);
        text.SetText("You Win! \n \n \n Levels Completed.........." + PlayerStats.levelsCompleted + "\n \n Total Difficulty................." + PlayerStats.totalDifficulty + "\n \n Maximum Difficulty........." + maxDiff + "\n \n Pilot Score................" + difficultyPercent + "%");

    }
}
