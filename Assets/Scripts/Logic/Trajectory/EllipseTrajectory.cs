using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EllipseTrajectory : ITrajectory
{
    private EllipseData _data;
    private float _perimeter;
    private Vector3[] _posList;
    
    public void Init(ITrajectoryData data)
    {
        if (data is EllipseData)
        {
            _data = data as EllipseData;
        }
        else
        {
            Debug.LogError("当前数据不是EllipseData类型，类型为:"+data);
            return;
        }
        InitPos(_data);
    }

    private void InitPos(EllipseData data)
    {
        int count = GetVailCount(data.Precision);
        float xLeft = data.Center.x - data.XRadius;
        float xRight = data.Center.x + data.XRadius;
        
        //x轴上的坐标分成多少份，举例，顶点为4个，x轴上坐标要被分成2份
        int partCount = count / 2;
        int xPointCount = partCount + 1;
        float x = xLeft;
        float[] y;
        _posList = new Vector3[count];
        float xOffset = data.XRadius * 2 / partCount;
        
        for (int i = 0; i < xPointCount; i++)
        {
            y = GetY(x, Vector2.zero);
            
            if (y[0] > 0)
            {
                _posList[i] = new Vector3(x, y[0]);
                _posList[_posList.Length - i] = new Vector3(x, y[1]);
            }
            else if(y[0] < 0)
            {
                _posList[i] = new Vector3(x, y[1]);
                _posList[_posList.Length - i] = new Vector3(x, y[0]);
            }
            else
            {
                _posList[i] = new Vector3(x, y[0]);
            }

            x += xOffset;
        }
    }

    private int GetVailCount(int count)
    {
        if (count < 4)
        {
            return 4;
        }
        else
        {
            if (count % 4 == 0)
            {
                return count;
            }
            else
            {
                return (count / 4)*4 + 4;
            }
        }
    }

    public float[] GetY(float x, Vector2 startPos)
    {
        if (_data == null)
            return null;

        //a是x轴半径，b是y轴半径，中心的是（h,k）
        //sqrtValue = (a^2*b^2 - b*(x - h))^0.5
        float sqrtValue = Mathf.Pow(
            Mathf.Abs(Mathf.Pow(_data.XRadius * _data.YRadius, 2) - Mathf.Pow(_data.YRadius * (x - _data.Center.x), 2))
            , 0.5f);
        
        // y = (+-sqrtValue + a * k)/a
        float[] temp = new float[2];
        temp[0] = (sqrtValue+ _data.XRadius * _data.Center.y)/_data.XRadius;
        temp[1] = (-sqrtValue+ _data.XRadius * _data.Center.y)/_data.XRadius;
        return temp;
    }

    public float[] GetX(float y, Vector2 startPos)
    {
        if (_data == null)
            return null;
        
        //a是x轴半径，b是y轴半径，中心的是（h,k）
        //sqrtValue = (a^2*b^2 - b*(y - k))^0.5
        float sqrtValue = Mathf.Pow(
            Mathf.Abs(Mathf.Pow(_data.XRadius * _data.YRadius, 2) - Mathf.Pow(_data.XRadius * (y - _data.Center.y), 2))
            , 0.5f);
        
        // x = (+-sqrtValue + b*h)/b
        float[] temp = new float[2];
        temp[0] = (sqrtValue + _data.YRadius * _data.Center.x)/_data.YRadius;
        temp[1] = (-sqrtValue + _data.YRadius * _data.Center.x)/_data.YRadius;
          
        return temp;
    }

    public Vector2 GetDirection()
    {
        Debug.LogError("椭圆形轨迹方向无法在轨迹类中获取");
        return Vector2.zero;
    }

    public float GetZRotate()
    {
        return 0;
    }
}
