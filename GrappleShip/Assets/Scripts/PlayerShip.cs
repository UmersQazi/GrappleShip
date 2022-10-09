using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Rigidbody))]
public class PlayerShip : MonoBehaviour
{
    [SerializeField]
    public float moveSpeed = 12f;
    [SerializeField]
    float turnSpeed = 3f;
    

    Rigidbody rb = null;
    [SerializeField] GameManager gameMan;
    [SerializeField] GameObject package;
    [SerializeField] TrailRenderer trail;
    [SerializeField] Color powerUpSpeedColor, powerUpInvincibleColor, superColor;
    [SerializeField] GameObject invincibleText, speedText;
    [SerializeField] ParticleSystem particles;
    Color originalStartColor;

    public bool canPassHazard;
    public bool isInvincible, isSpeed, isSuper;
    // Start is called before the first frame update
    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        originalStartColor = trail.startColor;
        canPassHazard = false;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!gameMan.win && !gameMan.gameOver && !gameMan.paused)
        {
            MoveShip();
            TurnShip();
        }
        transform.position = new Vector3(transform.position.x, 0, transform.position.z);
    }

    private void Update()
    {

        if(canPassHazard && isSpeed)
        {
            trail.startColor = superColor;
            particles.startColor = superColor;
        }
        if(!canPassHazard && !isSpeed)
        {
            trail.startColor = originalStartColor;
        }
        if(canPassHazard && !isSpeed)
        {
            trail.startColor = powerUpInvincibleColor;
        }
        if(!canPassHazard && isSpeed)
        {
            trail.startColor = powerUpSpeedColor;
        }


    }

    void MoveShip()
    {
        float moveAmountThisFrame = Input.GetAxisRaw("Vertical")*moveSpeed;
        Vector3 moveDirection = transform.forward * moveAmountThisFrame;
        rb.AddForce(moveDirection);
    }

    void TurnShip()
    {
        float turnAmountThisFrame = Input.GetAxisRaw("Horizontal")*turnSpeed;
        Quaternion turnOffset = Quaternion.Euler(0, turnAmountThisFrame, 0);
        rb.MoveRotation(rb.rotation * turnOffset);
    }

    public void SetSpeed(float speedIncreaseAmount)
    {
        moveSpeed += speedIncreaseAmount;
    }

    public void SetBoosters(bool canBoost)
    {
        isSpeed = canBoost;
        speedText.SetActive(canBoost);
        if (canBoost)
        {
            trail.startColor = powerUpSpeedColor;
            particles.startColor = powerUpSpeedColor;
            particles.Play();
        }
        else if (!canBoost)
            trail.startColor = originalStartColor;
    }

    public void SetInvincibility(bool invincible)
    {
        invincibleText.SetActive(invincible);
        canPassHazard = invincible;
        if (invincible)
        {
            particles.startColor = powerUpInvincibleColor;
            particles.Play();
        }
    }

    public void SetInvincibleColor(bool canInvincible)
    {
        if (canInvincible)
            trail.startColor = powerUpInvincibleColor;
        else if (!canInvincible)
            trail.startColor = originalStartColor;
    }

    public void Kill()
    {
        gameObject.SetActive(false);
    }
    void OnDrawGizmosSelected()
    {
        var joints = gameObject.GetComponents<CharacterJoint>();

        foreach (var item in joints)
        {
            Gizmos.color = Color.yellow;
            var connectedPos = item.connectedBody.gameObject.transform.position;
            Gizmos.DrawLine(transform.position, connectedPos);
            Gizmos.DrawCube(connectedPos, new Vector3(0.1f, 0.1f, 0.1f));
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Port"))
        {
            gameMan.win = true;
            
        }
    }

}
