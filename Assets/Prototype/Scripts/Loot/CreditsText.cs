using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CreditsText : MonoBehaviour
{
    private TextMeshProUGUI text;
    public int currentCredits;
    // Start is called before the first frame update
    void Start()
    {
        text = gameObject.GetComponent<TextMeshProUGUI>();
        currentCredits = PlayerStats.credits;
    }

    // Update is called once per frame
    void Update()
    {
        currentCredits = PlayerStats.credits;
        text.SetText($"{currentCredits}");
    }
}
