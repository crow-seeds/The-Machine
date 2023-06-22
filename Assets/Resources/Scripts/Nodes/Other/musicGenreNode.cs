using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class musicGenreNode : normalNode
{
    List<string> genres = new List<string> { "Classical", "Jazz", "Rock", "Metal" };
    List<int> volumeModifiers = new List<int> { -3, -1, 1, 3 };
    int currentIndex;
    public volumeNode volNode;
    public TextMeshProUGUI genreText;

    // Start is called before the first frame update
    void Start()
    {
        connectedNodes.Add(volNode);
        currentIndex = Random.Range(0, 4);
        genreText.text = genres[currentIndex];
    }

    // Update is called once per frame
    void Update()
    {
        if (isOn)
        {
            float currentVolume = volNode.volume;
            if((currentIndex == 0 && currentVolume >= 130) || (currentIndex == 1 && currentVolume < 130 && currentVolume >= 70) || (currentIndex == 2 && currentVolume < 70 && currentVolume >= 30) || (currentIndex == 3 && currentVolume < 30))
            {
                if (isFaulty)
                {
                    system.clearErrorReport("S", IDchar);
                    isFaulty = false;
                }
            }
            else
            {
                if (!isFaulty)
                {
                    StartCoroutine(system.sendErrorReport("S", IDchar, 0));
                    isFaulty = true;
                }
            }
        }
    }

    public override void turnOn()
    {
        volNode.volumeRate += volumeModifiers[currentIndex];
        base.turnOn();
    }

    public void changeGenre(int direction) //-1 and 1
    {
        if (isOn)
        {
            volNode.volumeRate -= volumeModifiers[currentIndex];
            currentIndex += direction;

            if (currentIndex == -1)
            {
                currentIndex = 3;
            }

            if (currentIndex == 4)
            {
                currentIndex = 0;
            }

            genreText.text = genres[currentIndex];
            volNode.volumeRate += volumeModifiers[currentIndex];
        }
        
    }
}
