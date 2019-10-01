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
        difficultyPercent = (PlayerStats.totalDifficulty / (PlayerStats.levelsTotal*3))*100;
        text = GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        text.SetText("Difficulty Score........... " + difficultyPercent + "%");
    }
}
