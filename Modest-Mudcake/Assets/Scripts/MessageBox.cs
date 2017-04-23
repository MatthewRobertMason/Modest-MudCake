﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MessageBox : MonoBehaviour 
{
    public string text;
    public string buttonText;

    public GameObject TextBox;
    public GameObject ButtonTextBox;

    void Start()
    {
        this.gameObject.GetComponent<Canvas>().worldCamera = GameObject.Find("Main Camera").GetComponent<Camera>();
    }

    public void SetText()
    {
        TextBox.GetComponent<Text>().text = text;
        ButtonTextBox.GetComponent<Text>().text = buttonText;
    }

    public void SetVisible(bool visible)
    {
        SetText();
        this.gameObject.SetActive(visible);
    }

    public bool IsVisible()
    {
        return this.gameObject.activeSelf;
    }

    public void btnOKPressed()
    {
        SetVisible(false);
    }
}
