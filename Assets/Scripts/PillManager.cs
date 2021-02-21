using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PillManager : MonoBehaviour
{
    public bool PillEntered;

    private void OnTriggerEnter(Collider other)
    {
        PillEntered = true;
    }
}
