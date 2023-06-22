using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class normalNode : MonoBehaviour
{
    public inputStuff system;
    public bool isOn = false;
    public List<diagnosticNode> connectedNodes;
    public string section;
    public bool isFaulty = false;

    public string IDchar;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public virtual void turnOn()
    {
        if (!isOn)
        {
            system.amountOfNormalNodes++;
        }
        isOn = true;
        Color c = new Color(1, 1, 1);
        system.newNodeText.text = section + "-" + IDchar;
        
        switch (section)
        {
            case "W":
                c = new Color(1, 0, 1);
                break;
            case "A":
                c = new Color(0, 1, 0);
                break;
            case "S":
                c = new Color(0, 1, 1);
                break;
        }

        foreach (Transform child in transform)
        {
            GameObject g = child.gameObject;

            foreach (Transform grandChild in child)
            {
                GameObject gc = grandChild.gameObject;

                if (gc.GetComponent<RawImage>() != null)
                {
                    gc.GetComponent<RawImage>().color = c;
                }
                else if (gc.GetComponent<TextMeshProUGUI>() != null)
                {
                    gc.GetComponent<TextMeshProUGUI>().color = c;
                }else if(gc.GetComponent<Image>() != null)
                {
                    gc.GetComponent<Image>().color = c;
                }
            }

            if (g.GetComponent<RawImage>() != null)
            {
                g.GetComponent<RawImage>().color = c;
            }
            else if (g.GetComponent<TextMeshProUGUI>() != null)
            {
                g.GetComponent<TextMeshProUGUI>().color = c;
            }
            else if (g.GetComponent<Image>() != null)
            {
                g.GetComponent<Image>().color = c;
            }
        }

        foreach(diagnosticNode d in connectedNodes)
        {
            d.turnOn();
        }
    }
}
