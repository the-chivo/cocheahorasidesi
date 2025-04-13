using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EndGameText : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] TMP_Text endGameText;
    bool resetIsOn = false;
    
    void Start()
    {
        endGameText.gameObject.SetActive(false);
        GameEvents.EndGame.AddListener(EndGame);
        GameEvents.ResetGame.AddListener(ResetGame);
    }

    // Update is called once per frame
    void Update()
    {
       if(resetIsOn == true)
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                GameEvents.ResetGame.Invoke();
            }
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Obstacle"))
        {
            GameEvents.EndGame.Invoke();
        }
    }
    private void EndGame()
    {      
        endGameText.gameObject.SetActive(true);
        resetIsOn = true;     
    }
    private void ResetGame()
    {
        endGameText.gameObject.SetActive(false);
    }
}
