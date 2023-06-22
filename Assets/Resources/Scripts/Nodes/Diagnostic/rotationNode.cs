using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class rotationNode : diagnosticNode
{
    public TextMeshProUGUI rotationText;
    public RectTransform rotatingImage;
    public float rotation;

    // Start is called before the first frame update
    void Start()
    {
        rotation = Random.Range(0f, 360f);
        rotatingImage.rotation = Quaternion.Euler(new Vector3(0, 0, rotation));
        rotationText.text = ((int)rotation).ToString() + "°";
    }

    // Update is called once per frame
    void Update()
    {
        if (isOn)
        {
            rotation += Time.deltaTime;

            if(rotation >= 360f)
            {
                rotation = 0f;
            }

            rotatingImage.rotation = Quaternion.Euler(new Vector3(0, 0, rotation));
            rotationText.text = ((int)rotation).ToString() + "°";

        }
    }
}
