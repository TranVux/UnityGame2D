using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurtleScript : MonoBehaviour
{
    public Rigidbody2D turtleRigidbody;
    public Animator turtleAnimator;
    public Transform wayPoint1;
    public Transform wayPoint2;
    public Transform currentWayPoint;
    public float speed = 3;
    public bool isFacingRight = false;

    public enum TurtleState { Alive, Death }
    public TurtleState turtleState;

    private void Awake()
    {
        currentWayPoint = wayPoint1;
        turtleState= TurtleState.Alive;
    }

    void Start()
    {
        //currentWayPoint = wayPoint1;
    }

    void Update()
    {
        turtleAnimator.SetBool("isRunning", true);

        if (Vector3.Distance(transform.position, currentWayPoint.position) < .1f)
        {
            if (currentWayPoint == wayPoint1)
            {
                currentWayPoint = wayPoint2;
                Flip();
            }
            else
            {
                currentWayPoint = wayPoint1;
                Flip();
            }
        }
        transform.position = Vector3.MoveTowards(transform.position, currentWayPoint.position, speed * Time.deltaTime);
    }

    public void Flip()
    {
        Vector3 newScale = transform.localScale;
        isFacingRight = !isFacingRight;
        newScale.x *= -1;
        transform.localScale = newScale;
    }
}
