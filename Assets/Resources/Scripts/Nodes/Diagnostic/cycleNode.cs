using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class cycleNode : diagnosticNode
{
    public float cycleTime;
    public RectTransform cycleIcon;
    public TextMeshProUGUI cycleText;

    public float cycleAcceleration = .005f;
    Vector2 leftVector = Vector2.left;

    // Start is called before the first frame update
    void Start()
    {
        cycleTime = Random.Range(lowestSpawnedNumber, highestSpawnedNumber);

        if (Random.Range(0f, 1f) > .5f)
        {
            cycleAcceleration *= -1;
        }

        cycleText.text = System.Math.Round(cycleTime, 2).ToString() + "s";
    }

    // Update is called once per frame
    void Update()
    {
        if (isOn)
        {
            cycleIcon.anchoredPosition = cycleIcon.anchoredPosition + (leftVector * Time.deltaTime * (296 / cycleTime));
            if(cycleIcon.anchoredPosition.x <= -148)
            {
                cycleIcon.anchoredPosition = new Vector2(148, cycleIcon.anchoredPosition.y);
            }
        }
    }

    public override void turnOn()
    {
        base.turnOn();
        StartCoroutine(change());
    }

    IEnumerator change()
    {
        yield return new WaitForSeconds(.1f);
        cycleTime += Random.Range(cycleAcceleration - 0.002f, cycleAcceleration + 0.002f);

        if(cycleTime > highestNumber)
        {
            cycleTime = highestNumber;
        }

        if(cycleTime < lowestNumber)
        {
            cycleTime = lowestNumber;
        }

        if(Random.Range(0f, 1f) < .03f)
        {
            if(cycleAcceleration >= 0)
            {
                cycleAcceleration -= 0.002f;
            }
            else
            {
                cycleAcceleration += 0.002f;
            }
        }

        cycleText.text = System.Math.Round(cycleTime, 2).ToString() + "s";
        StartCoroutine(change());
    }
}
