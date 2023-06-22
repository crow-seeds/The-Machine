using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class collectNode : normalNode
{
    float timer = 0;
    [SerializeField] List<GameObject> leftImages;
    [SerializeField] List<GameObject> centerImages;
    [SerializeField] List<GameObject> rightImages;
    [SerializeField] List<GameObject> selectorImages;
    int leftSide = -1;
    int centerSide = -1;
    int rightSide = -1;
    int cycleNum = 0;
    int selectedSide = 0; //0 is left, 1 is center, 2 is right

    // Start is called before the first frame update
    void Start()
    {
        leftSide = UnityEngine.Random.Range(3, 8);
        rightSide = leftSide + 3;
        if(rightSide > 7)
        {
            rightSide = leftSide - 3;
        }

        selectedSide = UnityEngine.Random.Range(0, 3);
        for (int j = 0; j < 3; j++)
        {
            selectorImages[j].SetActive(false);
        }
        selectorImages[selectedSide].SetActive(true);

        showcase();
    }

    void showcase()
    {
        for(int i = 0; i < 7; i++)
        {
            leftImages[i].SetActive(false);
            centerImages[i].SetActive(false);
            rightImages[i].SetActive(false);
        }

        if(leftSide >= 0 && leftSide < 8)
        {
            leftImages[Mathf.Max(leftSide - 1, 0)].SetActive(true);
        }

        if (centerSide >= 0 && centerSide < 8)
        {
            centerImages[Mathf.Max(centerSide - 1, 0)].SetActive(true);
        }

        if (rightSide >= 0 && rightSide < 8)
        {
            rightImages[Mathf.Max(rightSide - 1, 0)].SetActive(true);
        }
    }

    void moveDown()
    {
        if(leftSide != -1)
        {
            leftSide = Mathf.Max(0, leftSide - 1);
            if(leftSide == 0 && selectedSide != 0)
            {
                StartCoroutine(system.sendErrorReport("S", IDchar, 0));
            }

            if(leftSide == 0 && selectedSide == 0)
            {
                leftSide = -1;
            }
        }

        if (centerSide != -1)
        {
            centerSide = Mathf.Max(0, centerSide - 1);
            if (centerSide == 0 && selectedSide != 1)
            {
                StartCoroutine(system.sendErrorReport("S", IDchar, 0));
            }

            if (centerSide == 0 && selectedSide == 1)
            {
                centerSide = -1;
            }
        }

        if (rightSide != -1)
        {
            rightSide = Mathf.Max(0, rightSide - 1);
            if (rightSide == 0 && selectedSide != 2)
            {
                StartCoroutine(system.sendErrorReport("S", IDchar, 0));
            }

            if (rightSide == 0 && selectedSide == 2)
            {
                rightSide = -1;
            }
        }

        if(rightSide == 0 || leftSide == 0 || centerSide == 0)
        {
            if (!isFaulty)
            {
                isFaulty = true;
                StartCoroutine(system.sendErrorReport("S", IDchar, 0));
            }
        }
        else
        {
            if (isFaulty)
            {
                isFaulty = false;
                system.clearErrorReport("S", IDchar);
            }
        }
    }

    void addNew()
    {
        List<string> empties = new List<string>();
        if(leftSide == -1)
        {
            empties.Add("left");
        }

        if(centerSide == -1)
        {
            empties.Add("center");
        }

        if (rightSide == -1)
        {
            empties.Add("right");
        }

        if(empties.Count != 0)
        {
            string thing = empties[UnityEngine.Random.Range(0, empties.Count)];
            if(thing == "left")
            {
                leftSide = 7;
            }else if(thing == "center")
            {
                centerSide = 7;
            }
            else
            {
                rightSide = 7;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (isOn)
        {
            timer += Time.deltaTime;
            if(timer > 6f)
            {
                timer = 0;
                moveDown();
                showcase();
                cycleNum++;
                if(cycleNum == 3)
                {
                    cycleNum = 0;
                    addNew();
                }
            }
        }
    }

    public void moveSelector(int i)
    {
        if (isOn)
        {
            selectedSide = i;
            for (int j = 0; j < 3; j++)
            {
                selectorImages[j].SetActive(false);
            }
            selectorImages[i].SetActive(true);
        }
    }

    
}
