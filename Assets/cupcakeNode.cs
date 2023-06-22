using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class cupcakeNode : diagnosticNode
{
    public TextMeshProUGUI cupcakeText;
    public int cupcakeNumber = 0;
    List<string> cupcakes = new List<string>() { "Blueberry", "Strawberry", "Chocolate", "Vanilla", "Coffee" };


    // Start is called before the first frame update
    void Start()
    {
        cupcakeNumber = UnityEngine.Random.Range(0, cupcakes.Count);
        cupcakeText.text = cupcakes[cupcakeNumber];
    }

    // Update is called once per frame
    void Update()
    {

    }

    IEnumerator change()
    {
        yield return new WaitForSeconds(105f);
        cupcakeNumber = UnityEngine.Random.Range(0, cupcakes.Count);
        cupcakeText.text = cupcakes[cupcakeNumber];
        StartCoroutine(change());
    }
    public override void turnOn()
    {
        base.turnOn();
        StartCoroutine(change());
    }
}
