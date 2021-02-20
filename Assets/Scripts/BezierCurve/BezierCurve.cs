using System.Collections.Generic;
using UnityEngine;

public class BezierCurve : MonoBehaviour
{
    [SerializeField] private Transform[] _controlPoints;

    [SerializeField] private bool _showControlPoints;

    [SerializeField] private bool _showCurve;

    [SerializeField] private int _segments = 3;

    [SerializeField] private float _sphereRadius = 0.02f;

    public int SegmentsCount => _segments;

    public Transform[] GetControlPoints => _controlPoints;

    private Vector3 GetPosition(int index) => _controlPoints[index].position;

    public BezierCurvePointData DefinePointData(float t)
    {
        Vector3[][] lerps = new Vector3[_controlPoints.Length - 1][];

        for (int i = 1; i <= lerps.Length; i++)
        {
            lerps[i - 1] = new Vector3[_controlPoints.Length - i];
        }

        for (int i = 0; i < _controlPoints.Length - 1; i++)
        {
            lerps[0][i] = Vector3.Lerp(GetPosition(i), GetPosition(i + 1), t);
        }
        for (int i = 1; i < _controlPoints.Length - 1; i++)
        {
            for (int j = 0; j < lerps[i - 1].Length - 1; j++)
            {
                lerps[i][j] = Vector3.Lerp(lerps[i - 1][j], lerps[i - 1][j + 1], t);
            }
        }

        Vector3 tangent = (lerps[_controlPoints.Length - 3][1] - lerps[_controlPoints.Length - 3][0]).normalized;

        return new BezierCurvePointData(lerps[_controlPoints.Length - 2][0], Quaternion.LookRotation(tangent));
    }

    private void OnDrawGizmos()
    {
        if (_showControlPoints)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawSphere(GetPosition(0), Mathf.Abs(_sphereRadius));
            for (int i = 1; i < _controlPoints.Length; i++)
            {
                Gizmos.DrawSphere(GetPosition(i), Mathf.Abs(_sphereRadius));
                Gizmos.DrawLine(GetPosition(i - 1), GetPosition(i));
            }
        }
        if (_showCurve)
        {
            Gizmos.color = Color.red;
            var datas = new List<BezierCurvePointData>();
            var increment = 1 / (float)Mathf.Abs(_segments);
            for (float t = 0; t < 1; t += increment)
            {
                BezierCurvePointData data = DefinePointData(t);
                datas.Add(data);
            }
            for (int i = 0; i < datas.Count - 1; i++)
            {
                Gizmos.DrawLine(datas[i].Position, datas[i + 1].Position);
            }
        }
    }
}


