using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerController : MonoBehaviour
{

    [SerializeField]
    private CharacterController controller;

    [SerializeField]
    Rigidbody rb;

    [SerializeField]
    private float speed;


    // Update is called once per frame
    void Update()
    {
        float horizontal = ControlFreak2.CF2Input.GetAxisRaw("Horizontal");
        float vertical = ControlFreak2.CF2Input.GetAxisRaw("Vertical");
        Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;

        if(direction.magnitude >= 0.1f)
        {
            controller.Move(direction * speed * Time.deltaTime);
        }


        
    }
}
