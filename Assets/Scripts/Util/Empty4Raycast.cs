using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Empty4Raycast : MaskableGraphic {

	public Empty4Raycast()
	{
		useLegacyMeshGeneration = false;
	}

	protected override void OnPopulateMesh(VertexHelper vp)
	{
		vp.Clear();
	}
}
