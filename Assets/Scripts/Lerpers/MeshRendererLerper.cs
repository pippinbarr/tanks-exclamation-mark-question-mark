using System.Collections;
using UnityEngine;
using UnityEngine.Rendering;


public class MeshRendererLerper : ParameterLerper {

	// Use this for initialization
	protected override void Start () {
        base.Start();

        m_Parameters.Add(new ArrayList(new object[] { "LerpEnum", "shadowCastingMode", "ShadowCastingMode.Off", "ShadowCastingMode.On", "ShadowCastingMode.ShadowsOnly", "ShadowCastingMode.TwoSided" }));
        m_Parameters.Add(new ArrayList(new object[] { "LerpMaterial", "materials" }));

        m_Selection = GetComponent<MeshRenderer>();
        MeshRenderer m = (MeshRenderer)m_Selection;
        //m.receiveShadows

        type = "MeshRenderer";

        StartCoroutine(StartLerp());

    }
	
}
