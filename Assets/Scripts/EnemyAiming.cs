using UnityEngine;
using UnityEngine.Animations.Rigging;

public class EnemyAiming : CharacterAiming
{
    [SerializeField] private float _aimingSpeed = 10f;
    [SerializeField] private Vector3 _aimDeltaPosition = Vector3.up;

    private Transform _aimTransform;
    private RigBuilder _rigBuilder;
    private WeaponAiming[] _weaponAimings;

    protected override void OnInit()
    {
        _aimTransform = CreateAim().transform;
        _rigBuilder = GetComponentInChildren<RigBuilder>();
        _weaponAimings = GetComponentsInChildren<WeaponAiming>(true);

        InitWeaponAimings(_weaponAimings, _aimTransform);
    }

    private GameObject CreateAim()
    {
        GameObject aim = new GameObject("EnemyAim");
        aim.transform.SetParent(transform);
        aim.transform.position = transform.position + transform.forward + _aimDeltaPosition;
        return aim;
    }

    private void InitWeaponAimings(WeaponAiming[] weaponAimings, Transform aim)
    {
        for (int i = 0; i < weaponAimings.Length; i++)
        {
            weaponAimings[i].Init(aim);
        }
        _rigBuilder.Build();
    }

    private void FixedUpdate()
    {
        if (!IsActive)
        {
            return;
        }
        Aiming();
    }

    private void Aiming()
    {
        //Vector3 mouseScreenPosition = Input.mousePosition;
        //Ray findTargetRay = _mainCamera.ScreenPointToRay(mouseScreenPosition);

        //if (Physics.Raycast(findTargetRay, out RaycastHit hitInfo))
        //{
        //    Vector3 lookDirection = (hitInfo.point - transform.position).normalized;
        //    lookDirection.y = 0;
        //    var newRotation = Quaternion.LookRotation(lookDirection, Vector3.up);
        //    transform.rotation = Quaternion.Slerp(transform.rotation, newRotation, _aimingSpeed * Time.fixedDeltaTime);


        //    _aimTransform.position = Vector3.Lerp(_aimTransform.position, hitInfo.point, _aimingSpeed * Time.fixedDeltaTime);
        //}

        Vector3 lookDirection = (_aimTransform.position - transform.position).normalized;
        lookDirection.y = 0;
        var newRotation = Quaternion.LookRotation(lookDirection, Vector3.up);
        transform.rotation = Quaternion.Slerp(transform.rotation, newRotation, _aimingSpeed * Time.fixedDeltaTime);

    }
}
