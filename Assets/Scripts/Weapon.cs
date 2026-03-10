using System.Collections;
using System.Collections.Generic;
using Unity.IO.LowLevel.Unsafe;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;
using UnityEngine.XR.Interaction.Toolkit.Interactors;

[RequireComponent(typeof(XRGrabInteractable))]
[RequireComponent(typeof(Rigidbody))]
public class Weapon : MonoBehaviour
{
    [SerializeField] protected float shootingForce;
    [SerializeField] protected Transform bulletSpawn;
    [SerializeField] protected float recoilForce;
    [Serialize] public float damage;


    private Rigidbody newrigidbody;
    private XRGrabInteractable grabInteractableWeapon;    

    protected virtual void Awake()
    {
        newrigidbody = GetComponent<Rigidbody>();
        grabInteractableWeapon = GetComponent<XRGrabInteractable>();
        SetupInteractableWeaponEvent();
    }

    public void SetupInteractableWeaponEvent()
    {
        grabInteractableWeapon.selectEntered.AddListener(PickUpWeapon);
        grabInteractableWeapon.selectExited.AddListener(DropWeapon);
        grabInteractableWeapon.activated.AddListener(StartShooting);
        grabInteractableWeapon.deactivated.AddListener(StopShooting);
        
    }   

    private void PickUpWeapon(SelectEnterEventArgs args)
    {
        (args.interactorObject as MonoBehaviour).GetComponent<MeshHidder>().Hide();
    }
        private void DropWeapon(SelectExitEventArgs args)
    {
        (args.interactorObject as MonoBehaviour).GetComponent<MeshHidder>().Show();
    }
    protected virtual void StartShooting(ActivateEventArgs args)
    {
        // Base implementation intentionally left empty for derived classes.
    }
        protected virtual void StopShooting(DeactivateEventArgs args)
    {
        // Base implementation intentionally left empty for derived classes.
    }

    protected virtual void Shoot()
    {
        ApplyRecoil();
    }

    private void ApplyRecoil()
    {
        newrigidbody.AddRelativeForce(Vector3.back * recoilForce, ForceMode.Impulse);
    }

    public float GetShootingForce()
    {
        return shootingForce;
    }

    public float GetDamage()
    {
        return damage;
    }
}
