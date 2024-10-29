using UnityEngine;

public sealed class SelectionController : MonoBehaviour
{
    private float _raycastDistance = 10f;
    public PickUpView CurrentPickUpView { get; private set; }

    private float _timeDelay = 0.1f;
    private float _elapsed;
    public bool CanSelect = true;

    private void Update()
    {
        _elapsed += Time.deltaTime;
        if (_elapsed >= _timeDelay)
        {
            if (!CanSelect) return;
            Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, _raycastDistance))
            {
                if (hit.transform.gameObject.TryGetComponent(out PickUpView pickUp))
                {
                    if (pickUp)
                    {
                        if (CurrentPickUpView && CurrentPickUpView != pickUp)
                        {
                            CurrentPickUpView.Deselect();
                            CurrentPickUpView = pickUp;
                            CurrentPickUpView.Select();
                        }
                        else
                        {
                            CurrentPickUpView = pickUp;
                            CurrentPickUpView.Select();
                        }
                    }
                    else
                    {
                        if (CurrentPickUpView)
                        {
                            CurrentPickUpView.Deselect();
                            CurrentPickUpView = null;
                        }
                    }
                }
                else
                {
                    if (CurrentPickUpView)
                    {
                        CurrentPickUpView.Deselect();
                        CurrentPickUpView = null;
                    }
                }
            }
            _elapsed = 0;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (CurrentPickUpView)
        {
            CurrentPickUpView.Deselect();
            CurrentPickUpView = null;
        }
    }
}
