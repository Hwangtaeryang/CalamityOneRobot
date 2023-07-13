using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WebCam : MonoBehaviour
{
    public RawImage display;
    WebCamTexture camTexture;
    int currentIndex = 0;



    void Start()
    {
        //WebCamTexture web = new WebCamTexture(934, 705, 60);
        //GetComponent<MeshRenderer>().material.mainTexture = web;
        //web.Play();
        if(camTexture != null)
        {
            display.texture = null;
            camTexture.Stop();
            camTexture = null;
        }
        WebCamDevice device = WebCamTexture.devices[currentIndex];
        camTexture = new WebCamTexture(device.name);
        display.texture = camTexture;
        camTexture.Play();

    }

    
    void Update()
    {
        //if (UI_Manager.instance.realViewState)
        //    camTexture.Play();
        //else
        //    camTexture.Stop();
    }
}
