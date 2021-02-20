using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoadMoveManagement : MonoBehaviour
{
    private float _timer = 0;

    [SerializeField] private Transform _objToMove = null;

    [SerializeField] private bool _isAutoMode = false;

    [SerializeField] private BezierCurve _bezierCurve;

    [SerializeField] private float _speed = 0.001f;

    public float T { get; private set; }

    void Start()
    {
        BezierCurvePointData point = _bezierCurve.DefinePointData(T);
        _objToMove = MoveObjectToPos(_objToMove, point.Position);
    }
    
    private void Update()
    {
        if (_objToMove != null)
        {
            if (T != 1)
            {
                if (_isAutoMode)
                {
                    if (_timer >= 0.001)
                    {
                        BezierCurvePointData point = _bezierCurve.DefinePointData(T);      
                        _objToMove = MoveObjectToPos(_objToMove, point.Position);
                        _timer = 0;
                    }
                    T += _speed;
                    _timer += Time.deltaTime;
                }
                else if (Input.GetKey(KeyCode.W))
                {
                    BezierCurvePointData point = _bezierCurve.DefinePointData(T);
                    _objToMove = MoveObjectToPos(_objToMove, point.Position);
                    T += _speed;
                }
            }
            if (T == 1) T = 0;


        }
    }

    public Transform MoveObjectToPos(Transform obj, Vector3 position)
    {
        obj.position = position;
        return obj;
    }
}
