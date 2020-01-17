using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTypeComponent : MonoBehaviour {

	public EnemyType Type { get; private set; }

	public void Init(EnemyType type)
	{
		Type = type;
	}
}
