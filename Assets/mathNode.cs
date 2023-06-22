using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class mathNode : normalNode
{
    [SerializeField] TextMeshProUGUI solutionNumber;
    [SerializeField] TextMeshProUGUI equation;
    int input = 0;
    List<string> questions = new List<string>(){ "2 + 3 =", "2 + 2 =", "1 + 1 =", "3 - 2 =", "7 - 4 =", "9 - 6 =", "4 - 3 =", "sqrt(4) =", "e^(i*pi) + 1 =", "sin^2(9)+cos^2(9) =", "471 - 469 =", "117 / 39 =", "68 / 17 =" };
    List<int> answers = new List<int>() {5,4,2,1,3,3,1,2,0,1,3,3,4};
    int index = 0;
    [SerializeField] batteryNode batNode;


    // Start is called before the first frame update
    void Start()
    {
        connectedNodes.Add(batNode);
        index = UnityEngine.Random.Range(0, questions.Count);
        input = UnityEngine.Random.Range(0, 6);
        solutionNumber.text = input.ToString();
        equation.text = questions[index];
    }

    // Update is called once per frame
    void Update()
    {
        if (isOn)
        {
            if(batNode.batteryLife > 5)
            {
                if (answers[index] == input && isFaulty)
                {
                    system.clearErrorReport("S", IDchar);
                    isFaulty = false;
                }
                else if(answers[index] != input && !isFaulty)
                {
                    StartCoroutine(system.sendErrorReport("S", IDchar, 0));
                    isFaulty = true;
                }
            }
            else
            {
                if (answers[index] != input && isFaulty)
                {
                    system.clearErrorReport("S", IDchar);
                    isFaulty = false;
                }
                else if (answers[index] == input && !isFaulty)
                {
                    StartCoroutine(system.sendErrorReport("S", IDchar, 0));
                    isFaulty = true;
                }
            }
        }
    }

    IEnumerator changeProblem()
    {
        yield return new WaitForSeconds(47f);
        index = UnityEngine.Random.Range(0, questions.Count);
        input = UnityEngine.Random.Range(0, 6);
        solutionNumber.text = input.ToString();
        equation.text = questions[index];
        StartCoroutine(changeProblem());
    }

    public override void turnOn()
    {
        StartCoroutine(changeProblem());
        base.turnOn();
    }

    public void changeAnswer()
    {
        if (isOn)
        {
            input++;
            if (input > 5)
            {
                input = 0;
            }
            solutionNumber.text = input.ToString();
        }
        
    }
}
