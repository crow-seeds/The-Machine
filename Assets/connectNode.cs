using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class connectNode : normalNode
{
    [SerializeField] rotationNode rotNode;
    [SerializeField] List<GameObject> dots = new List<GameObject>();
    [SerializeField] List<GameObject> circles = new List<GameObject>();
    List<int> dotNumbers = new List<int>();
    List<int> dotsEntered = new List<int>();
    bool ifDone = false;



    // Start is called before the first frame update
    void Start()
    {
        connectedNodes.Add(rotNode);
        randomize();
    }

    void randomize()
    {
        dotNumbers.Clear();
        dotsEntered.Clear();
        ifDone = false;

        for (int i = 0; i < dots.Count; i++)
        {
            dots[i].SetActive(false);
            circles[i].SetActive(false);
        }

        for (int i = 0; i < dots.Count; i++)
        {
            if(UnityEngine.Random.Range(0f, 1f) < .5f)
            {
                dots[i].SetActive(true);
                dotNumbers.Add(i);
            }
        }
    }

    bool compareLists(List<int> i1, List<int> i2)
    {
        if(i1.Count != i2.Count)
        {
            return false;
        }

        for(int i = 0; i < i1.Count; i++)
        {
            if(i1[i] != i2[i])
            {
                return false;
            }
        }
        return true;
    }



    public override void turnOn()
    {
        StartCoroutine(randomizer());
        base.turnOn();
    }

    // Update is called once per frame
    void Update()
    {
        if (isOn)
        {
            if (!ifDone)
            {
                if (isFaulty && compareLists(dotNumbers, dotsEntered))
                {
                    system.clearErrorReport(section, IDchar);
                    isFaulty = false;
                }
                else if (!isFaulty && !compareLists(dotNumbers, dotsEntered))
                {
                    StartCoroutine(system.sendErrorReport(section, IDchar, 0));
                    isFaulty = true;
                }
            }
            
        }
    }

    void printArray(List<int> l)
    {
        string p = "";
        for (int i = 0; i < l.Count; i++)
        {
            p += l[i].ToString();
        }
        Debug.Log(p);
    }

    public void touchDot(int i)
    {
        if (isOn)
        {
            if (dotsEntered.Contains(i))
            {
                circles[i].SetActive(false);
                dotsEntered.Remove(i);
                ifDone = false;
            }
            else
            {
                circles[i].SetActive(true);
                if(rotNode.rotation <= 180)
                {
                    dotsEntered.Insert(0, i);
                }
                else
                {
                    dotsEntered.Add(i);
                }

                if(compareLists(dotNumbers, dotsEntered))
                {
                    ifDone = true;
                    if (isFaulty)
                    {
                        system.clearErrorReport(section, IDchar);
                        isFaulty = false;
                    }
                }
            }

            printArray(dotNumbers);
            printArray(dotsEntered);
            Debug.Log(compareLists(dotNumbers, dotsEntered));
        }
    }

    IEnumerator randomizer()
    {
        yield return new WaitForSeconds(55);
        randomize();
        StartCoroutine(randomizer());
    }


}
