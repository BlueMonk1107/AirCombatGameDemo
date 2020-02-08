using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AllBulletData
{
    public BulletData Player;
    public BulletData Enemy_Normal_0;
    public BulletData Enemy_Boss_0;
    public BulletData Enemy_Boss_1;
}

public class BulletData
{
    public double bulletSpeed;
    public PathType trajectoryType;
    public int[][] trajectory;
}
