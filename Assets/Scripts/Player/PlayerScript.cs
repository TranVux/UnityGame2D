using System.Collections;
using Unity.Netcode;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    [SerializeField] private Transform playerTransform;
    [SerializeField] private Transform checkGround;
    [SerializeField] private float speed;
    [SerializeField] private float jumpPower;
    [SerializeField] private Animator anim;
    [SerializeField] private Rigidbody2D mRigidbody;
    [SerializeField] private LayerMask groundLayerMark;
    [SerializeField] private LayerMask mysteryBoxMark;
    [SerializeField] private GameObject gameCamera;
    [SerializeField] private GameObject menuReplay;
    [SerializeField] private GameObject menuReplayClearWorld;

    public GameObject firework;
    public Transform fireworkPos;
    public GameObject transition;
    private bool isComplete;
    public AudioSource deathSound, collectCoinSound, jumpSound, clearStateSound, clearWorldSound;

    public bool isFacingRight = true;
    private float dirX;

    //total coins of player ingame
    public int totalCoin = 0;
    PlayerConstantManager playerConstantManager;


    //new run method
    private Vector2 direction;
    public float maxSpeed = 7f;

    //new jump method
    public float jumpDelay = 0.25f;
    public float jumpTimer;

    private void Start()
    {
        playerConstantManager = PlayerConstantManager.GetInstance();
        if (playerConstantManager.coins != -1)
        {
            totalCoin = playerConstantManager.coins;
        }
        isComplete = false;
    }
    // Update is called once per frame
    void Update()
    {
       if(!isComplete)
        {
            Jump();
            Run();
            Flip();
        }
    }

    private void FixedUpdate()
    {
        // for run function
        mRigidbody.velocity = new Vector2(dirX, mRigidbody.velocity.y);
    }

    bool IsGrounded()
    {
        return Physics2D.OverlapCircle(checkGround.position, .2f, groundLayerMark);
    }

    bool IsMysteryBox()
    {
        return Physics2D.OverlapCircle(checkGround.position, .2f, mysteryBoxMark);
    }

    public void Jump()
    {
        if ((Input.GetButtonDown("Jump") && IsGrounded()) || (Input.GetButtonDown("Jump") && IsMysteryBox()))
        {
            mRigidbody.AddForce(new Vector2(mRigidbody.position.x, jumpPower));
            jumpSound.Play();
        }

        if (!IsGrounded() && !IsMysteryBox())
        {
            anim.SetBool("isJumping", true);
        }
        else
        {
            anim.SetBool("isJumping", false);
        }
    }

    public void Run()
    {
        dirX = Input.GetAxisRaw("Horizontal") * speed;

        if ((IsGrounded() && (mRigidbody.velocity.x >= 0.0001f || mRigidbody.velocity.x <= -0.0001f)) ||
            (IsMysteryBox() && (mRigidbody.velocity.x >= 0.0001f || mRigidbody.velocity.x <= -0.0001f)))
        {
            anim.SetBool("isRunning", true);
        }
        else
        {
            anim.SetBool("isRunning", false);
        }
    }
    public void Flip()
    {
        if (isFacingRight && dirX > 0f || !isFacingRight && dirX < 0f)
        {
            isFacingRight = !isFacingRight;
            Vector3 playerLocalScale = transform.localScale;
            playerLocalScale.x *= -1;
            playerTransform.localScale = playerLocalScale;
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(checkGround.position, 0.2f);
    }

    public void Attack(Collider2D collision)
    {
        Debug.Log("Kill Turtle");
        if (collision.gameObject.CompareTag("TopCollider"))
        {
            var name = collision.attachedRigidbody.name;
            GameObject turtle = GameObject.Find(name);
            if (turtle != null && turtle.GetComponent<TurtleScript>().turtleState.Equals(TurtleScript.TurtleState.Alive))
            {
                turtle.GetComponent<Animator>().SetBool("beforeDeath", true);
                turtle.GetComponent<Rigidbody2D>().gravityScale = 1f;
                turtle.GetComponent<TurtleScript>().speed = 0;
                turtle.GetComponent<TurtleScript>().turtleState = TurtleScript.TurtleState.Death;
                //turtle.GetComponent<BoxCollider2D>().enabled = false;
                Debug.Log("Turtle Death");
            }
        }
    }

    public void DeathAnimation()
    {
        anim.SetBool("isRunning", false);
        StartCoroutine(Animated());
    }

    public IEnumerator Animated()
    {
        //yield return new WaitForSeconds(.3f);
        GetComponent<BoxCollider2D>().enabled = false;
        GetComponent<CapsuleCollider2D>().enabled = false;
        gameCamera.GetComponent<CameraFollowScript>().isFollow = false;
        Vector3 resetPos = transform.position;
        Vector3 targetPos = resetPos + Vector3.up * 1.5f;
        yield return MoveTop(resetPos, targetPos);
        menuReplay.GetComponent<MenuReplay>().DisableResumeButton();
        menuReplay.GetComponent<MenuReplay>().Open(totalCoin, "Game Over");
        yield return MoveTop(targetPos, resetPos + Vector3.down * 2f);
        GameObject.Find(this.name).SetActive(false);
    }

    public IEnumerator MoveTop(Vector3 from, Vector3 to)
    {
        float currentDurationAnim = 0f;
        float animDuration = 0.4f;

        while (currentDurationAnim < animDuration)
        {
            float t = currentDurationAnim / animDuration;
            transform.localPosition = Vector3.Lerp(from, to, t);
            currentDurationAnim += Time.deltaTime;
            yield return null;
        }

        transform.localPosition = to;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Finish Level
        if (collision.gameObject.CompareTag("Finish"))
        {
            playerConstantManager.coins = totalCoin;
            Instantiate(firework, fireworkPos.position, fireworkPos.rotation);
            StartCoroutine(AnimatedClearState());
        }

        //Clear level
        if (collision.gameObject.CompareTag("ClearWorld"))
        {
            playerConstantManager.coins = totalCoin;
            Instantiate(firework);
            StartCoroutine(AnimatedClearWorld());
        }

        //collect coin
        if (collision.gameObject.CompareTag("Coin"))
        {
            totalCoin++;
            collectCoinSound.Play();
            GetComponent<UIManager>().UpdateUI(totalCoin);
            Debug.Log(totalCoin);
            Destroy(collision.gameObject);
        }

        //attack turtle
        Attack(collision);

        if (collision.gameObject.CompareTag("BodyCollider"))
        {
            //game over | reload level
            DeathAnimation();
            deathSound.Play();
            Debug.Log("Player Death");
        }
    }

    public IEnumerator AnimatedClearState()
    {
        clearStateSound.Play();
        this.GetComponent<SpriteRenderer>().material.color = new Color(0f, 0f, 0f, 0f);
        isComplete = true;
        yield return new WaitForSeconds(5.641f);
        //SceneManager.LoadScene("SceneLevel2");
        transition.GetComponent<LevelLoaderScript>().LoadNextLevel();
    }

    public IEnumerator AnimatedClearWorld()
    {
        clearWorldSound.Play();
        menuReplayClearWorld.GetComponent<MenuReplay>().Open(totalCoin, "Congratulation");
        this.GetComponent<SpriteRenderer>().material.color = new Color(0f, 0f, 0f, 0f);
        isComplete = true;
        yield return null;
    }
}
