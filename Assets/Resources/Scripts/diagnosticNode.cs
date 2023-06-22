using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class diagnosticNode : MonoBehaviour
{
    public float lowestNumber;
    public float highestNumber;
    public float lowestSpawnedNumber;
    public float highestSpawnedNumber;
    public bool isOn = false;

    public inputStuff system;
    public string IDchar;

    public List<normalNode> dependents;

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
            system.amountOfDiagnosticNodes++;
            isOn = true;
            foreach (Transform child in transform)
            {
                GameObject g = child.gameObject;

                foreach (Transform grandChild in child)
                {
                    GameObject gc = grandChild.gameObject;

                    if (gc.GetComponent<RawImage>() != null)
                    {
                        gc.GetComponent<RawImage>().color = new Color(1, 0, 0);
                    }
                    else if (gc.GetComponent<TextMeshProUGUI>())
                    {
                        gc.GetComponent<TextMeshProUGUI>().color = new Color(1, 0, 0);
                    }
                }

                if (g.GetComponent<RawImage>() != null)
                {
                    g.GetComponent<RawImage>().color = new Color(1, 0, 0);
                }
                else if (g.GetComponent<TextMeshProUGUI>())
                {
                    g.GetComponent<TextMeshProUGUI>().color = new Color(1, 0, 0);
                }
            }

            foreach (normalNode n in dependents)
            {
                if (!n.isOn)
                {
                    n.turnOn();
                }
            }
        }
    }

    public virtual void refresh()
    {

    }
}
