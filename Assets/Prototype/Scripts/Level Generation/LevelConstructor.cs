using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class LevelGeneration
{
    public int difficultyScale;
    public int lootScale;
    public string levelName;

    public LevelGeneration(string name, int difficulty, int loot)
    {
        this.levelName = name;
        this.difficultyScale = difficulty;
        this.lootScale = loot;
    }
}
