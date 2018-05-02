using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PathfindingParticles : MonoBehaviour {
    [SerializeField]
    public ParticleSystem modelToCopy;
    public Color color;
    public float timeToReach;
    float timer;

    Player player;

	// Use this for initialization
	void Start ()
    {
        timer = 4.9f;
        player = FindObjectOfType<Player>();
    }

    // Update is called once per frame
    void Update ()
    {
        timer += Time.deltaTime;
        if(timer > timeToReach)
        {
            ParticleSystem copy = Instantiate(modelToCopy);
            copy.name = "Particle System" + Time.unscaledTime;
            ParticleSystem.MainModule main = copy.GetComponent<ParticleSystem>().main;
            main.startColor = color;

           copy.trigger
                .SetCollider(0, player.GetComponent<BoxCollider>());

            copy.transform.SetParent(transform, false);
            timer = 0;
        }
    }
}
