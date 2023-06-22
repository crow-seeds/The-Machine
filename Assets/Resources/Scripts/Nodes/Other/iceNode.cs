using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class iceNode : normalNode
{
    bool hasIce = false;
    public GameObject iceIndicator;
    public GameObject noIceIndicator;
    public temperatureNode tempNode;


    // Start is called before the first frame update
    void Start()
    {
        if (Random.Range(0f, 1f) < .5f)
        {
            hasIce = true;
            iceIndicator.SetActive(true);
            noIceIndicator.SetActive(false);
        }
        else
        {
            iceIndicator.SetActive(false);
            noIceIndicator.SetActive(true);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (isOn)
        {
            if((hasIce && tempNode.temperature > 273) || (!hasIce && tempNode.temperature <= 273))
            {
                if (isFaulty == true)
                {
                    system.clearErrorReport(section, IDchar);
                    isFaulty = false;
                }
            }
            else
            {
                if (isFaulty == false)
                {
                    StartCoroutine(system.sendErrorReport(section, IDchar, 0));
                    isFaulty = true;
                }
            }
        }
    }

    public void changeSetting(bool x)
    {
        hasIce = x;
        iceIndicator.SetActive(hasIce);
        noIceIndicator.SetActive(!hasIce);
    }
}
