using UnityEngine;

public class SphereAtMouseRayHitDrawer : MonoBehaviour
{
    [SerializeField] private Camera _mainCamera;
    [SerializeField] private Color _color = Color.white;
    [SerializeField] private float _radius = 0.3f;

    private void OnDrawGizmos()
    {
        if (!_mainCamera)
        {
            return;
        }

        Vector3 mouseScreenPosition = Input.mousePosition;
        Ray findTargetRay = _mainCamera.ScreenPointToRay(mouseScreenPosition);

        if (Physics.Raycast(findTargetRay, out RaycastHit hitInfo))
        {
            Gizmos.color = _color;
            Gizmos.DrawSphere(hitInfo.point, _radius);
        }
    }
}
