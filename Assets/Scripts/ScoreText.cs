using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreText : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] float score;
    [SerializeField] float scoreValue;
    [SerializeField] RectTransform addScoreTextTransform;
    [SerializeField] RectTransform ScoreTextTransform;
    [SerializeField] TMP_Text scoreTextTMPText;
    [SerializeField] float animationDuration;
    [SerializeField] Transform transformPointA;
    [SerializeField] Transform transformPointB;
    [SerializeField] Transform scoreTransformPoint;
    [SerializeField] Vector3 addScoreNumberScaleA;
    [SerializeField] Vector3 addScoreNumberScaleB;
    float time;
    float normalicedTime;
    void Start()
    {
        GameEvents.CoinAdded.AddListener(AddScore);
        GameEvents.ResetGame.AddListener(Reset);
        scoreTextTMPText.gameObject.SetActive(false);
        addScoreTextTransform.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator ScoreTextAnimation()
    {
        while(time <= animationDuration)
        {
            scoreTextTMPText.gameObject.SetActive(true);
            addScoreTextTransform.gameObject.SetActive(true);
            time += Time.deltaTime;
            normalicedTime = time / animationDuration;
            addScoreTextTransform.position = Vector3.Lerp(transformPointA.position, transformPointB.position, normalicedTime);
            addScoreTextTransform.localScale = Vector3.Lerp(addScoreNumberScaleA, addScoreNumberScaleB, normalicedTime);
            ScoreTextTransform.position = scoreTransformPoint.position;
            scoreTextTMPText.text = score.ToString();            
            yield return null;
        }
        time = 0;
        normalicedTime = 0;
        scoreTextTMPText.gameObject.SetActive(false);
        addScoreTextTransform.gameObject.SetActive(false);
    }
    private void AddScore() 
    {
        score += scoreValue;
        StartCoroutine(ScoreTextAnimation());
    }

    private void Reset()
    {
        score = 0;
        scoreTextTMPText.text = score.ToString();
    }



}
