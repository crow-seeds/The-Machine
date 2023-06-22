using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class batteryNode : diagnosticNode
{
    public int batteryLife;
    public List<GameObject> batteryIndicators;
    public float batteryDrainRate;
    public bool isFaulty = false;

    // Start is called before the first frame update
    void Start()
    {
        batteryLife = Random.Range((int)lowestSpawnedNumber, (int)highestNumber + 1);
        for (int i = batteryLife; i < 16; i++)
        {
            batteryIndicators[i].SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    public override void turnOn()
    {
        base.turnOn();
        StartCoroutine(change());
    }

    IEnumerator change()
    {
        yield return new WaitForSeconds(batteryDrainRate);
        if(batteryLife > lowestNumber)
        {
            batteryLife--;
            batteryIndicators[batteryLife].SetActive(false);
            if (batteryLife == 0 && !isFaulty)
            {
                system.sendErrorReport("D", IDchar, 0);
                isFaulty = true;
            }
        }
        StartCoroutine(change());
    }

    public void increaseBattery()
    {
        if(batteryLife < highestNumber)
        {
            batteryIndicators[batteryLife].SetActive(true);
            batteryLife++;

            if(batteryLife > 0 && isFaulty)
            {
                isFaulty = false;
                system.clearErrorReport("D", IDchar);
            }
        }
    }
}
