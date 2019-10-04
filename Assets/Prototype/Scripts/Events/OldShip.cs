using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OldShip : MonoBehaviour
{
    public static bool spawning;
    Vector3 location;
    Vector3 direction;
    // Start is called before the first frame update
    void Awake()
    {
        spawning = true;
        location.Set(0,0,1);
        direction = (location - transform.position).normalized;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(0, -1.25f * Time.deltaTime, 0);
    }

}
