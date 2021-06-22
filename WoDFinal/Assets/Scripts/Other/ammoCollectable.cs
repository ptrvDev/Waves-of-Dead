using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ammoCollectable : MonoBehaviour
{
    public GameObject Player;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Player.GetComponent<playerController>().CollectAmmo();
            Destroy(this.gameObject);
        }
    }
}
