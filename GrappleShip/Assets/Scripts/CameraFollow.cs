using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    Vector3 offset;
    [SerializeField] Transform objectToFollow;
    // Start is called before the first frame update
    void Awake()
    {
        offset = transform.position - objectToFollow.position;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        transform.position = objectToFollow.position + offset;
    }
}
