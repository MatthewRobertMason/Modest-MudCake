using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MessageBox : MonoBehaviour 
{
    public GameSession gameSession = null;

    public string text;
    public string buttonText;

	public Font font;
	public Font buttonFont;

    public GameObject TextBox;
    public GameObject ButtonTextBox;

    void Start()
    {
        gameSession = GameObject.Find("GameSession").GetComponent<GameSession>();
        this.gameObject.GetComponent<Canvas>().worldCamera = GameObject.Find("Main Camera").GetComponent<Camera>();
					
		TextBox.GetComponent<Text>().font = font;
		ButtonTextBox.GetComponent<Text>().font = buttonFont;
		ButtonTextBox.GetComponent<Text>().fontSize = 15;
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
