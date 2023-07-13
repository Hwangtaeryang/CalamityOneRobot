using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireParticleCtrl : MonoBehaviour
{
    public ParticleSystem fireParticle; //불 파티클

    int count = 0;


    void Start()
    {
        
    }

    
    void Update()
    {
        
    }

    private void OnParticleCollision(GameObject other)
    {
        if(other.CompareTag("Water"))
        {
            count += 1;
            Debug.Log("불이닷" + count);
            

            if (count == 50)
                fireParticle.startSize = 0.1f;
            else if (count == 100)
                fireParticle.startSize = 0.06f;
            else if (count == 150)
                fireParticle.gameObject.SetActive(false);
        }
    }
}
