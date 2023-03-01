using System.Collections;
using UnityEngine;

public class PipeDoorScript : MonoBehaviour
{
    public Transform endPoint;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.gameObject.CompareTag("Player"))
        {
            Debug.Log("Colli with player");
            GameObject player = collision.collider.gameObject;
            player.GetComponent<Collider2D>().enabled = false;
            player.GetComponent<CapsuleCollider2D>().enabled = false;
            if (player != null)
            {
                StartCoroutine(animationPlayer(player));
            }
        }
    }

    public IEnumerator animationPlayer(GameObject player)
    {
        yield return MoveDown(player);
    }

    public IEnumerator MoveDown(GameObject player)
    {
        float duration = 0.125f;
        float elapsed = 0;
        while (elapsed < duration)
        {
            float t = elapsed / duration;
            player.transform.localPosition = Vector3.Lerp(player.transform.position, endPoint.position, t);
            elapsed += Time.deltaTime;
            yield return null;
        }
        player.transform.position = endPoint.position;
        player.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
    }
}
