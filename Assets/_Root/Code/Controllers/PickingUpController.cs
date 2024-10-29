using UnityEngine;

public sealed class PickingUpController : MonoBehaviour
{
    [field: SerializeField] public float ForceMultiplier { get; private set; }
    [field: SerializeField] public Transform PickUpAnchor { get; private set; }
    private SelectionController _selectionController;
    private PickUpView _currentCariedPickUp;

    private void Awake()
    {
        _selectionController = GetComponent<SelectionController>();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (_selectionController.CurrentPickUpView)
            {
                if (!_currentCariedPickUp)
                {
                    _selectionController.CanSelect = false;
                    _selectionController.CurrentPickUpView.MoveToAnchorPoint(PickUpAnchor);
                    _currentCariedPickUp = _selectionController.CurrentPickUpView;
                }
                else
                {
                    _currentCariedPickUp.Drop(transform.forward * ForceMultiplier);
                    _currentCariedPickUp = null;
                    _selectionController.CanSelect = true;
                }
            }
            if (!_selectionController.CurrentPickUpView)
            {
                if (_currentCariedPickUp)
                {
                    _currentCariedPickUp.Drop(transform.forward * ForceMultiplier);
                    _currentCariedPickUp = null;
                    _selectionController.CanSelect = true;
                }
            }
        }

    }

}