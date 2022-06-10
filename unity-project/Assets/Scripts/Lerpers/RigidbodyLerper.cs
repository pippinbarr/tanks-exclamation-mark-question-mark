using System.Collections;
using UnityEngine;


public class RigidbodyLerper : ParameterLerper
{

    // Use this for initialization
    protected override void Start()
    {
        base.Start();

        m_Parameters.Add(new ArrayList(new object[] { "LerpFloat", "mass", 0.5f, 5f, 0f, 1000f }));
        m_Parameters.Add(new ArrayList(new object[] { "LerpFloat", "drag", 1f, 10f, 0f, 1000f }));
        m_Parameters.Add(new ArrayList(new object[] { "LerpFloat", "angularDrag", 1f, 10f, 0f, 1000f }));
        m_Parameters.Add(new ArrayList(new object[] { "LerpBool", "useGravity" }));

        #if UNITY_STANDALONE
        m_Parameters.Add(new ArrayList(new object[] { "LerpBool", "isKinematic" }));
        #endif

        m_Parameters.Add(new ArrayList(new object[] { "LerpEnum", "constraints", "RigidbodyConstraints.FreezeAll", "RigidbodyConstraints.FreezePosition", "RigidbodyConstraints.FreezePositionX", "RigidbodyConstraints.FreezePositionY", "RigidbodyConstraints.FreezePositionZ", "RigidbodyConstraints.FreezeRotation", "RigidbodyConstraints.FreezeRotationX", "RigidbodyConstraints.FreezeRotationY", "RigidbodyConstraints.FreezeRotationZ", "RigidbodyConstraints.None"}));

        m_Selection = GetComponent<Rigidbody>();

        type = "Rigidbody";

        StartCoroutine(StartLerp());
    }
	
}
