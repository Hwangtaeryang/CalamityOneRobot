using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterParticle : MonoBehaviour
{
    //public GameObject sphereParticleCollider;

    //List<ParticleSystem.Particle> enter = new List<ParticleSystem.Particle>();
    //List<ParticleSystem.Particle> neighbouringParicles = new List<ParticleSystem.Particle>();
    public ParticleSystem mainParticle;

    int count = 0;


    void Start()
    {
        mainParticle = GetComponent<ParticleSystem>();
    }

    
    void Update()
    {
        
    }

    private void OnParticleTrigger()
    {
        count+=1;
        Debug.Log("---물이닷" + count);
        //FindEneterdParticles();
        //FindNeighbouringParticles();
    }

    //void FindEneterdParticles()
    //{
    //    if(mainParticle == null)
    //    {
    //        mainParticle = GetComponent<ParticleSystem>();
    //    }
    //    ParticlePhysicsExtensions.GetTriggerParticles(mainParticle, ParticleSystemTriggerEventType.Enter, enter);
    //}

    //void FindNeighbouringParticles()
    //{
    //    Vector3 currentParticleCollider = sphereParticleCollider.transform.position;

    //    for(int i = 0; i<enter.Count; i++)
    //    {
    //        sphereParticleCollider.transform.position = enter[i].position;

    //        ParticlePhysicsExtensions.GetTriggerParticles(mainParticle, ParticleSystemTriggerEventType.Inside, neighbouringParicles);

    //        for(int j = 0; j < neighbouringParicles.Count; j++)
    //        {
    //            ParticleSystem.Particle neighbourParticles = neighbouringParicles[j];
    //            Debug.Log("Found neighbouring particles!");
    //        }
    //    }
    //    sphereParticleCollider.transform.position = currentParticleCollider;
    //}





    private void OnParticleCollision(GameObject other)
    {
        //if(other.CompareTag("Fire"))
        //{
        //    count += 1;
        //    mainParticle.startSize = 0.3f;
        //    Debug.Log("불이닷" + count);
        //}

        if (other.CompareTag("Water"))
        {
            count += 1;
            mainParticle.startSize = 0.3f;
            Debug.Log("불이닷" + count);
        }
    }
}
