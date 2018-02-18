using System.Collections;
using UnityEngine;


public class TransformLerper : ParameterLerper {

	// Use this for initialization
	protected override void Start () {
        base.Start();

        m_Parameters.Add(new ArrayList(new object[] { "LerpPosition", "localPosition" }));
        m_Parameters.Add(new ArrayList(new object[] { "LerpRotation", "localRotation" }));
        m_Parameters.Add(new ArrayList(new object[] { "LerpScale", "localScale" }));

        m_Selection = GetComponent<Transform>();
        Transform t = (Transform)m_Selection;


        type = "Transform";

        StartCoroutine(StartLerp());
    }
	
}
