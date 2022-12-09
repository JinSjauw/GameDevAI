using System;
using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json.Serialization;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class SmokeBomb : MonoBehaviour
{
    public ParticleSystem _smokeVFX;
    private float _timer = 2f;
    private LayerMask invisibleLayer = 0;
    private LayerMask playerLayer = 6;
    private bool hasLanded = false;

    private GameObject player;
    private void Update()
    {
        _timer -= Time.deltaTime;
        if (transform.position.y < 0f || _timer < 0)
        {
            _timer = 2f;
            _smokeVFX.Play();
            hasLanded = true;
            GetComponent<Rigidbody>().isKinematic = true;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player")
        {
            if (hasLanded)
            {
                player = other.gameObject;
                player.layer = invisibleLayer;   
            }
        }
    }

    private void OnDisable()
    {
        if (player != null)
        {
            player.layer = playerLayer;
        }
    }
}
