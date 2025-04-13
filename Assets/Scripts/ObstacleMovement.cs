using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    // Start is called before the first frame update
    float time;
    [SerializeField] Transform transformPointA;
    [SerializeField] Transform transformPointB;
    [SerializeField] float animationDuration; 
    [SerializeField] AnimationCurve ease;
    [SerializeField] AnimationCurve esae2;
    [SerializeField] GameObject obstacle;
    float normalicedTime;
    [SerializeField] bool animationIsOn;

    void Start()
    {
        StartCoroutine(Animation());
    }
    IEnumerator Animation()
    {
        while(animationIsOn == true)
        {
            while(time <= animationDuration)
            {
                time += Time.deltaTime;
                normalicedTime = time / animationDuration;
                obstacle.transform.position = Vector3.Lerp(transformPointA.position, transformPointB.position, ease.Evaluate(normalicedTime));
                yield return null;
            }
            time = 0;
            normalicedTime = 0;
            while(time <= animationDuration)
            {
                time += Time.deltaTime;
                normalicedTime = time / animationDuration;
                obstacle.transform.position = Vector3.Lerp(transformPointB.position, transformPointA.position, esae2.Evaluate(normalicedTime));
                yield return null;
            }
            time = 0;
            normalicedTime = 0;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
