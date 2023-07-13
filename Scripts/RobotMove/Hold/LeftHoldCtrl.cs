using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeftHoldCtrl : MonoBehaviour
{
    public static LeftHoldCtrl instance { get; private set; }



    public bool contactLeftState;

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
            contactLeftState = true;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Obstacle"))
        {
            contactLeftState = false;
        }
    }
}
