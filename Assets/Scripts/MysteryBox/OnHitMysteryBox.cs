using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class OnHitMysteryBox : MonoBehaviour
{
    [SerializeField] private UnityEvent _hit;
    public bool isHit = false;
    public Sprite emptySprite;
    public GameObject[] itemGameObjects;
    public enum TypeItem { Coin, Mobs }

    public TypeItem typeItem;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        var player = collision.collider.GetComponent<PlayerScript>();
        if (player && collision.contacts[0].normal.y > 0)
        {
            HitMysteryBox(collision);
        }
    }

    public void HitMysteryBox(Collision2D collision)
    {
        if (_hit != null && !isHit)
        {
            GetComponent<SpriteRenderer>().sprite = emptySprite;
            isHit = !isHit;
            StartCoroutine(Animate());
            StartCoroutine(ItemAnimate(collision));
        }
    }

    private IEnumerator Animate()
    {
        Debug.Log("Animated");
        Vector3 resetPosition = transform.localPosition;
        Vector3 animatePosition = resetPosition + Vector3.up * 0.5f;
        yield return Move(resetPosition, animatePosition, 0.125f, GetComponent<Transform>());
        yield return Move(animatePosition, resetPosition, 0.125f, GetComponent<Transform>());
    }

    private IEnumerator ItemAnimate(Collision2D collision)
    {
        Debug.Log("Animated");
        yield return HandleItemOnCollider(collision);
    }

    private IEnumerator Move(Vector3 from, Vector3 to, float duration, Transform handleTransformGameObject)
    {
        float elapsed = 0f;

        while (elapsed < duration)
        {
            float t = elapsed / duration;
            handleTransformGameObject.localPosition = Vector3.Lerp(from, to, t);
            elapsed += Time.deltaTime;
            yield return null;
        }
        handleTransformGameObject.localPosition = to;
    }

    public GameObject GetGameObject(string gameObjectName)
    {
        foreach (GameObject gameObject in itemGameObjects)
        {
            if (gameObject.name == gameObjectName) return gameObject;
        }
        return null;
    }

    public IEnumerator HandleItemOnCollider(Collision2D collision)
    {
        //Debug.Log("Enter HandleItemOnCollider");
        GameObject currentGameObject = Instantiate(GetGameObject(typeItem.ToString()));
        //Debug.Log("Current GameObject: " + currentGameObject.name);
        currentGameObject.transform.position = transform.position;
        switch (typeItem)
        {
            case TypeItem.Coin:
                {
                    Vector3 resetPosition = currentGameObject.transform.position;
                    Vector3 animtePosition = resetPosition + Vector3.up * 2.5f;
                    //handle animation of coin
                    yield return Move(resetPosition, animtePosition, 0.2f, currentGameObject.transform);
                    PlayerScript player = collision.collider.GetComponent<PlayerScript>();
                    //player.GetComponent<PlayerScript>().collectCoinSound.Play();
                    player.totalCoin++;
                    player.GetComponent<UIManager>().UpdateUI(player.totalCoin);
                    yield return Move(animtePosition, (resetPosition + Vector3.up * 1.5f), 0.2f, currentGameObject.transform);
                    // end handle animation of coin
                    // add coin for player
                    Debug.Log(player.totalCoin);
                    Destroy(currentGameObject);
                    break;
                }
            case TypeItem.Mobs:
                {
                    break;
                }
            default:
                {
                    break;
                }
        }
    }
}
