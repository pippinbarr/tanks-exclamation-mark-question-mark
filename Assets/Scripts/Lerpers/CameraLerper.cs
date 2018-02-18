using System.Collections;
using UnityEngine;


public class CameraLerper : ParameterLerper {

	// Use this for initialization
	protected override void Start () {
        base.Start();

        m_Parameters.Add(new ArrayList(new object[] { "LerpEnum", "clearFlags", "CameraClearFlags.Color", "CameraClearFlags.Depth", "CameraClearFlags.Nothing", "CameraClearFlags.Skybox", "CameraClearFlags.SolidColor" }));
        m_Parameters.Add(new ArrayList(new object[] { "LerpColor", "backgroundColor" }));
        m_Parameters.Add(new ArrayList(new object[] { "LerpBool", "orthographic" }));
        m_Parameters.Add(new ArrayList(new object[] { "LerpFloat", "size", 0f, 100f }));
        m_Parameters.Add(new ArrayList(new object[] { "LerpFloat", "fieldOfView", 1f, 179f }));
        m_Parameters.Add(new ArrayList(new object[] { "LerpFloat", "nearClipPlane", 0f, 50f }));
        m_Parameters.Add(new ArrayList(new object[] { "LerpFloat", "farClipPlane", 50f, 100f }));
        m_Parameters.Add(new ArrayList(new object[] { "LerpRect", "rect", 0f, 1f }));

        m_Selection = GetComponent<Camera>();
        //Camera cam = (Camera)m_Selection;

        type = "Camera";

        StartCoroutine(StartLerp());
    }
	
}
