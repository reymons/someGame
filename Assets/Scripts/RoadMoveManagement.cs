using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoadMoveManagement : MonoBehaviour
{
    private float _timer = 0;

    [SerializeField] private Transform _objToMove = null;

    [SerializeField] private bool _isAutoMode;

    [SerializeField] private BezierCurve _bezierCurve;

    [SerializeField] public float Speed;

    public float T { get; private set; }

    [SerializeField] private MenuManager _menuManager;

    void Start()
    {
        BezierCurvePointData point = _bezierCurve.DefinePointData(T);
        _objToMove = MoveObjectToPos(_objToMove, point.Position);
        Speed = _menuManager.Menu.Settings.MovementSpeed / 100000 * 3;
        _isAutoMode = _menuManager.Menu.Settings.IsAutoMode;
    }
    
    private void Update()
    {
        if (!_menuManager.IsMenuOpen)
        {
            if (_objToMove != null)
            {
                if (T < 1)
                {
                    if (_isAutoMode)
                    {
                        if (_timer >= 0.001)
                        {
                            BezierCurvePointData point = _bezierCurve.DefinePointData(T);
                            _objToMove = MoveObjectToPos(_objToMove, point.Position);
                            _timer = 0;
                        }
                        T += Speed;
                        _timer += Time.deltaTime;
                    }
                    else if (Input.GetKey(KeyCode.W))
                    {
                        BezierCurvePointData point = _bezierCurve.DefinePointData(T);
                        _objToMove = MoveObjectToPos(_objToMove, point.Position);
                        T += Speed;
                    }
                }
                else
                {
                    _menuManager.ShowEndgameWindow();
                }
            }
        }
        else
        {
            Speed = _menuManager.Menu.Settings.MovementSpeed / 100000 * 3;
            _isAutoMode = _menuManager.Menu.Settings.IsAutoMode;
        }
    }

    public Transform MoveObjectToPos(Transform obj, Vector3 position)
    {
        obj.position = position;
        return obj;
    }
}
