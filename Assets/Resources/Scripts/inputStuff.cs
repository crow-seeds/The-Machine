using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.Rendering.PostProcessing;

public class inputStuff : MonoBehaviour
{
    public canvasMovement movementManager;
    public List<diagnosticNode> diagnosticNodes;
    public List<normalNode> nodes;
    List<int> nodeNumbers = new List<int>();

    public List<RawImage> healthIndicators;

    public TextMeshProUGUI newNodeText;
    public TextMeshProUGUI errorNodeText;

    int health = 16;

    List<string> faultyNodes = new List<string>();
    [SerializeField] AudioSource soundEffects;
    [SerializeField] AudioSource music;
    [SerializeField] RawImage blackScreen;
    [SerializeField] bool debugMode = false;
    [SerializeField] RawImage flashbang;
    [SerializeField] PostProcessVolume m_Volume;

    [SerializeField] List<int> secondsUntilNextNode = new List<int>();

    public int amountOfNormalNodes = 0;
    public int amountOfDiagnosticNodes = 0;
    int stage = 0;

    // Start is called before the first frame update
    void Start()
    {
        blackScreen.color = new Color(0, 0, 0, 1);
        fadeIn();
        if (!debugMode)
        {
            for(int i = 0; i < nodes.Count; i++)
            {
                nodeNumbers.Add(i);
            }



            addNode();
            addNode();
            StartCoroutine(completeGame());
            StartCoroutine(addMoreNodes());
            StartCoroutine(addHealthTimer());
            StartCoroutine(flashBang());
        }

    }

    void addNode()
    {
        int index = UnityEngine.Random.Range(0, nodeNumbers.Count);
        int randNode = nodeNumbers[index];
        nodeNumbers.RemoveAt(index);
        nodes[randNode].turnOn();
    }

    void fadeIn()
    {
        GameObject g = Instantiate<GameObject>(Resources.Load<GameObject>("Prefabs/Image Fader"));
        g.GetComponent<imageFader>().set(blackScreen, new Color(0, 0, 0, 0), 10);
    }

    IEnumerator completeGame()
    {
        yield return new WaitForSeconds(480);
        SceneManager.LoadScene("endscreen");
    }

    IEnumerator addMoreNodes()
    {
        if(stage == 0)
        {
            yield return new WaitForSeconds(secondsUntilNextNode[stage]);
        }
        else
        {
            yield return new WaitForSeconds(secondsUntilNextNode[stage] - secondsUntilNextNode[stage - 1]);
        }
        soundEffects.PlayOneShot(Resources.Load<AudioClip>("Sound/nodealert"));
        stage++;
        addNode();
        if(stage < secondsUntilNextNode.Count)
        {
            StartCoroutine(addMoreNodes());
        }
    }

    IEnumerator addHealthTimer()
    {
        yield return new WaitForSeconds(60);
        addHealth();
        StartCoroutine(addHealthTimer());
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            movementManager.moveToSection("up");
        }

        if (Input.GetKeyDown(KeyCode.A))
        {
            movementManager.moveToSection("left");
        }

        if (Input.GetKeyDown(KeyCode.S))
        {
            movementManager.moveToSection("down");
        }

        if (Input.GetKeyDown(KeyCode.D))
        {
            movementManager.moveToSection("right");
        }

        if (debugMode)
        {
            if (Input.GetKeyDown(KeyCode.F))
            {
                nodes[12].turnOn();
            }
        }
        

        if (Input.anyKeyDown)
        {
            soundEffects.PlayOneShot(Resources.Load<AudioClip>("Sound/beep" + UnityEngine.Random.Range(0, 7).ToString()));
        }
    }

    public IEnumerator sendErrorReport(string section, string node, int cycleCount)
    {
        Debug.Log(cycleCount);
        if(cycleCount != 0 || !faultyNodes.Contains(section + "-" + node.ToString()))
        {
            if (cycleCount == 0 && !faultyNodes.Contains(section + "-" + node.ToString()))
            {
                faultyNodes.Add(section + "-" + node);
                soundEffects.PlayOneShot(Resources.Load<AudioClip>("Sound/erroralert"));
                updateProblemText();
                StartCoroutine(sendErrorReport(section, node, cycleCount++));
            }
            yield return new WaitForSeconds(15);
            Debug.Log("error!!!!");
            if (faultyNodes.Contains(section + "-" + node))
            {
                subtractHealth();
                Debug.Log(section + "-" + node + "-" + cycleCount.ToString());
                soundEffects.PlayOneShot(Resources.Load<AudioClip>("Sound/error"));
                StartCoroutine(sendErrorReport(section, node, cycleCount++));
            }
        }
    }

    public void clearErrorReport(string section, string node)
    {
        faultyNodes.Remove(section + "-" + node);
        updateProblemText();
    }

    void subtractHealth()
    {
        health--;
        if(health >= 0)
        {
            healthIndicators[health].color = Color.black;
        }
        
        if(health == 0)
        {
            soundEffects.PlayOneShot(Resources.Load<AudioClip>("Sound/die"));
            gameOver();
        }
    }

    public void gameOver()
    {
        SceneManager.LoadScene("SampleScene");
    }

    void addHealth()
    {
        health = Mathf.Max(health++, 16);
        healthIndicators[health].color = Color.red;
    }

    void updateProblemText()
    {
        if(faultyNodes.Count != 0)
        {
            errorNodeText.text = faultyNodes[0];
        }
        else
        {
            errorNodeText.text = "---";
        }
    }

    IEnumerator flashBang()
    {
        yield return new WaitForSeconds(352f);
        colorFader2 c = Instantiate<GameObject>(Resources.Load<GameObject>("Prefabs/colorFader2")).GetComponent<colorFader2>();
        c.set(flashbang, new Color(1, 1, 1, 1), .5f);
        
        yield return new WaitForSeconds(.6f);
        ColorGrading s;
        
        m_Volume.profile.TryGetSettings(out s);
        s.saturation.value = -100f;
        colorFader2 c2 = Instantiate<GameObject>(Resources.Load<GameObject>("Prefabs/colorFader2")).GetComponent<colorFader2>();
        c2.set(flashbang, new Color(1, 1, 1, 0), .5f);
    }
}
