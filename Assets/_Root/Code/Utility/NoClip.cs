using Unity.VisualScripting;
using UnityEngine;

public sealed class NoClip : MonoBehaviour
{
    [SerializeField] private float _distance;
    [SerializeField] private float _radius;
    [SerializeField] private LayerMask _clippingLayerMask;
    [SerializeField] private AnimationCurve _offsetCurve;
    private Vector3 _originalLocalPosition;
    private void Awake() => _originalLocalPosition = transform.localPosition;

    private void FixedUpdate()
    {
        if (Physics.SphereCast(Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0)), _radius, out var hit, _distance, _clippingLayerMask))
        {
            transform.localPosition = _originalLocalPosition - new Vector3(0, 0, _offsetCurve.Evaluate(hit.distance / _distance));
        }
        else
        {
            transform.localPosition = _originalLocalPosition;
        }
    }

}
