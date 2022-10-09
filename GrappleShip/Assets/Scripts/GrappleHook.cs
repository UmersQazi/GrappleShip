using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrappleHook : MonoBehaviour
{
    public Vector3 shipPosOffset;
    public GameObject playerShip;
    PlayerShip playerShipScript;
    public Vector3 target;
    public bool deployed;

    public float moveSpeed = 20f;

    //Player moving towards target will be done in player script
    Rigidbody rb;

    private void Awake()
    {
        shipPosOffset = transform.position - playerShip.transform.position;
        rb = GetComponent<Rigidbody>(); 
        playerShipScript = playerShip.GetComponent<PlayerShip>();
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            Deploy();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (deployed)
        {
            target = collision.transform.position;
            PullShip();
        }
        
    }

    void Deploy()
    {
        deployed = true;
        Vector3 moveDirection = transform.forward * moveSpeed;
        rb.AddForce(moveDirection);
    }

    void PullShip()
    {
        deployed = false;
        Vector3 moveDirection = target * playerShipScript.moveSpeed;
        Rigidbody playerRB = playerShip.GetComponent<Rigidbody>();
        playerRB.AddForce(-moveDirection);
    }

}
