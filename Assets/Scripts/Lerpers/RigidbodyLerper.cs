using System.Collections;
using UnityEngine;


public class RigidbodyLerper : ParameterLerper {

	// Use this for initialization
	protected override void Start () {
        base.Start();

        m_Parameters.Add(new ArrayList(new object[] { "LerpFloat", "mass", 0f, 100f }));
        m_Parameters.Add(new ArrayList(new object[] { "LerpFloat", "drag", 0f, 100f }));
        m_Parameters.Add(new ArrayList(new object[] { "LerpFloat", "angularDrag", 0f, 100f }));
        m_Parameters.Add(new ArrayList(new object[] { "LerpBool", "useGravity" }));
        m_Parameters.Add(new ArrayList(new object[] { "LerpBool", "isKinematic" }));
        m_Parameters.Add(new ArrayList(new object[] { "LerpEnum", "constraints", "RigidbodyConstraints.FreezeAll", "RigidbodyConstraints.FreezePosition", "RigidbodyConstraints.FreezePositionX", "RigidbodyConstraints.FreezePositionY", "RigidbodyConstraints.FreezePositionZ", "RigidbodyConstraints.FreezeRotation", "RigidbodyConstraints.FreezeRotationX", "RigidbodyConstraints.FreezeRotationY", "RigidbodyConstraints.FreezeRotationZ", "RigidbodyConstraints.None"}));

        m_Selection = GetComponent<Rigidbody>();

        type = "Rigidbody";

        StartCoroutine(StartLerp());
    }
	
}
