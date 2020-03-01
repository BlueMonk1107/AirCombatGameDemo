using System;
using UnityEngine;

public class ShieldView : MonoBehaviour
{
   private void Start()
   {
      gameObject.AddComponent<ColliderComponent>();
      gameObject.AddComponent<AutoDestroyComponent>().Init(Const.SHIELD_TIME);
   }
}