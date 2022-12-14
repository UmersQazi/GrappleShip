using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HazardVolume : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        PlayerShip playerShip = other.gameObject.GetComponent<PlayerShip>();
        if(playerShip!= null && !playerShip.canPassHazard)
        {
            playerShip.Kill();
            GameObject.Find("Game Manager").GetComponent<GameManager>().gameOver = true;
        }
    }

}
