using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpDownMovement : MonoBehaviour
{
    public Vector3 angle;
    public float speed;
    public float radius;

    private float startY;
    private float timeCounter;


    private void Start()
    {
        startY = transform.position.y;
        timeCounter = 0;
    }

    private void Update()
    {
        timeCounter += speed * Time.deltaTime;
        Vector3 pos = transform.position;
        pos.y = startY + radius * Mathf.Sin(timeCounter);
        transform.position = pos;
    }
}
