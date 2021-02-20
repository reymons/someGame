using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetSpawner : MonoBehaviour
{
    [SerializeField] private Transform _target;

    [SerializeField] private RoadMoveManagement _roadMM;

    [SerializeField] private BezierCurve _bezierCurve;

    [Range(0.1f, 3)]
    [SerializeField] private float _expireTime = 1;

    private float _timer;

    private float prevTargetT;

    private System.Random _rnd = new System.Random();

    [SerializeField] private int _procChange = 10;

    private int[] _procNumbers;

    private bool _isAppearing;

    void Start()
    {
        _target.gameObject.SetActive(false);
        _procNumbers = new int[_procChange];
        for (int i = 0; i < _procChange; i++)
            _procNumbers[i] = i;
    }

    void FixedUpdate()
    {
        if (_timer - _expireTime > 10e-11)
        {
            _target.gameObject.SetActive(false);
            _timer = 0;

            int rndNum = _rnd.Next(0, 100);
            if (System.Array.IndexOf(_procNumbers, rndNum) > -1)
                _isAppearing = true;
        }
        else
        {
            if (!_target.gameObject.activeSelf)
            {
                float currentTargetT = _roadMM.T + 0.1f;
                if (currentTargetT != prevTargetT)
                {
                    if (_isAppearing)
                    {
                        BezierCurvePointData point = _bezierCurve.DefinePointData(currentTargetT);

                        int xOffset = _rnd.Next(-6, 6);
                        int yOffset = _rnd.Next(1, 4);
                        int zOffset = _rnd.Next(0, 4);

                        Vector3 newPoint = new Vector3(point.Position.x + xOffset, point.Position.y + yOffset, point.Position.z + zOffset);

                        _target = _roadMM.MoveObjectToPos(_target, newPoint);
                        _target.rotation = point.Rotation;
                        _target.gameObject.SetActive(true);
                        
                        prevTargetT = currentTargetT;
                        _isAppearing = false;
                    }
                }  
            }

            _timer += Time.deltaTime;
        }
    }
}
