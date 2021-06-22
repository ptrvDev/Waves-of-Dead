using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WinLoseMenu : MonoBehaviour
{
    public Image Screen;
    public Text text1;
    public Text text2;
    public Button button1;

    public void enableAll()
    {
        Screen.GetComponent<Image>().enabled = true;
        button1.GetComponent<Button>().enabled = true;
        text1.GetComponent<Text>().enabled = true;
        text2.GetComponent<Text>().enabled = true;
    }
}
