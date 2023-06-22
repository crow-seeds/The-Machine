using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class volumeNode : diagnosticNode
{
    public float volume;
    public TextMeshProUGUI volumeText;
    public RawImage volumeIcon;
    public float volumeRate = 0;

    public bool isFaulty = false;

    // Start is called before the first frame update
    void Start()
    {
        volume = Random.Range(lowestSpawnedNumber, highestSpawnedNumber + 1);

        if (volume < 30)
        {
            volumeIcon.texture = Resources.Load<Texture>("Sprites/d_node2_silent");
        }
        else if (volume < 70)
        {
            volumeIcon.texture = Resources.Load<Texture>("Sprites/d_node2_quiet");
        }
        else if (volume < 130)
        {
            volumeIcon.texture = Resources.Load<Texture>("Sprites/d_node2_medium");
        }
        else
        {
            volumeIcon.texture = Resources.Load<Texture>("Sprites/d_node2_loud");
        }

        volumeText.text = ((int)volume).ToString() + "dB";
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
        yield return new WaitForSeconds(5f);

        volume += Random.Range(volumeRate - 5, volumeRate + 5);

        if (volume < lowestNumber)
        {
            volume = lowestNumber;
        }

        if (volume > highestNumber)
        {
            volume = highestNumber;
        }

        if (Random.Range(0f, 1f) < .25f && volume > 70 && volume < 130)
        {
            if (Random.Range(0f, 1f) > .5f)
            {
                volume += 20;
            }
            else
            {
                volume -= 20;
            }
        }

        if (volume < 30)
        {
            volumeIcon.texture = Resources.Load<Texture>("Sprites/d_node2_silent");
        }
        else if (volume < 70)
        {
            volumeIcon.texture = Resources.Load<Texture>("Sprites/d_node2_quiet");
        }
        else if (volume < 130)
        {
            volumeIcon.texture = Resources.Load<Texture>("Sprites/d_node2_medium");
        }
        else
        {
            volumeIcon.texture = Resources.Load<Texture>("Sprites/d_node2_loud");
        }

        if ((volume < 0 || volume > 130) && !isFaulty)
        {
            StartCoroutine(system.sendErrorReport("D", IDchar, 0));
            isFaulty = true;
        }else if(!(volume < 0 || volume > 130) && isFaulty)
        {
            system.clearErrorReport("D", IDchar);
            isFaulty = false;
        }

        volumeText.text = ((int)volume).ToString() + "dB";
        StartCoroutine(change());
    }

}
