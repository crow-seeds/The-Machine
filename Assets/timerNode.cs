using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class timerNode : MonoBehaviour
{
    float time = 480;
    [SerializeField] TextMeshProUGUI timerText;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(time > 0)
        {
            time -= Time.deltaTime;
            string secondsString = ((int)(time) % 60).ToString();
            if (((int)(time) % 60) < 10)
            {
                secondsString = "0" + ((int)(time) % 60).ToString();
            }

            timerText.text = ((int)(time / 60)).ToString() + ":" + secondsString;
        }
        
    }

    public float getTime()
    {
        return 480 - time;
    }
}
