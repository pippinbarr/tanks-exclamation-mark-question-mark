using System.Collections;
using UnityEngine;


public class MeshFilterLerper : ParameterLerper {

	// Use this for initialization
	protected override void Start () {
        base.Start();

        m_Parameters.Add(new ArrayList(new object[] { "LerpMesh", "mesh", m_Meshes }));

        m_Selection = GetComponent<MeshFilter>();
        //MeshFilter m = (MeshFilter)m_Selection;

        type = "MeshFilter";

        StartCoroutine(StartLerp());
    }
	
}
