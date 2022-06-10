using System.Collections;
using UnityEngine;
using UnityEngine.Rendering;


public class ParticleSystemLerper : ParameterLerper {

	// Use this for initialization
	protected override void Start () {
        base.Start();

        Debug.Log("Starting ParticleSystemLerper...");

        m_Selection = GetComponent<ParticleSystem>();
        ParticleSystem p = (ParticleSystem)m_Selection;


        m_Parameters.Add(new ArrayList(new object[] { "LerpMainParticleSystemModule", "main" }));
        if (p.emission.enabled) m_Parameters.Add(new ArrayList(new object[] { "LerpEmissionParticleSystemModule", "emission" }));
        if (p.shape.enabled) m_Parameters.Add(new ArrayList(new object[] { "LerpShapeParticleSystemModule", "shape" }));
        //if (p.velocityOverLifetime.enabled) m_Parameters.Add(new ArrayList(new object[] { "LerpVelocityOverLifetimeParticleSystemModule" }));
        //if (p.sizeOverLifetime.enabled) m_Parameters.Add(new ArrayList(new object[] { "LerpSizeOverLifetimeParticleSystemModule" }));
        //if (p.sizeBySpeed.enabled) m_Parameters.Add(new ArrayList(new object[] { "LerpSizeBySpeedParticleSystemModule" }));
        //if (p.subEmitters.enabled) m_Parameters.Add(new ArrayList(new object[] { "LerpSubEmittersParticleSystemModule" }));
        //if (p.colorOverLifetime.enabled) m_Parameters.Add(new ArrayList(new object[] { "LerpColorOverLifetimeParticleSystemModule" }));

        type = "ParticleSystem";

        StartCoroutine(StartLerp());

    }
	
}
