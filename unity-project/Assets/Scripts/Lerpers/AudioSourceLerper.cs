using System.Collections;
using UnityEngine;


public class AudioSourceLerper : ParameterLerper {

	// Use this for initialization
	protected override void Start () {
        base.Start();

        m_Parameters.Add(new ArrayList(new object[] { "LerpBool", "mute" }));
        m_Parameters.Add(new ArrayList(new object[] { "LerpFloat", "volume", 0.5f, 1f, 0f, 1f }));
        m_Parameters.Add(new ArrayList(new object[] { "LerpFloat", "pitch", 1f, 2f, -3f, 3f }));
        m_Parameters.Add(new ArrayList(new object[] { "LerpFloat", "pan", 0.5f, 1f, -1f, 1f }));
        m_Parameters.Add(new ArrayList(new object[] { "LerpFloat", "spatialBlend", 0.5f, 1f, 0f, 1f }));
        m_Parameters.Add(new ArrayList(new object[] { "LerpAudioClip", "clip", m_AudioClips }));


        m_Selection = GetComponent<AudioSource>();
        type = "AudioSource";

        StartCoroutine(StartLerp());
    }
	
}
