using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class matrixNode : normalNode
{
    int amountOfNodes = 0;
    int amountOfDiagnostic = 0;
    int amountOfMinutes = 0;
    int amountTotal = 0;
    [SerializeField] timerNode t;
    [SerializeField] List<TextMeshProUGUI> numbers = new List<TextMeshProUGUI>();


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isOn)
        {
            if(((amountOfNodes != system.amountOfNormalNodes) || (amountOfDiagnostic != system.amountOfDiagnosticNodes + 4) || ((int)(t.getTime() / 60) != amountOfMinutes) || (amountOfMinutes + amountOfDiagnostic + amountOfNodes != amountTotal)))
            {
                if (!isFaulty)
                {
                    StartCoroutine(system.sendErrorReport(section, IDchar, 0));
                    isFaulty = true;
                }
            }
            else
            {
                if (isFaulty)
                {
                    system.clearErrorReport(section, IDchar);
                    isFaulty = false;
                }
            }
        }
    }

    public void increaseNumber(int i)
    {
        if (isOn)
        {
            switch (i)
            {
                case 0:
                    amountOfNodes++;
                    numbers[0].text = amountOfNodes.ToString();
                    break;
                case 1:
                    amountOfDiagnostic++;
                    numbers[1].text = amountOfDiagnostic.ToString();
                    break;
                case 2:
                    amountOfMinutes++;
                    numbers[2].text = amountOfMinutes.ToString();
                    break;
                case 3:
                    amountTotal++;
                    numbers[3].text = amountTotal.ToString();
                    break;
            }
        }
    }

    public void clearNumbers()
    {
        if (isOn)
        {
            amountOfNodes = 0;
            amountOfDiagnostic = 0;
            amountOfMinutes = 0;
            amountTotal = 0;
            numbers[0].text = amountOfNodes.ToString();
            numbers[1].text = amountOfDiagnostic.ToString();
            numbers[2].text = amountOfMinutes.ToString();
            numbers[3].text = amountTotal.ToString();
        }
    }
}
