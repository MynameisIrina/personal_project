using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RainController : MonoBehaviour
{
    public AudioSource thunderSound;
    public AudioSource rainSound;
    private bool trigger;
    private float timer;
    private float timerSound;
    public ParticleSystem RainFallParticleSystem;
    private float counter;

    public List<Light> lights;
    float min = 0.22f; // min intensity
    float max = 1; // max intensity

    private void Start()
    {
        trigger = false;
        RainFallParticleSystem.Stop();
    }


    IEnumerator LightIntensityOff()
    {
        while (counter < 250f)
        {
            counter += Time.deltaTime;

            foreach (var light in lights)
            {
                light.intensity = Mathf.Lerp(max, min, counter / 250f);
            }
            yield return null;
        }
        RainFallParticleSystem.Play();
        StartCoroutine(SlowlyStartRain());

    }

    IEnumerator SlowlyStartRain()
    {
        while (timer < 100f)
        {
            timer += Time.deltaTime;
            float normalizedTime = timer / 100f;
            var emission = RainFallParticleSystem.emission;
            emission.rateOverTime = normalizedTime * 100;
            rainSound.volume = normalizedTime * 0.15f;
            yield return null;
        }
    }

    private void Update()
    {
        if (trigger)
        {
            StartCoroutine(LightIntensityOff());
        } 
    }


    private void OnTriggerEnter(Collider other)
    {
        trigger = true;
        thunderSound.Play();
        rainSound.Play();
    }

    private void OnTriggerExit(Collider other)
    {
        GetComponent<BoxCollider>().enabled = false;
    }
}
