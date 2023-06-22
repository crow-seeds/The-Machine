using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class temperatureNode : diagnosticNode
{
    public float temperature;
    public TextMeshProUGUI temperatureText;
    public RawImage temperatureImage;
    public RawImage intensityImage;
    public float temperatureRate = -1;

    bool isFaulty = false;

    // Start is called before the first frame update
    void Start()
    {
        temperature = Random.Range(lowestSpawnedNumber, highestSpawnedNumber + 1);
        if(Random.Range(0f, 1f) > .5f)
        {
            temperatureRate = 1;
        }
        temperatureText.text = ((int)temperature).ToString() + "°K";

        if (temperature > 273)
        {
            temperatureImage.texture = Resources.Load<Texture>("Sprites/d_node1_hot");
        }
        else
        {
            temperatureImage.texture = Resources.Load<Texture>("Sprites/d_node1_cold");
        }

        if (temperature < 110 || temperature > 500)
        {
            intensityImage.texture = Resources.Load<Texture>("Sprites/d_node1_intense");
        }
        else
        {
            intensityImage.texture = Resources.Load<Texture>("Sprites/d_node1_normal");
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void turnOn()
    {
        base.turnOn();
        StartCoroutine(change());
    }

    IEnumerator change()
    {
        yield return new WaitForSeconds(1f);

        temperature += Random.Range(temperatureRate - 1, temperatureRate + 1);

        if (temperature < lowestNumber)
        {
            temperature = lowestNumber;
        }

        if (temperature > highestNumber)
        {
            temperature = highestNumber;
        }

        if (temperature > 273)
        {
            temperatureImage.texture = Resources.Load<Texture>("Sprites/d_node1_hot");
        }
        else
        {
            temperatureImage.texture = Resources.Load<Texture>("Sprites/d_node1_cold");
        }

        if (temperature < 100 || temperature > 400)
        {
            intensityImage.texture = Resources.Load<Texture>("Sprites/d_node1_intense");
        }
        else
        {
            intensityImage.texture = Resources.Load<Texture>("Sprites/d_node1_normal");
        }

        if(Random.Range(0f, 1f) < .05f)
        {
            temperatureRate *= -1;
        }

        if((temperature < 100 || temperature > 800) && !isFaulty)
        {
            StartCoroutine(system.sendErrorReport("D", IDchar, 0));
            isFaulty = true;
        }

        if(temperature >= 100 && temperature <= 800 && isFaulty)
        {
            system.clearErrorReport("D", IDchar);
            isFaulty = false;
        }

        temperatureText.text = ((int)temperature).ToString() + "°K";
        StartCoroutine(change());
    }


}
