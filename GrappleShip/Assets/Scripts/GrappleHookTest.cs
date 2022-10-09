using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class GrappleHookTest : MonoBehaviour
{
    [SerializeField] LayerMask grappable;
    private Vector3 grapplePoint;
    LineRenderer lineRenderer;
    [SerializeField] Transform grappleTip, ship;
    public float maxDistance = 100f;
    SpringJoint joint;
    [SerializeField] GameObject player;
    [SerializeField] bool deployed;

    [Header("Joint Values")]
    [SerializeField] float spring = 4.5f;
    [SerializeField] float damper = 7f;
    [SerializeField] float massScale = 4.5f;
    [SerializeField] float hookDuration = 1.5f;

    PlayerShip playerShipScript;

    [SerializeField] AudioSource startGrapAudio, stopGrapAudio;



    public GameObject reticle;

    private void Awake()
    {
        lineRenderer = GetComponent<LineRenderer>();
        deployed = false;
        playerShipScript = GetComponent<PlayerShip>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && !deployed)
        {
            StartGrapple();
            
            
        }

        if (Input.GetKeyDown(KeyCode.V) && deployed)
        {
            //StartCoroutine(GrappleCountdown());
            StopGrapple();  
        }

        

    }

    private void LateUpdate()
    {
        DrawRope();
    }


    public void StartGrapple()
    {
        RaycastHit hit;
        if(Physics.Raycast(ship.position, ship.forward, out hit, maxDistance, grappable))
        {
            reticle.SetActive(false);
            grapplePoint = hit.point;
            joint = player.AddComponent<SpringJoint>();
            joint.autoConfigureConnectedAnchor = false;
            joint.connectedAnchor = grapplePoint;

            deployed = true;

            float distanceFromPoint = Vector3.Distance(player.transform.position, grapplePoint);

            joint.maxDistance = distanceFromPoint * 0.8f;
            joint.minDistance = distanceFromPoint * .25f;

            //Tweak these!
            joint.spring = spring;
            joint.damper = damper;
            joint.massScale = massScale;

            lineRenderer.positionCount = 2;

            startGrapAudio.Play();
        }
    }

    void DrawRope()
    {
        if (!joint) return;

        lineRenderer.SetPosition(0, grappleTip.position);   
        lineRenderer.SetPosition(1, grapplePoint);
    }

    IEnumerator GrappleCountdown()
    {
        yield return new WaitForSeconds(hookDuration);
        StopGrapple();
    }

    public void StopGrapple()
    {
        lineRenderer.positionCount = 0;
        Destroy(joint);
        deployed = false;
        reticle.SetActive(true);
        stopGrapAudio.Play();
    }


}
