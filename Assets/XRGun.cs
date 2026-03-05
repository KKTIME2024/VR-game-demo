using UnityEngine;
using UnityEngine.InputSystem;

public class XRGun : MonoBehaviour
{
    public InputActionProperty triggerAction;
    public Transform muzzle;
    public float range = 50f;

    LineRenderer lr;

    void Awake()
    {
        lr = GetComponent<LineRenderer>();
    }

    void Update()
    {
        // 始终画射线（调试阶段推荐）
        DrawRay();

        if (triggerAction.action.WasPressedThisFrame())
        {
            Shoot();
        }
    }

    void DrawRay()
    {
        lr.SetPosition(0, muzzle.position);
        lr.SetPosition(1, muzzle.position + muzzle.forward * range);
    }

    void Shoot()
    {
        Ray ray = new Ray(muzzle.position, muzzle.forward);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, range))
        {
            Debug.Log("Hit: " + hit.collider.name);
        }
    }
}