using UnityEngine;

public class CameraFollowScript : MonoBehaviour
{
    public bool isFollow = true;
    public float followSpeed = 2f;
    public Transform target;
    public Transform maxX;
    public Transform minX;

    // Update is called once per frame
    void Update()
    {
        Vector3 newPos;
        if(isFollow)
        {
            if (target.position.x >= minX.position.x && target.position.x <= maxX.position.x)
            {
                newPos = new Vector3(target.position.x + 2f, target.position.y + 1f, -10f);
            }
            else
            {
                newPos = new Vector3(transform.position.x, target.position.y + 1f, -10f);
            }
            transform.position = Vector3.Slerp(transform.position, newPos, followSpeed * Time.deltaTime);
        }
    }
}
