using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] List<Transform> spawnPointList;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            GameObject.Instantiate(gameObject, spawnPointList[Random.Range(0,spawnPointList.Count)].position, Quaternion.identity);
            GameEvents.CoinAdded.Invoke();
            print("metocaste");
            Destroy(gameObject);
        }
    }
}
