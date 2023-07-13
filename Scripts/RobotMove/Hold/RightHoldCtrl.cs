using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RightHoldCtrl : MonoBehaviour
{
    public static RightHoldCtrl instance { get; private set; }


    public bool contactRightState;


    private void Awake()
    {
        if (instance != null)
            Destroy(this);
        else
            instance = this;
    }

    void Start()
    {
        
    }

    
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Obstacle"))
        {
            contactRightState = true;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Obstacle"))
        {
            contactRightState = false;
        }
    }
}
