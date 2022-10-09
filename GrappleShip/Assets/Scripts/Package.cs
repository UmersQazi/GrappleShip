using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Package : MonoBehaviour
{
    public float durability;
    float barSubtraction;
    [SerializeField] GameManager gameMan;
    [SerializeField] AudioSource audio;
    private void Awake()
    {
        barSubtraction = (1 / durability);
    }
    // Start is called before the first frame update
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Debris"))
        {
            durability--;
            gameMan.durabilityBar.fillAmount -= barSubtraction;
            audio.Play();
            
        }
        if (collision.gameObject.CompareTag("Port"))
        {
            gameMan.win = true;
            
        }
    }
}
