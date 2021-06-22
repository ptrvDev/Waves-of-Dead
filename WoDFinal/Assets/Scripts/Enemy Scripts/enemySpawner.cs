using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class enemySpawner : MonoBehaviour
{
    public GameObject[] enemy;
    public GameObject text;
    public GameObject helicopterArea;
    int wave;
    public int zombiesRemaining;
    float waitForWave = 2;
    bool wave1Instantiated = false;
    bool wave2Instantiated = false;
    bool wave3Instantiated = false;

    // Start is called before the first frame update
    void Start()
    {
        wave = 1;
        zombiesRemaining = 7;
        helicopterArea.SetActive(false);
    }

    public void zombieMinus()
    {
        zombiesRemaining--;
    }

    void FixedUpdate()
    {
        waveController();
        UIcontroller();
        spawnZombies();
    }


    void waveController()
    {
        if (zombiesRemaining == 0)
        {
            waitForWave -= Time.deltaTime;
            if(waitForWave <= 0)
            {
                wave++;
            }
        }
    }


    void UIcontroller()
    {
        if(wave == 2 && !wave2Instantiated && waitForWave <= 0)
        {
            text.GetComponent<Text>().text = "Wave 2 ";
            text.GetComponent<Animator>().Play("textFadeIn", -1, 0);
        } else if(wave == 3 && !wave3Instantiated && waitForWave <= 0)
        {
            text.GetComponent<Text>().text = "Last Wave";
            text.GetComponent<Animator>().Play("textFadeIn", -1, 0);
        } else if(wave == 5 && waitForWave <= 0)
        {
            helicopterArea.SetActive(true);
            text.GetComponent<Text>().text = "Proceed to the Chopper";
            text.GetComponent<Animator>().Play("textFadeIn", -1, 0);
        }
    }


    void spawnZombies()
    {
        if (wave == 1 && !wave1Instantiated)
        {
            Instantiate(enemy[0], GameObject.Find("w1_Pos1").transform.position, Quaternion.identity);
            Instantiate(enemy[2], GameObject.Find("w1_Pos1").transform.position, Quaternion.identity);
            Instantiate(enemy[5], GameObject.Find("w1_Pos2").transform.position, Quaternion.identity);
            Instantiate(enemy[6], GameObject.Find("w1_Pos2").transform.position, Quaternion.identity);
            Instantiate(enemy[4], GameObject.Find("w1_Pos3").transform.position, Quaternion.identity);
            Instantiate(enemy[5], GameObject.Find("w1_Pos3").transform.position, Quaternion.identity);
            Instantiate(enemy[1], GameObject.Find("w1_Pos3").transform.position, Quaternion.identity);
            wave1Instantiated = true;

        }

        if (wave == 2 && !wave2Instantiated)
        {
            Instantiate(enemy[5], GameObject.Find("w2_Pos1").transform.position, Quaternion.identity);
            Instantiate(enemy[8], GameObject.Find("w2_Pos1").transform.position, Quaternion.identity);
            Instantiate(enemy[0], GameObject.Find("w2_Pos1").transform.position, Quaternion.identity);
            Instantiate(enemy[2], GameObject.Find("w2_Pos1").transform.position, Quaternion.identity);
            Instantiate(enemy[7], GameObject.Find("w2_Pos2").transform.position, Quaternion.identity);
            Instantiate(enemy[10], GameObject.Find("w2_Pos2").transform.position, Quaternion.identity);
            Instantiate(enemy[2], GameObject.Find("w2_Pos2").transform.position, Quaternion.identity);
            Instantiate(enemy[6], GameObject.Find("w2_Pos2").transform.position, Quaternion.identity);


            zombiesRemaining = 8;
            wave2Instantiated = true;
        }
        if (wave == 3 && !wave3Instantiated)
        {
            Instantiate(enemy[7], GameObject.Find("w3_Pos1").transform.position, Quaternion.identity);
            Instantiate(enemy[8], GameObject.Find("w3_Pos1").transform.position, Quaternion.identity);
            Instantiate(enemy[2], GameObject.Find("w3_Pos1").transform.position, Quaternion.identity);
            Instantiate(enemy[3], GameObject.Find("w3_Pos2").transform.position, Quaternion.identity);
            Instantiate(enemy[7], GameObject.Find("w3_Pos2").transform.position, Quaternion.identity);
            Instantiate(enemy[4], GameObject.Find("w3_Pos3").transform.position, Quaternion.identity);
            Instantiate(enemy[5], GameObject.Find("w3_Pos4").transform.position, Quaternion.identity);
            Instantiate(enemy[6], GameObject.Find("w3_Pos4").transform.position, Quaternion.identity);


            zombiesRemaining = 8;
            wave3Instantiated = true;
        }
    }




}
