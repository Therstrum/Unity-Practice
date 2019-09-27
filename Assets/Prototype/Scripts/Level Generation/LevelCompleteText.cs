using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LevelCompleteText : MonoBehaviour
{
    float timer=WaveController.levelEndTimer;
    private TextMeshProUGUI text;
    // Start is called before the first frame update
    void Start()
    {
        text = GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        if (WaveController.levelFinished)
        {
            timer -= Time.deltaTime;
            text.SetText("Level Complete! " + Mathf.CeilToInt(timer));
        }
    }
}
