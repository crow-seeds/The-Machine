using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class powerNode : normalNode
{
    public batteryNode batNode;
    public temperatureNode tempNode;
    public List<GameObject> circles;
    int stage;

    // Start is called before the first frame update
    void Start()
    {
        stage = Random.Range(0, 3);
        for(int i = 0; i <= stage; i++)
        {
            circles[i].SetActive(true);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void incrementStage()
    {
        if (isOn)
        {
            stage++;
            if (stage == 3)
            {
                batNode.increaseBattery();
                batNode.increaseBattery();
                tempNode.temperature += 20;
                circles[1].SetActive(false);
                circles[2].SetActive(false);
                stage = 0;
            }
            else
            {
                circles[stage].SetActive(true);
            }
        }
    }

    public override void turnOn()
    {
        base.turnOn();
    }
}
