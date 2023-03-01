using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudScript : MonoBehaviour
{
    // Start is called before the first frame update
    public float _cloudSpeed = 0f;
    private float _endPosX = 0f;

    // Update is called once per frame
    void Update()
    {
        if (_cloudSpeed == 0f)
        {
            _cloudSpeed = UnityEngine.Random.Range(0.8f, 1f);
        }

        if (_endPosX == 0f)
        {
            _endPosX = 10f;
        }

        transform.Translate(Vector3.right * Time.deltaTime * _cloudSpeed);

        if(gameObject.transform.position.x > _endPosX)
        {
            Destroy(gameObject);
        }
    }

    public void StartFloating(float cloudSpeed, float endPosX) {
        _cloudSpeed = cloudSpeed;    
        _endPosX = endPosX;
    }
}
