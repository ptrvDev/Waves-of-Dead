using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{

    GameObject enemy;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            enemy = other.gameObject;
            enemy.SendMessage("bulletDamage");
            // Debug.Log("bulletDestroyed");
            Destroy(this.gameObject);
        }
    }
}
