using DG.Tweening;
using UnityEngine;

public class DoorOpen : MonoBehaviour
{
    [field: SerializeField] public Vector3 OpenRotation { get; private set; }

    public void Open() => transform.DORotate(OpenRotation, 1f, RotateMode.LocalAxisAdd).SetAutoKill();
}
