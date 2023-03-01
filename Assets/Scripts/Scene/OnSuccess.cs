using UnityEngine.SceneManagement;
using UnityEngine;

public class OnSuccess : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            //PlayerConstantManager.coins = CollectCoin.totalCoin;
            SceneManager.LoadScene("SceneLevel2");
        }
    }
}
