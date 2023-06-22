using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class volumeKnobNode : normalNode
{
    public volumeNode volNode;
    public Image knobBars;

    public GameObject off;
    public GameObject on;
    bool sending = false;

    Vector2 knobLocation = new Vector2(256, 152);

    // Start is called before the first frame update
    void Start()
    {
        if(Random.Range(0f, 1f) < .5f)
        {
            sending = true;
        }
        on.SetActive(sending);
        off.SetActive(!sending);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void onHandleDrag()
    {
        if (isOn)
        {
            Vector2 mousePos = new Vector2(1600 * (Input.mousePosition.x / Screen.width), 900 * (Input.mousePosition.y / Screen.height));
            Vector2 direction = knobLocation - mousePos;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

            angle -= 90;
            angle = (angle <= 0) ? (360 + angle) : angle;

            if (angle > 25 && angle < 335)
            {
                knobBars.fillAmount = ((360 - angle) / 360f);
            }
        }
      
    }

    public override void turnOn()
    {
        base.turnOn();
    }

    public void toggle()
    {
        if (isOn)
        {
            sending = !sending;
            on.SetActive(sending);
            off.SetActive(!sending);

            if (sending)
            {
                volNode.volume += (knobBars.fillAmount - .5f) * 30;
            }
        }
    }
}
