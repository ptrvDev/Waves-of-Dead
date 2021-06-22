using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class areaController : MonoBehaviour
{
    public GameObject[] Area;


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Area[0].SetActive(false);
            Area[1].SetActive(true);
        }
    }
}
