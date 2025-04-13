using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class CameraFollowing : MonoBehaviour
{
    // Start is called before the first frame update
    public Transform target;
    public float lookAheadDistance;

    public float followSmooth;
    public float cameraZAngle;
    public int minDistanceToTarget = 1;
    public Vector3 velocity = Vector3.zero;
    float distanceToTarget;
    Vector3 vectorToTarget;
    void Start()
    {
        
    }
    
    private void FixedUpdate()
    {
        Vector3 targetPosition = target.TransformPoint(new Vector3(0, 2, cameraZAngle));
        vectorToTarget = transform.position - target.position;
        float distanceToTarget = Mathf.Sqrt(vectorToTarget.x * vectorToTarget.x + vectorToTarget.y * vectorToTarget.y + vectorToTarget.z * vectorToTarget.z);
        if(distanceToTarget <= minDistanceToTarget)
        {
            print("cerca");
            Vector3 minDistanceVector = vectorToTarget.normalized * minDistanceToTarget;
            Vector3 targetMinDistance = target.TransformPoint(minDistanceVector);
            transform.position = target.position + minDistanceVector;
        }
        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, followSmooth);
        transform.LookAt(target, Vector3.up);
    }
    
    
}
