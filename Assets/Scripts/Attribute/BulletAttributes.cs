using System;
using System.Collections;
using System.Collections.Generic;
using NUnit.Framework.Constraints;
using UnityEngine;

[AttributeUsage(AttributeTargets.Class)]
public class BulletAttribute : Attribute
{
    public BulletType Type;

    public BulletAttribute(BulletType type)
    {
        Type = type;
    }
}
