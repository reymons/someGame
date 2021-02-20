using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrosshairTargetManager : MonoBehaviour
{
    [Range(1, 100, order = 1)]
    [SerializeField] private int _raycastLength = 10;

    [SerializeField] private CrosshairManager _crosshairManager;

    [SerializeField] private ScoreManager _scoreManager;

    [SerializeField] private TargetSpawner _targetSpawner;

    void Update()
    {
        CheckIfPlayerHit();
    }

    private void CheckIfPlayerHit()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit, _raycastLength))
        {
            GameObject hitObj = hit.collider.gameObject;
            if (hitObj.CompareTag("Target"))
            {
                _crosshairManager.TargetEnter();
                if (Input.GetMouseButtonDown(0))
                {
                    _scoreManager.Increase();
                    _scoreManager.UpdateText();

                    _targetSpawner.DestroyTarget();

                    _crosshairManager.TargetExit();
                }
            }
            else _crosshairManager.TargetExit();
        }
    }
}
