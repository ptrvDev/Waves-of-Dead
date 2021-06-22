using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class healthCollectable : MonoBehaviour
{
    public GameObject Player;
    bool collected = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            collected = true;
        }
    }

    private void Update()
    {
        if (collected)
        {
            Player.GetComponent<playerController>().CollectHealth();
            Destroy(this.gameObject);
        }
    }
}