using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PillSpawner : MonoBehaviour
{
    [SerializeField] private Transform _pillObj;

    [SerializeField] private BezierCurve _bezierCurve;

    [SerializeField] private RoadMoveManagement _roadMM;

    [SerializeField] private MenuManager _menuManager;

    private System.Random _rnd = new System.Random();

    private float _expireTime = 3;

    private float _timeBetweenSpawn = 3;

    private float prevSpeed;

    private PillManager _pillManager;

    private float _effectDuration = 1.5f;

    private void Start()
    {
        _pillObj.gameObject.SetActive(false);
        _pillManager = _pillObj.gameObject.GetComponent<PillManager>();
        prevSpeed = _menuManager.Menu.Settings.MovementSpeed;
    }

    private void FixedUpdate()
    {
        if (prevSpeed != _menuManager.Menu.Settings.MovementSpeed)
        {
            prevSpeed = _menuManager.Menu.Settings.MovementSpeed;
        }

        if (!_menuManager.IsMenuOpen)
        {
            if (_timeBetweenSpawn <= 0 && !_pillObj.gameObject.activeSelf)
            {

                float newT = _roadMM.T + (float)_rnd.Next(1, 6) / 100;
                BezierCurvePointData point = _bezierCurve.DefinePointData(newT);
                _pillObj = _roadMM.MoveObjectToPos(_pillObj, point.Position + Vector3.up);
                EnablePill();
                _expireTime = 3;
            }
            else if (!_pillObj.gameObject.activeSelf)
            {
                _timeBetweenSpawn -= Time.deltaTime;
            }

            if (_pillObj.gameObject.activeSelf)
            {
                if (_expireTime <= 0)
                {
                    DisablePill();
                    _timeBetweenSpawn = 3;
                }
                else
                {
                    _expireTime -= Time.deltaTime;
                    if (_pillManager.PillEntered)
                    {
                        DisablePill();
                        _roadMM.Speed = (float)200 / 100000;
                        _timeBetweenSpawn = 3;
                    }
                }
            }

            if (_pillManager.PillEntered)
            {
                if (_effectDuration <= 0)
                {
                    _roadMM.Speed = _menuManager.Menu.Settings.MovementSpeed / 100000 * 3;
                    _effectDuration = 1.5f;
                    _pillManager.PillEntered = false;
                }
                else
                {
                    _effectDuration -= Time.deltaTime;
                }
            }
        }
    }

    private void DisablePill()
    {
        _pillObj.gameObject.SetActive(false);
    }

    private void EnablePill()
    {
        _pillObj.gameObject.SetActive(true);
    }
}
