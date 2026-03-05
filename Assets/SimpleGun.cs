using UnityEngine;
using UnityEngine.InputSystem;

public class SimpleGun : MonoBehaviour
{
    public float range = 50f;
    public LayerMask targetLayer;

    void Update()
    {
        if (Mouse.current != null && Mouse.current.leftButton.wasPressedThisFrame)
        {
            Debug.Log("Mouse click detected");
            Shoot();
        }
    }

    void Shoot()
    {
        Debug.Log("Shoot called");
        Ray ray = new Ray(transform.position, transform.forward);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, range))
        {
            Debug.Log("Hit object: " + hit.collider.name);
        }
        else
        {
            Debug.Log("Raycast missed");
        }
    }
}