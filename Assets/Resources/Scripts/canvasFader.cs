using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class canvasFader : MonoBehaviour
{
    CanvasGroup cg;
    float destOpacity;
    float sourceOpacity;

    float time = 0;
    float duration;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime / duration;


        if (cg == null)
        {
            Destroy(gameObject);
        }
        else if (time > 1)
        {
            cg.alpha = destOpacity;
            Destroy(gameObject);
        }
        else
        {
            cg.alpha = Mathf.Lerp(sourceOpacity, destOpacity, time);
        }
    }

    public void set(CanvasGroup o, float d, float dur)
    {
        cg = o;
        destOpacity = d;
        sourceOpacity = o.alpha;
        duration = dur;
    }
}
