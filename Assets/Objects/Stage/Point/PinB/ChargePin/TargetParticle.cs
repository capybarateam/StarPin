using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetParticle : MonoBehaviour
{
    private Transform target = null;

    public Transform Target
    {
        set { target = value; }
    }

    private ParticleSystem system = null;

    private ParticleSystem.Particle[] particles;

    // Start is called before the first frame update
    void Start()
    {
        particles = new ParticleSystem.Particle[100];
        this.GetComponent<ParticleSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        if (target)
        {
            if (system == null)
            {
                system = GetComponent<ParticleSystem>();
            }

            int count = system.GetParticles(particles);

            for (int i = 0; i < count; i++)
            {
                ParticleSystem.Particle particle = particles[i];

                float distance = Vector3.Distance(particle.position, target.position);

                Vector3 v1 = system.transform.TransformPoint(particle.position);
                Vector3 v2 = target.transform.position;

                //パーティクル生成残り時間に応じて距離をつめる
                float lifeDelta = 1.0f - (particle.remainingLifetime / particle.startLifetime);

                Vector3 dist = system.transform.InverseTransformPoint(Vector3.Lerp(v1, v2, lifeDelta));
                particle.position = dist;
                particles[i] = particle;

            }

            system.SetParticles(particles, count);
        }

    }
}
