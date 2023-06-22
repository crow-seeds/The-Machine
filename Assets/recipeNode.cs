using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class recipeNode : normalNode
{
    float amountSugar = 0;
    float amountFlour = 0;
    float amountMilk = 0;
    float amountEggs = 0;
    [SerializeField] cupcakeNode cupcake;
    [SerializeField] List<TextMeshProUGUI> numbers = new List<TextMeshProUGUI>();

    List<float> correctSugar = new List<float>() { 1.5f, 1f, 1f, 2f, .5f };
    List<float> correctFlour = new List<float>() { 2f, 2f, 2f, 4f, 1.5f };
    List<float> correctMilk = new List<float>() { .5f, .5f, .5f, 2f, .5f };
    List<float> correctEggs = new List<float>() { 2f, 2f, 2f, 2f, 2f };


    // Start is called before the first frame update
    void Start()
    {

    }

    bool floatEquals(float f1, float f2)
    {
        return Mathf.Abs(f1 - f2) < .1f;
    }


    // Update is called once per frame
    void Update()
    {
        if (isOn)
        {
            int cupcakeNum = cupcake.cupcakeNumber;
            if(floatEquals(correctSugar[cupcakeNum], amountSugar) && floatEquals(correctFlour[cupcakeNum], amountFlour) && floatEquals(correctMilk[cupcakeNum], amountMilk) && floatEquals(correctEggs[cupcakeNum], amountEggs))
            {
                if (isFaulty)
                {
                    system.clearErrorReport(section, IDchar);
                    isFaulty = false;
                }
            }
            else
            {
                if (!isFaulty)
                {
                    StartCoroutine(system.sendErrorReport(section, IDchar, 0));
                    isFaulty = true;
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
                    amountSugar += .5f;
                    if(floatEquals(amountSugar, 1f))
                    {
                        numbers[0].text = "1 Cup";
                    }
                    else
                    {
                        numbers[0].text = ((int)(amountSugar * 10) / 10f).ToString() + " Cups";
                    }
                    break;
                case 1:
                    amountFlour += .5f;
                    if (floatEquals(amountFlour, 1f))
                    {
                        numbers[1].text = "1 Cup";
                    }
                    else
                    {
                        numbers[1].text = ((int)(amountFlour * 10) / 10f).ToString() + " Cups";
                    }
                    break;
                case 2:
                    amountMilk += .5f;
                    if (floatEquals(amountMilk, 1f))
                    {
                        numbers[2].text = "1 Cup";
                    }
                    else
                    {
                        numbers[2].text = ((int)(amountMilk * 10) / 10f).ToString() + " Cups";
                    }
                    break;
                case 3:
                    amountEggs += 1f;
                    if (floatEquals(amountEggs, 1f))
                    {
                        numbers[3].text = "1 Large";
                    }
                    else
                    {
                        numbers[3].text = ((int)(amountEggs * 10) / 10f).ToString() + " Large";
                    }
                    break;
            }
        }
    }

    public void clearNumbers()
    {
        if (isOn)
        {
            amountSugar = 0;
            amountFlour = 0;
            amountMilk = 0;
            amountEggs = 0;
            numbers[0].text = "0 Cups";
            numbers[1].text = "0 Cups";
            numbers[2].text = "0 Cups";
            numbers[3].text = "0 Large";
        }
    }
}
