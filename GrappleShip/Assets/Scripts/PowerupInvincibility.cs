using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerupInvincibility : MonoBehaviour
{
    [Header("Power Setup")]
    [SerializeField] float powerUpDuration;

    [SerializeField] GameObject visualsToDeactivate;

    bool poweredUp;
    Collider colliderToDeactivate = null;
    [SerializeField] AudioSource invincibleSound, loseInvincibleSound;
    private void Awake()
    {
        colliderToDeactivate = GetComponent<Collider>();
    }


    private void OnTriggerEnter(Collider other)
    {
        PlayerShip playerShip = other.GetComponent<PlayerShip>();
        

        if (playerShip != null && !poweredUp)
            StartCoroutine(InvinciblePowerUp(playerShip));

    }

    IEnumerator InvinciblePowerUp(PlayerShip playerShip)
    {
        poweredUp = true;

        playerShip.SetInvincibility(poweredUp);
        playerShip.SetInvincibleColor(true);
        invincibleSound.Play();
        DisableObject();

        yield return new WaitForSeconds(powerUpDuration);

        poweredUp = false;

        playerShip.SetInvincibility(poweredUp);
        playerShip.SetInvincibleColor(false);
        loseInvincibleSound.Play();
        EnableObject();
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
