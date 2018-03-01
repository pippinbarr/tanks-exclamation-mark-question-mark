using System.Collections;
using UnityEngine;


public class CameraLerper : ParameterLerper
{

    // Use this for initialization
    protected override void Start()
    {
        base.Start();

        #if UNITY_STANDALONE
        m_Parameters.Add(new ArrayList(new object[] { "LerpEnum", "clearFlags", "CameraClearFlags.Color", "CameraClearFlags.Depth", "CameraClearFlags.Nothing", "CameraClearFlags.Skybox", "CameraClearFlags.SolidColor" }));
        #endif

        m_Parameters.Add(new ArrayList(new object[] { "LerpColor", "backgroundColor" }));
        m_Parameters.Add(new ArrayList(new object[] { "LerpBool", "orthographic" }));
        m_Parameters.Add(new ArrayList(new object[] { "LerpFloat", "size", 1f, 2f, 0.01f, 300f }));
        m_Parameters.Add(new ArrayList(new object[] { "LerpFloat", "fieldOfView", 20f, 40f, 1f, 179f }));

        #if UNITY_STANDALONE
        m_Parameters.Add(new ArrayList(new object[] { "LerpEnum", "clearFlags", "CameraClearFlags.Color", "CameraClearFlags.Depth", "CameraClearFlags.Nothing", "CameraClearFlags.Skybox", "CameraClearFlags.SolidColor" }));
        m_Parameters.Add(new ArrayList(new object[] { "LerpFloat", "nearClipPlane", 10f, 30f, 0.01f, 70f }));
        m_Parameters.Add(new ArrayList(new object[] { "LerpFloat", "farClipPlane", 10f, 30f, 60f, 150f }));
        #endif

        m_Parameters.Add(new ArrayList(new object[] { "LerpRect", "rect", 0.1f, 0.2f }));

        m_Selection = GetComponent<Camera>();

        type = "Camera";

        StartCoroutine(StartLerp());
    }
	
}
