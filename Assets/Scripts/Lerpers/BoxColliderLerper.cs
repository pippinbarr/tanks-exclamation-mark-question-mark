using System.Collections;
using UnityEngine;


public class BoxColliderLerper : ParameterLerper {

	// Use this for initialization
	protected override void Start () {
        base.Start();

        m_Parameters.Add(new ArrayList(new object[] { "LerpBool", "isTrigger" }));
        m_Parameters.Add(new ArrayList(new object[] { "LerpVector3", "center", 0.5f, 1.0f }));
        m_Parameters.Add(new ArrayList(new object[] { "LerpVector3", "size", 0.5f, 1.0f }));

        m_Selection = GetComponent<BoxCollider>();
        BoxCollider b = (BoxCollider)m_Selection;

        type = "BoxCollider";

        StartCoroutine(StartLerp());
    }
	
}
