using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PowerupSpeed : MonoBehaviour
{
    [Header("Powerup Settings")]
    [SerializeField] float speedIncreaseAmount = 20;
    [SerializeField] float powerUpDuration = 5;


    [Header("Setup")]
    [SerializeField] GameObject visualsToDeactivate;

    Collider colliderToDeactivate = null;
    bool poweredUp = false;

    [SerializeField] AudioSource speedSound, loseSpeedSound;

    
    private void Awake()
    {
        colliderToDeactivate = GetComponent<Collider>();
    }

    private void OnTriggerEnter(Collider other)
    {
        PlayerShip playerShip = other.gameObject.GetComponent<PlayerShip>();
        
        if(playerShip != null && !poweredUp)
        {
            StartCoroutine(PowerUpSequence(playerShip));
        }
    }
    
    IEnumerator PowerUpSequence(PlayerShip playerShip)
    {
        poweredUp = true;

        ActivatePowerUp(playerShip);
        speedSound.Play();
        DisableObject();

        yield return new WaitForSeconds(powerUpDuration);

        DeactivatePowerUp(playerShip);
        loseSpeedSound.Play();
        EnableObject();

        poweredUp = false;
    }


    void ActivatePowerUp(PlayerShip playerShip)
    {
        if (playerShip != null)
        {
            playerShip.SetSpeed(speedIncreaseAmount);
            playerShip.SetBoosters(true);
        }
    }

    void DeactivatePowerUp(PlayerShip playerShip) {

        playerShip?.SetSpeed(-speedIncreaseAmount);

        playerShip?.SetBoosters(false);
    
    }

    public void DisableObject()
    {
        colliderToDeactivate.enabled = false;
        visualsToDeactivate.SetActive(false);
    }

    public void EnableObject()
    {
        colliderToDeactivate.enabled = true;
        visualsToDeactivate.SetActive(true);
    }

}
