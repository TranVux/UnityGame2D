using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectCoin : MonoBehaviour
{
    public int totalCoin = 0;

    private void Start()
    {
        
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Coin"))
        {
            totalCoin++;
            Debug.Log(totalCoin);
            Destroy(collision.gameObject);
        }
    }
}
