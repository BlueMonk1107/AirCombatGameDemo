using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EllipseTrajectory : ITrajectory
{
    private EllipseData _data;
    private float _perimeter;
    private Vector3[] _posList;
    private int _middleIndex;
    private int _curIndex;
    private float _xRadius;
    private float _yRadius;
    
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

        _xRadius = (float) _data.XRadius;
        _yRadius = (float) _data.YRadius;
        InitPos(_data);
        _middleIndex = _posList.Length / 2;
        _curIndex = GetStartIndex(_posList,_data.StartPos);
    }

    private void InitPos(EllipseData data)
    {
        int count = GetVailCount(data.Precision);
        float xLeft = data.Center.x - _xRadius;
        
        //x轴上的坐标分成多少份，举例，顶点为4个，x轴上坐标要被分成2份
        int partCount = count / 2;
        int xPointCount = partCount + 1;
        float x = xLeft;
        float[] y;
        _posList = new Vector3[count];
        float xOffset = _xRadius * 2 / partCount;
        
        for (int i = 0; i < xPointCount; i++)
        {
            y = GetY(x, Vector2.zero);

            if (Mathf.Abs(y[0] - _data.Center.y)<0.01f)
            {
                _posList[i] = new Vector3(x, y[0]);
            }
            else if (y[0] > _data.Center.y)
            {
                _posList[i] = new Vector3(x, y[0]);
                _posList[_posList.Length - i] = new Vector3(x, y[1]);
            }
            else if(y[0] < _data.Center.y)
            {
                _posList[i] = new Vector3(x, y[1]);
                _posList[_posList.Length - i] = new Vector3(x, y[0]);  
            }

            x += xOffset;
        }
    }

    private int GetStartIndex(Vector3[] posList,Vector2 startPos)
    {
        int index = 0;
        float distance = 0;
        float oldDistance = 0;

        int offset = posList.Length / 4;
        List<KeyValuePair<int,float>> points = new List<KeyValuePair<int, float>>(4);
        for (int i = 0; i < 4; i++)
        {
            distance = Vector3.Distance(posList[index], startPos);
            points.Add(new KeyValuePair<int, float>(index,distance));

            index += offset;
        }

        points = points.OrderBy(pair => pair.Value).ToList();
        points.RemoveRange(2,points.Count -2);
        points = points.OrderBy(pair => pair.Key).ToList();

        bool mark = true;
        index = points[0].Key;
        oldDistance = Vector3.Distance(posList[index], startPos);
        for (int i = points[0].Key + 1; i <= points[1].Key; i++)
        {
            distance = Vector3.Distance(posList[i], startPos);
            mark = oldDistance > distance;
            
            if (!mark)
            {
                break;
            }
            else
            {
                index = i;
            }
        }

        return index;
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
            Mathf.Abs(Mathf.Pow(_xRadius * _yRadius, 2) - Mathf.Pow(_yRadius * (x - _data.Center.x), 2))
            , 0.5f);
        
        // y = (+-sqrtValue + a * k)/a
        float[] temp = new float[2];
        temp[0] = (sqrtValue+ _xRadius * _data.Center.y)/_xRadius;
        temp[1] = (-sqrtValue+ _xRadius * _data.Center.y)/_xRadius;
        return temp;
    }

    public float[] GetX(float y, Vector2 startPos)
    {
        if (_data == null)
            return null;
        
        //a是x轴半径，b是y轴半径，中心的是（h,k）
        //sqrtValue = (a^2*b^2 - b*(y - k))^0.5
        float sqrtValue = Mathf.Pow(
            Mathf.Abs(Mathf.Pow(_xRadius * _yRadius, 2) - Mathf.Pow(_xRadius * (y - _data.Center.y), 2))
            , 0.5f);
        
        // x = (+-sqrtValue + b*h)/b
        float[] temp = new float[2];
        temp[0] = (sqrtValue + _yRadius * _data.Center.x)/_yRadius;
        temp[1] = (-sqrtValue + _yRadius * _data.Center.x)/_yRadius;
          
        return temp;
    }

    public Vector2 GetDirection()
    {
        ChangeIndex();
        return _posList[GetVailIndex(_curIndex + 1)] - _posList[_curIndex];
    }

    private void ChangeIndex()
    {
        if (_curIndex < _middleIndex)
        {
            if (_data.XPos > _posList[GetVailIndex(_curIndex + 1)].x)
            {
                _curIndex++;
            }
        }
        else
        {
            if (_data.XPos < _posList[GetVailIndex(_curIndex + 1)].x)
            {
                _curIndex++;
            }
        }

        _curIndex = GetVailIndex(_curIndex);
    }

    private int GetVailIndex(int index)
    {
        return index % _posList.Length;
    }

    public float GetZRotate()
    {
        return 0;
    }
}
