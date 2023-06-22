using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class heaterNode : normalNode
{
    public temperatureNode tempNode;
    public GameObject leftTab;
    public GameObject rightTab;
    public Slider slide;

    bool hasReachedBottom = false;
    bool increaseTemp = false;

    // Start is called before the first frame update
    void Start()
    {
        slide.enabled = false;
        if(Random.Range(0f, 1f) < .5f)
        {
            increaseTemp = true;
        }

        leftTab.SetActive(!increaseTemp);
        rightTab.SetActive(increaseTemp);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void turnOn()
    {
        base.turnOn();
        slide.enabled = true;
        slide.onValueChanged.AddListener(delegate { checkSliderValue(); });
    }

    void checkSliderValue()
    {
        if(slide.value == 0)
        {
            hasReachedBottom = true;
        }

        if(slide.value == 1 && hasReachedBottom)
        {
            hasReachedBottom = false;
            if (increaseTemp)
            {
                tempNode.temperature += 12;
            }
            else
            {
                tempNode.temperature -= 12;
            }
        }
    }

    public void changeMode(bool x)
    {
        if (isOn)
        {
            increaseTemp = x;
            leftTab.SetActive(!increaseTemp);
            rightTab.SetActive(increaseTemp);
        }
    }
}
