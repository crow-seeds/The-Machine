using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using System.Xml;
using System.IO;
using System;
using System.Globalization;
using System.Text;

public class intro : MonoBehaviour
{
    // Start is called before the first frame update


    [SerializeField] List<string> messageStrings = new List<string>();
    [SerializeField] List<TextMeshProUGUI> messages = new List<TextMeshProUGUI>();
    [SerializeField] TMP_InputField input;
    Dictionary<string, string> responses = new Dictionary<string, string>();
    [SerializeField] private TextAsset responseData;
    IFormatProvider format = new CultureInfo("en-US");

    List<string> dontknowResponses = new List<string> { "I don't know.", "I can't respond to that.", "What?", "Don't understand you.", "What are you saying?", "No response.", "?", "I am unfamiliar with that.", "Sorry, can't help you.", "Ok." };

    void Start()
    {
        generateResponses();
    }

    void generateResponses()
    {
        string data = responseData.text;
        XmlDocument xmlDoc = new XmlDocument();
        xmlDoc.Load(new StringReader(data));

        XmlNodeList responseNodeList = xmlDoc.SelectNodes("//data/response");
        foreach (XmlNode infonode in responseNodeList)
        {
            responses.Add(infonode.Attributes["input"].Value, infonode.Attributes["response"].Value);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            if(StripPunctuation(input.text.ToLower()) == "library")
            {
                SceneManager.LoadScene("SampleScene");
            }
            addToMessages(">" + input.text);
            checkResponses(StripPunctuation(input.text.ToLower()));
            Debug.Log("why");
            input.text = "";
        }
    }

    void checkResponses(string s)
    {
        if (responses.ContainsKey(s))
        {
            addToMessages(responses[s]);
        }
        else
        {
            addToMessages(dontknowResponses[UnityEngine.Random.Range(0, dontknowResponses.Count)]);
        }
    }

    void addToMessages(string s)
    {
        if(messageStrings.Count < 7)
        {
            messages[messageStrings.Count].text = s;
            messageStrings.Add(s);
        }
        else
        {
            messageStrings.RemoveAt(0);
            messageStrings.Add(s);
            setText();
        }
    }

    void setText()
    {
        for(int i = 0; i < 7; i++)
        {
            messages[i].text = messageStrings[i];
        }
    }

    public string StripPunctuation(string s)
    {
        var sb = new StringBuilder();
        foreach (char c in s)
        {
            if (!char.IsPunctuation(c))
                sb.Append(c);
        }
        return sb.ToString();
    }
}
