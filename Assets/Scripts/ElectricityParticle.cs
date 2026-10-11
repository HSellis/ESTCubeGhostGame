using UnityEngine;

public class ElectricityParticle : MonoBehaviour
{

    public ParticleSystem electricityEffect;
    void Start()
    {
        
    }

    // Update is called once per frame
    public void PlayElectricityEffect(ParticleSystem electricityEffect, Vector3 position)
    {
        if (electricityEffect != null)
        {
            ParticleSystem effectInstance = Instantiate(electricityEffect, position, Quaternion.identity);
            effectInstance.Play();
            Destroy(effectInstance.gameObject, effectInstance.main.duration);
        }
    }
}
