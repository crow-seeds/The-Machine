using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class petNode : normalNode
{
    float timer = 0;
    [SerializeField] RawImage face;
    [SerializeField] List<Texture> faces;

    bool petOnce = false;

    // Start is called before the first frame update
    void Start()
    {
        timer = UnityEngine.Random.Range(0f, 20f);
        checkFace();
    }

    void checkFace()
    {
        if(timer < 7.5f)
        {
            face.texture = faces[0];
        }else if(timer < 15f)
        {
            face.texture = faces[1];
        }else if(timer < 22.5f)
        {
            face.texture = faces[2];
        }
        else
        {
            face.texture = faces[3];
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (isOn)
        {
            timer += Time.deltaTime;
            checkFace();
            if (timer < 30)
            {
                if (isFaulty == true)
                {
                    system.clearErrorReport("W", IDchar);
                    isFaulty = false;
                }
            }
            else
            {
                if (isFaulty == false)
                {
                    StartCoroutine(system.sendErrorReport("W", IDchar, 0));
                    isFaulty = true;
                }
            }
        }
    }

    public void pet()
    {
        Debug.Log("pet?");
        if (isOn)
        {
            Debug.Log("pet!");
            if (!petOnce)
            {
                petOnce = true;
                StartCoroutine(petTimer());
            }
            else
            {
                timer = 0;
                petOnce = false;
            }
        }
    }

    IEnumerator petTimer()
    {
        yield return new WaitForSeconds(1);
        petOnce = false;
    }
}
