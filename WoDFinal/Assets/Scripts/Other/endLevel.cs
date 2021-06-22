using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class endLevel : MonoBehaviour
{
    public GameObject Player;
    public Image winScreen;

    public bool levelFinished = false;
    public float delayBeforeWinScreen = 1;
    public bool winScreenEnabled = false;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            levelFinished = true;
        }
    }

    private void Update()
    {
        if (levelFinished)
        {
            delayBeforeWinScreen -= Time.deltaTime;
            if(delayBeforeWinScreen <= 0 && !winScreenEnabled)
            {
                Player.GetComponent<playerController>().LevelFinished();
                winScreen.GetComponent<WinLoseMenu>().enableAll();
                winScreenEnabled = true;
            }
        }
    }
}
