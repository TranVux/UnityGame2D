using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantScript : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            var player = collision.gameObject;
            if (player != null)
            {
                player.GetComponent<PlayerScript>().DeathAnimation();
                player.GetComponent<PlayerScript>().deathSound.Play();
            }
        }
    }
}
