using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangeDetector : MonoBehaviour
{
    public event Action<Collider> OnRangeEnter;
    public event Action<Collider> OnRangeStay;
    public event Action<Collider> OnRangeExit;
    [SerializeField] float mRadius;
    private SphereCollider mCollider;

    private void Awake()
    {
        mCollider = GetComponent<SphereCollider>();
    }

    // Start is called before the first frame update
    void Start()
    {
        //Debug.Log(mCollider.radius);
        mCollider.radius = mRadius;
        //Debug.Log(mCollider.radius);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void SetRadius(float newRadius)
    {
        mCollider.radius = newRadius;
    }

    private void OnTriggerEnter(Collider other)
    {
        OnRangeEnter?.Invoke(other);
        Debug.Log("exp collider enter " + other.gameObject.name);
        //if (other.tag == "Exp")
        //{
        //    Debug.Log("exp in range");
        //    other.gameObject.GetComponent<TargetMovement>().enabled = true;
        //}
    }

    private void OnTriggerStay(Collider other)
    {
        OnRangeStay?.Invoke(other);
    }

    private void OnTriggerExit(Collider other)
    {
        OnRangeExit?.Invoke(other);
        //Debug.Log("exp collider exit " + other.gameObject.name);
    }

}
