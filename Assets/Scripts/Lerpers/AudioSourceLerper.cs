using System.Collections;
using UnityEngine;


public class AudioSourceLerper : ParameterLerper {

	// Use this for initialization
	protected override void Start () {
        base.Start();

        m_Parameters.Add(new ArrayList(new object[] { "LerpBool", "mute" }));
        m_Parameters.Add(new ArrayList(new object[] { "LerpFloat", "volume", 0f, 2f }));
        m_Parameters.Add(new ArrayList(new object[] { "LerpFloat", "pitch", -3f, 3f }));
        m_Parameters.Add(new ArrayList(new object[] { "LerpFloat", "pan", -1f, 1f }));
        m_Parameters.Add(new ArrayList(new object[] { "LerpFloat", "spatialBlend", 0f, 1f }));
        m_Parameters.Add(new ArrayList(new object[] { "LerpAudioClip", "clip", m_AudioClips }));

        m_Selection = GetComponent<AudioSource>();
        type = "AudioSourceLerper";

        StartCoroutine(StartLerp());
    }
	
}
