﻿using System.Collections;
using UnityEngine;


public class CapsuleColliderLerper : ParameterLerper {

	// Use this for initialization
	protected override void Start () {
        base.Start();

        m_Parameters.Add(new ArrayList(new object[] { "LerpBool", "isTrigger" }));
        m_Parameters.Add(new ArrayList(new object[] { "LerpVector3", "center", 0.5f, 1.0f }));
        m_Parameters.Add(new ArrayList(new object[] { "LerpFloat", "radius", 0.5f, 1.0f }));
        m_Parameters.Add(new ArrayList(new object[] { "LerpFloat", "height", 0.5f, 1.0f }));
        m_Parameters.Add(new ArrayList(new object[] { "LerpInt", "direction", 0, 2}));

        m_Selection = GetComponent<CapsuleCollider>();

        type = "CapsuleCollider";

        StartCoroutine(StartLerp());
    }
	
}