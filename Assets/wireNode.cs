using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class wireNode : normalNode
{
    [SerializeField] List<bool> wires = new List<bool> { false, false, false };
    [SerializeField] List<RawImage> wireImages = new List<RawImage>();
    [SerializeField] cycleNode cyc;
    [SerializeField] timerNode time;
    bool settled = false;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isOn)
        {
            if (!settled)
            {
                if (isCorrect() && isFaulty)
                {
                    system.clearErrorReport("A", IDchar);
                    isFaulty = false;
                    settled = true;
                }else if(!isCorrect() && !isFaulty)
                {
                    isFaulty = true;
                    StartCoroutine(system.sendErrorReport("A", IDchar, 0));
                }
            }
        }
    }

    public override void turnOn()
    {
        StartCoroutine(minuteChange(60 - time.getTime()));
        base.turnOn();
        setWireColor();
    }

    IEnumerator minuteChange(float i)
    {
        yield return new WaitForSeconds(i);
        settled = false;
        StartCoroutine(minuteChange(60f));
    }

    bool isCorrect()
    {
        int p = ((int)(cyc.cycleTime * 10)) % 10;
        switch (p)
        {
            case 0:
                if(wires[0] == false && wires[1] == false && wires[2] == false)
                {
                    return true;
                }
                break;
            case 1:
                if (wires[0] == false && wires[1] == false && wires[2] == false)
                {
                    return true;
                }
                break;
            case 2:
                if (wires[0] == false && wires[1] == false && wires[2] == true)
                {
                    return true;
                }
                break;
            case 3:
                if (wires[0] == false && wires[1] == true && wires[2] == false)
                {
                    return true;
                }
                break;
            case 4:
                if (wires[0] == false && wires[1] == true && wires[2] == true)
                {
                    return true;
                }
                break;
            case 5:
                if (wires[0] == true && wires[1] == false && wires[2] == false)
                {
                    return true;
                }
                break;
            case 6:
                if (wires[0] == true && wires[1] == false && wires[2] == true)
                {
                    return true;
                }
                break;
            case 7:
                if (wires[0] == true && wires[1] == true && wires[2] == false)
                {
                    return true;
                }
                break;
            case 8:
                if (wires[0] == true && wires[1] == true && wires[2] == true)
                {
                    return true;
                }
                break;
            case 9:
                if (wires[0] == true && wires[1] == true && wires[2] == true)
                {
                    return true;
                }
                break;
        }


        return false;

    }


    public void changeSetting(int i)
    {
        if (isOn)
        {
            wires[i] = !wires[i];
            setWireColor();
        }
    }

    void setWireColor()
    {
        for(int i = 0; i < 3; i++)
        {
            if (wires[i])
            {
                wireImages[i].color = new Color(0, 1f, 0);
            }
            else
            {
                wireImages[i].color = new Color(0, .4f, 0);
            }
        }

        
    }
}
