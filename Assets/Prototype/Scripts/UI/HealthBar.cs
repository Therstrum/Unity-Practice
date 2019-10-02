using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField]
    private float barFill;
    [SerializeField]
    private Image content;
    public Image background;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        UpdateHealth();
    }
    private void UpdateHealth()
    {
        content.fillAmount = Map(playerController.playerCurrentHealth,PlayerStats.playerMaxHealth); 
    }
    private float Map(float currentHP, float maxHP)
    {
        return currentHP / maxHP;
    }
}
