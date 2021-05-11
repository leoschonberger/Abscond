using System;
using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using TMPro;
using UnityEngine;

public class Signpost : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject box;
    public String message;
    private List<String> subStrings = new List<String>();
    public TextMeshPro signText;
    public GameObject continueText;
    private int messageNum = 0;
    void Start()
    {
        signText = box.GetComponent<TextMeshPro>();
        box.SetActive(false);
        int start = 0;
        for (int i = 0; i < message.Length; i++)
        {
            if (message[i] == '|')
            {
                subStrings.Add(message.Substring(start, i-start));
                start = i+1;
            }
        }
        subStrings.Add(message.Substring(start, message.Length-start));
        signText.text = subStrings[0];
    }

    // Update is called once per frame
    void Update()
    {
        if (messageNum < subStrings.Count - 1)
        {
            if (Input.GetKeyDown(KeyCode.DownArrow) && box.activeSelf)
            {
                messageNum++;
                signText.text = subStrings[messageNum];
            }
        }
        else
        {
            continueText.SetActive(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        box.SetActive(true);
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        box.SetActive(false);
        messageNum = 0;
        continueText.SetActive(true);
        signText.text = subStrings[0];
    }
}
