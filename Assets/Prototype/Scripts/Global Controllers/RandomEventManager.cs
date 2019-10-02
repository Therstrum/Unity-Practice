using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomEventManager : MonoBehaviour
{
    public GameObject oldShip;
    public HullPoints hullPoint;
    bool oldShipEvent = false;
    public static bool eventInProgress = false;
    public static bool eventStart = false;
    static int eventType;

    // Start is called before the first frame update
    void Start()
    {
        WaveController Wave = GameObject.Find("_WaveController").GetComponent<WaveController>();
        Wave.NextWave();
    }

    // Update is called once per frame
    void Update()
    {
        if (eventStart)
        {
            /*
                if (eventType == 1)
                {
                    StartCoroutine("FullHealEvent");
                }
                else if (eventType == 2)
                {
                    StartCoroutine("NewItem");
                }
                else if (eventType == 3)
                {
                    StartCoroutine("TryForCredits");
                }
                eventStart = false;
            }
            */
            //when there are more options remove this last one
            
            StartCoroutine("FullHealEvent");
            eventStart = false;
        }
    }
    public static void RandomEvent()
    {
        int eventChance = Random.Range(0, 100);
        {
            if (eventChance >= 0 && eventChance <=33)
            {
                eventType = 1;
            }
            else if (eventChance > 33 && eventChance <= 66)
            {
                eventType = 2;
            }
            else
            {
                eventType = 3;
            }
        }
        eventStart = true;
    }
    IEnumerator FullHealEvent()
    {
        EventTextLower.text.enabled = true;
        int randomText = Random.Range(0, 100);
        if (randomText >= 0 && randomText <= 33)
        {
            EventTextLower.text.SetText("Another escape pod from the fleet? May your soul find rest from this torment. There's some useful supplies here for me.");
        }
        else if (randomText >33 && randomText <=66)
        {
            EventTextLower.text.SetText("I swear I've seen this exact ship before...");
        }
        else
        {
            EventTextLower.text.SetText("The losses of the past are stepping stones for the future");
        }

        //spawn some health packs
        int dropChance = Random.Range(1, 5);
        for (int i = 0; i < dropChance; i++)
        {
            //pick a random vector close to the origin
            Vector2 randomize = new Vector2(Random.Range(-0.5f, 0.5f), Random.Range(-0.5f, 0.5f));
            //spawn credits at the location where the enemy died and add some randomness to the location
            Instantiate(hullPoint, randomize, Quaternion.identity);
        }
        yield return new WaitForSeconds(5);
        EventTextLower.text.enabled = false;
        WaveController.waveCooldown = false;
    }
    /*IEnumerator NewItem()
    {

    }
    IEnumerator TryForCredits()
    {

    }
    */
}
