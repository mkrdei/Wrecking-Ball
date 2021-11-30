using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleEffects : MonoBehaviour
{
    Hashtable particleEffects;
    // Start is called before the first frame update
    void Awake()
    {
        particleEffects = new Hashtable();
        for(int i = 0; i<transform.childCount; i++)
        {
            particleEffects.Add(transform.GetChild(i).name, transform.GetChild(i).GetComponent<ParticleSystem>());
            
        }
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("Particle: " + ((ParticleSystem)particleEffects["Flaming"]));
    }

    public void playParticle(string particleName)
    {
        if (!((ParticleSystem)particleEffects[particleName]).isPlaying)
        {
            ((ParticleSystem)particleEffects[particleName]).Play();
        }
    }
    public void stopParticle(string particleName)
    {
        /*if (((ParticleSystem)particleEffects[particleName])!=null)
        {
            if (((ParticleSystem)particleEffects[particleName]).isPlaying)
            {
                ((ParticleSystem)particleEffects[particleName]).Stop();
            }
        }*/
        
        
    }
    
}
