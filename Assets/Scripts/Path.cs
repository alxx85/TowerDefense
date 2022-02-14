using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Path : MonoBehaviour
{
    private List<ChekPoint> _points;

    private void Start()
    {
        _points = new List<ChekPoint>(gameObject.GetComponentsInChildren<ChekPoint>());
    }

    public Vector3 GetNextPoint(int currentPoint)
    {
        if (currentPoint < _points.Count)
            return _points[currentPoint].transform.position;

        return default;
    }

    public Vector3 StartPoint()
    {
        return GetNextPoint(0);
    }
}
