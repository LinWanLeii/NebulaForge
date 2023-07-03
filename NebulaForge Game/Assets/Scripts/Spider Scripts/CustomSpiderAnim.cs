using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomSpiderAnim : MonoBehaviour
{
    public float speed;
    public Transform targetLocation;
    public Transform spiderBody;
    public GameObject frontLeftLeg;
    public GameObject frontRightLeg;
    public GameObject backLeftLeg;
    public GameObject backRightLeg;
    public Vector3 direction;
    public Quaternion targetRotation;
    public Quaternion prevRotation;
    public float rotationLerpTimer;
    public float rotationLerpSpeed;
    public float rotationAngleMargin;
    public float minDistFromTarget;
    public float legBodyOffset;

    // Start is called before the first frame update
    void Start()
    {
        legBodyOffset = spiderBody.transform.position.y - frontLeftLeg.transform.position.y;
        
    }

    // Update is called once per frame
    void Update()
    {
        direction = (targetLocation.position - spiderBody.transform.position).normalized;
        direction = new Vector3(direction.x, spiderBody.transform.position.y, direction.z);
        if (Vector3.Distance(targetLocation.position, spiderBody.position) > minDistFromTarget) {
            UpdatePos();

            targetRotation = Quaternion.LookRotation(-Vector3.Cross(transform.up, direction), transform.up);

            // If need to rotate towards the target
            if (Quaternion.Angle(targetRotation, spiderBody.transform.rotation) > rotationAngleMargin) {
                rotationLerpTimer += Time.deltaTime * rotationLerpSpeed;
                UpdateRotation(prevRotation, rotationLerpTimer);
            } else {
                rotationLerpTimer = 0;
                prevRotation = spiderBody.transform.rotation;
            }
        }

        // If left or right is animating
        // then the opposite cannot animate
        frontLeftLeg.GetComponent<SmoothLegAnim>().canAnimate = !frontRightLeg.GetComponent<SmoothLegAnim>().isAnimating;
        frontRightLeg.GetComponent<SmoothLegAnim>().canAnimate = !frontLeftLeg.GetComponent<SmoothLegAnim>().isAnimating;
        backLeftLeg.GetComponent<SmoothLegAnim>().canAnimate = !backRightLeg.GetComponent<SmoothLegAnim>().isAnimating;
        backRightLeg.GetComponent<SmoothLegAnim>().canAnimate = !backLeftLeg.GetComponent<SmoothLegAnim>().isAnimating;
    }

    void UpdatePos() {
        Vector3 newPos = spiderBody.transform.position + direction * speed * Time.deltaTime;

        float yAverage = frontLeftLeg.transform.position.y;
        yAverage += frontRightLeg.transform.position.y;
        yAverage += backLeftLeg.transform.position.y;
        yAverage += backRightLeg.transform.position.y;
        yAverage *= 0.25f;

        spiderBody.transform.position = new Vector3(newPos.x, spiderBody.transform.position.y, newPos.z);
    }
    void UpdateRotation(Quaternion _r, float _t) {
        spiderBody.transform.rotation = Quaternion.Lerp(_r, targetRotation, _t);
    }
}
