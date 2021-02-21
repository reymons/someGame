using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLightManager : MonoBehaviour
{
    private Light _thisLight;

    [SerializeField] private MenuManager _menuManager;

    void Start()
    {
        _thisLight = GetComponent<Light>();
    }

    // Update is called once per frame
    void Update()
    {
        if (_menuManager.IsMenuOpen)
        {
            _thisLight.enabled = false;
        }
        else if (!_thisLight.enabled)
        {
            _thisLight.enabled = true;
        }
    }
}
