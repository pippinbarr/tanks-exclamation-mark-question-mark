using System.Collections;
using UnityEngine;


public class TransformLerper : ParameterLerper {

	// Use this for initialization
	protected override void Start () {
        base.Start();

        m_Parameters.Add(new ArrayList(new object[] { "LerpVector3", "localPosition", 5.0f, 10.0f }));
        m_Parameters.Add(new ArrayList(new object[] { "LerpRotation", "localRotation", -30f, 30f }));
        m_Parameters.Add(new ArrayList(new object[] { "LerpVector3", "localScale", 0.5f, 1.0f }));

        m_Selection = GetComponent<Transform>();

        type = "Transform";

        StartCoroutine(StartLerp());
    }
	
}
