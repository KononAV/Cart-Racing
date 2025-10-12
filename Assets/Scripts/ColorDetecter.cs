using System;
using UnityEngine;
using UnityEngine.EventSystems;
using static UnityEngine.UI.Image;

public class ColorDetecter : MonoBehaviour
{
    ParticleSystem _particleSystem;
    public bool isPlaying = false;

    void Start()
    {
        _particleSystem = GetComponent<ParticleSystem>();
    }

    public void StartParticle()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, -transform.up, out hit, 10f))
        {
            Debug.Log("SH");
            Color color = hit.transform.gameObject.GetComponent<Renderer>().material.color;

            _particleSystem.GetComponent<Renderer>().material.color = color;
            Debug.DrawRay(transform.position, -transform.up, Color.red, 10);
            isPlaying = true;
        }
        else
        {
            Debug.DrawRay(transform.position, -transform.up, Color.green, 10);
        }
        _particleSystem.Play();
    }

    public void StopParticle()
    {
        _particleSystem?.Stop();
        isPlaying = false;
    }

    // Update is called once per frame
}
