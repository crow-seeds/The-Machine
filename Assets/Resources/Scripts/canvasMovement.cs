using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class canvasMovement : MonoBehaviour
{
    EasingFunction.Function function;
    bool isMoving = false;
    string direction;
    float xPosOld;
    float yPosOld;
    float xPos;
    float yPos;
    float timer = 0;
    public RectTransform sections;


    // Start is called before the first frame update
    void Start()
    {
        EasingFunction.Ease movement = EasingFunction.Ease.EaseInOutBounce;
        function = EasingFunction.GetEasingFunction(movement);
        xPosOld = sections.anchoredPosition.x;
        yPosOld = sections.anchoredPosition.y;
    }

    // Update is called once per frame
    void Update()
    {
        if (isMoving)
        {
            timer += Time.deltaTime;
            sections.anchoredPosition = new Vector2(function(xPosOld, xPos, timer * 4), function(yPosOld, yPos, timer * 4));
            if(timer >= .25f)
            {
                isMoving = false;
                xPosOld = xPos;
                yPosOld = yPos;
                timer = 0;
                sections.anchoredPosition = new Vector2(xPos, yPos);
            }
        }
    }

    public void moveToSection(string s)
    {
        if (!isMoving && s != direction)
        {
            direction = s;
            isMoving = true;
            switch (direction)
            {
                case "up":
                    xPos = 0;
                    yPos = -900;
                    break;
                case "down":
                    xPos = 0;
                    yPos = 0;
                    break;
                case "left":
                    xPos = 1600;
                    yPos = 0;
                    break;
                case "right":
                    xPos = -1600;
                    yPos = 0;
                    break;
            }
        }
    }
}
