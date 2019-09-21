using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class ShopScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void BuyDamage()
    {
        if (PlayerStats.credits >= 50)
        {
            PlayerStats.playerDamage += 10;
            PlayerStats.credits -= 50;
        }
    }
    public void BuyFireRate()
    {
        if (PlayerStats.credits >= 50 && PlayerStats.playerShotRate >= .20f)
        {
            PlayerStats.playerShotRate -= .10f;
            PlayerStats.credits -= 50;
        }
    }
    public void BuySpeed()
    {
        if (PlayerStats.credits >= 50)
        {
            PlayerStats.playerSpeed += 2;
            PlayerStats.credits -= 50;
        }
    }
    public void LeaveShop()
    {
        SceneController.ChangeScene();
    }


}
