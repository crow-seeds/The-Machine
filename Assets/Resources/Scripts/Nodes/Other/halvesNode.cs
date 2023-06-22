using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class halvesNode : normalNode
{
    public rotationNode rotNode;

    public GameObject leftSide;
    public GameObject rightSide;
    bool leftSideActive = false;

    public GameObject topSide;
    public GameObject bottomSide;
    bool topSideActive = false;

    // Start is called before the first frame update
    void Start()
    {
        connectedNodes.Add(rotNode);
        if(Random.Range(0f, 1f) < .5f)
        {
            leftSideActive = true;
        }

        if (Random.Range(0f, 1f) < .5f)
        {
            topSideActive = true;
        }

        leftSide.SetActive(leftSideActive);
        rightSide.SetActive(!leftSideActive);
        topSide.SetActive(topSideActive);
        bottomSide.SetActive(!topSideActive);
    }

    // Update is called once per frame
    void Update()
    {
        if (isOn)
        {   

            if ((rotNode.rotation <= 180 && leftSideActive) || (rotNode.rotation >= 180 && !leftSideActive)){

                bool isTop = (rotNode.rotation <= 90 || rotNode.rotation >= 270);
                bool isBottom = (rotNode.rotation >= 90 && rotNode.rotation <= 270);

                if ((isTop && topSideActive) || (isBottom && !topSideActive))
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

    public void toggleLeftRight()
    {
        if (isOn)
        {
            leftSideActive = !leftSideActive;
            leftSide.SetActive(leftSideActive);
            rightSide.SetActive(!leftSideActive);
        }
    }

    public void toggleUpDown()
    {
        if (isOn)
        {
            topSideActive = !topSideActive;
            topSide.SetActive(topSideActive);
            bottomSide.SetActive(!topSideActive);
        }
    }
}
