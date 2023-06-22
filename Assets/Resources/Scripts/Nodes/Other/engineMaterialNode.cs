using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class engineMaterialNode : normalNode
{
    int elementNumber;
    public RectTransform[] elements = new RectTransform[3];
    public temperatureNode tempNode;
    public RectTransform underlineBar;

    // Start is called before the first frame update
    void Start()
    {
        connectedNodes.Add(tempNode);
        elementNumber = Random.Range(0, 3);
        underlineBar.anchoredPosition = new Vector2(elements[elementNumber].anchoredPosition.x, underlineBar.anchoredPosition.y);
    }

    // Update is called once per frame
    void Update()
    {
        if (isOn)
        {
            if((elementNumber == 0 && tempNode.temperature <= 400) || (elementNumber == 1 && tempNode.temperature > 400 && tempNode.temperature <= 500) || (elementNumber == 2 && tempNode.temperature > 500)){
                if(isFaulty == true)
                {
                    system.clearErrorReport("W", IDchar);
                    isFaulty = false;
                }
            }
            else
            {
                if(isFaulty == false)
                {
                    StartCoroutine(system.sendErrorReport("W", IDchar, 0));
                    isFaulty = true;
                }
            }
        }
    }

    public void selectElement(int x)
    {
        if (isOn)
        {
            elementNumber = x;
            underlineBar.anchoredPosition = new Vector2(elements[elementNumber].anchoredPosition.x, underlineBar.anchoredPosition.y);
        }
    }
}
