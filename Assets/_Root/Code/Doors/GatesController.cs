using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public sealed class GatesController : MonoBehaviour
{
    private BoxCollider _collider;

    [field: SerializeField] public List<DoorOpen> Doors;

    private void Awake()
    {
        _collider = GetComponent<BoxCollider>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out FirstPersonMovement player))
        {
            foreach (DoorOpen door in Doors)
            {
                door.Open();
            }
            _collider.enabled = false;
        }
    }
}
