using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmoothLegAnim : MonoBehaviour
{
    public Transform groundMarker;
    public GameObject leg;
    public Transform legIKTarget;
    public Transform nextPosMarker;
    public Vector3 legIKPrevPos;
    public int layerMask;
    public float distFromTarget;
    public float maxDistFromTarget;
    public float maxDistFromMarker;
    public float paraHeight;
    public float animationTimer;
    public float animationTime;
    public bool isAnimating;
    public bool canAnimate;

    // Start is called before the first frame update
    void Start()
    {
        maxDistFromTarget = 4.567f;
        paraHeight = 1.2345f;

        isAnimating = false;
        animationTimer = 0;
        // Cast rays only against colliders in layer 8 aka ground
        layerMask = 1 << 8;
        legIKPrevPos = legIKTarget.position;
        maxDistFromMarker = transform.position.y - groundMarker.position.y;
    }

    // Update is called once per frame
    void Update()
    {
        if (isAnimating) {
            animationTimer += Time.deltaTime;
            legIKTarget.position = SampleParabola(legIKPrevPos, nextPosMarker.position, paraHeight, animationTimer / animationTime, 1);
            if (animationTimer >= animationTime) {
                isAnimating = false;
                animationTimer = 0;
            }
        } else {
            legIKPrevPos = legIKTarget.position;
        }
    }
    
    void FixedUpdate() {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, -transform.TransformDirection(Vector3.up), out hit, maxDistFromMarker, layerMask))
        {
            groundMarker.position = transform.position -transform.TransformDirection(Vector3.up) * hit.distance;
        }

        Debug.DrawLine(transform.position, transform.position - transform.TransformDirection(Vector3.up) * maxDistFromMarker, Color.red);
        Debug.DrawLine(leg.transform.position, groundMarker.position, Color.green);
        distFromTarget = Vector3.Distance(leg.transform.position, groundMarker.position);
        if (distFromTarget >= maxDistFromTarget) {
            nextPosMarker.position = new Vector3(groundMarker.position.x, groundMarker.position.y + maxDistFromMarker, groundMarker.position.z);
            if (canAnimate) {
                isAnimating = true;
            }
        }
    }


    // Stole from internet and modified for my needs :)
    #region Parabola sampling function
    /// <summary>
    /// Get position from a parabola defined by start and end, height, and time
    /// </summary>
    /// <param name='start'>
    /// The start point of the parabola
    /// </param>
    /// <param name='end'>
    /// The end point of the parabola
    /// </param>
    /// <param name='height'>
    /// The height of the parabola at its maximum
    /// </param>
    /// <param name='t'>
    /// Normalized time (0->1)
    /// </param>S
    Vector3 SampleParabola ( Vector3 start, Vector3 end, float height, float t, float time) {
        if (t >= time) {
            isAnimating = false;
            return end;
        }
        if ( Mathf.Abs( start.y - end.y ) < 0.1f ) {
            //start and end are roughly level, pretend they are - simpler solution with less steps
            Vector3 travelDirection = end - start;
            Vector3 result = start + t * travelDirection;
            result.y += Mathf.Sin( t * Mathf.PI ) * height;
            return result;
        } else {
            //start and end are not level, gets more complicated
            Vector3 travelDirection = end - start;
            Vector3 levelDirecteion = end - new Vector3( start.x, end.y, start.z );
            Vector3 right = Vector3.Cross( travelDirection, levelDirecteion );
            Vector3 up = Vector3.Cross( right, travelDirection );
            if ( end.y > start.y ) up = -up;
            Vector3 result = start + t * travelDirection;
            result += ( Mathf.Sin( t * Mathf.PI ) * height ) * up.normalized;
            return result;
        }
    }
    #endregion
}


