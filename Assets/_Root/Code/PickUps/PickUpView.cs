
using DG.Tweening;
using UnityEngine;

public sealed class PickUpView : MonoBehaviour
{
    private Collider _collider;
    private Rigidbody _rigidBody;
    [field: SerializeField] public Outline Outliner { get; private set; }

    private void Awake()
    {
        _collider = GetComponent<Collider>();
        _rigidBody = GetComponent<Rigidbody>();
        Outliner = GetComponent<Outline>();
        Outliner.enabled = false;
    }

    public void Select() => Outliner.enabled = true;

    public void Deselect() => Outliner.enabled = false;

    public void MoveToAnchorPoint(Transform anchor)
    {
        transform.SetParent(null);
        _rigidBody.isKinematic = true;
        _rigidBody.detectCollisions = false;
        _collider.enabled = false;
        Outliner.enabled = false;
        transform.DOMove(anchor.position, 0.1f).SetEase(Ease.OutSine).SetAutoKill().SetRecyclable().OnComplete(() => transform.parent = anchor);
    }

    public void Drop(Vector3 dropVector)
    {
        transform.parent = null;
        _rigidBody.isKinematic = false;
        _rigidBody.detectCollisions = true;
        _collider.enabled = true;
        _rigidBody.AddForce(dropVector, ForceMode.Impulse);
    }
}
