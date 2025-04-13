using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreBar : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] RectTransform scoreBar;
    [SerializeField] float animationDuration;
    [SerializeField] Vector3 barAScale;
    [SerializeField] Vector3 barBScale;
    [SerializeField] float animationPickDuration;
    float time;
    float normalicedTime;
    void Start()
    {
        GameEvents.CoinAdded.AddListener(VarAnimationFunction);
        scoreBar.localScale = new Vector3(0,0,0);
    }

    // Update is called once per frame
    void Update()
    {

    }
    void VarAnimationFunction()
    {
        StartCoroutine(VarAnimation());
    }
    IEnumerator VarAnimation()
    {
        while(time <= animationDuration)
        {
            time += Time.deltaTime;
            normalicedTime = time / animationDuration;
            scoreBar.localScale = Vector3.Lerp(barAScale, barBScale, normalicedTime);
            yield return null;
        }
        time = 0;
        normalicedTime = 0;
        while(time <= animationDuration)
        {
            time += Time.deltaTime;
            scoreBar.localScale = barBScale;
            yield return null;
        }
        time = 0;
        while(time <= animationDuration)
        {
            time += Time.deltaTime;
            normalicedTime = time / animationDuration;
            scoreBar.localScale = Vector3.Lerp(barBScale, barAScale, normalicedTime);
            yield return null;
        }
        time = 0;
        normalicedTime = 0;
    }
}
