using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class switchNode : normalNode
{
    bool switchOn = false;
    public RectTransform switchIcon;
    public cycleNode cycNode;

    // Start is called before the first frame update
    void Start()
    {
        if(Random.Range(0f, 1f) > .5f)
        {
            switchOn = true;
            switchIcon.localRotation = Quaternion.Euler(0, 0, 0);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (isOn)
        {
            if(switchOn == (cycNode.cycleTime < 1))
            {
                if (isFaulty)
                {
                    system.clearErrorReport("A", IDchar);
                    isFaulty = false;
                }
            }
            else
            {
                if (!isFaulty)
                {
                    StartCoroutine(system.sendErrorReport("A", IDchar, 0));
                    isFaulty = true;
                }
            }
        }
    }

    public override void turnOn()
    {
        base.turnOn();
    }

    public void switchToggle()
    {
        if (isOn)
        {
            switchOn = !switchOn;
            if (switchOn)
            {
                switchIcon.localRotation = Quaternion.Euler(0, 0, 0);
            }
            else
            {
                switchIcon.localRotation = Quaternion.Euler(0, 0, 30);
            }
        }
    }
}
