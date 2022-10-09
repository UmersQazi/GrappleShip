using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    Collider colliderToDeactivate;
    [SerializeField] GameObject visualsToDeactivate;
    [SerializeField] AudioSource collectAudio;

    private void Awake()
    {
        colliderToDeactivate = GetComponent<Collider>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            GameObject.Find("Game Manager").GetComponent<GameManager>().IncreaseMoney();
            DisableObject();
        }
    }

    public void DisableObject()
    {
        colliderToDeactivate.enabled = false;
        visualsToDeactivate.SetActive(false);
        collectAudio.Play();
    }

}
