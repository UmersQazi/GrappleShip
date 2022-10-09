using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reticle : MonoBehaviour
{
    [SerializeField] Color originalColor, hitColor;
    [SerializeField] LayerMask grappable;

    private void Awake()
    {
        originalColor = GetComponent<Renderer>().material.color;
    }

    private void OnTriggerEnter(Collider other)
    {
        
        if(other.gameObject.layer == grappable)
            GetComponent<Renderer>().material.color = hitColor;
        
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == grappable)
            GetComponent<Renderer>().material.color = originalColor;
    }

}
