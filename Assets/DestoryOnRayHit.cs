using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

public class DestroyOnRayHit : XRSimpleInteractable
{
    protected override void OnSelectEntered(SelectEnterEventArgs args)
    {
        base.OnSelectEntered(args);
        Destroy(gameObject);
        Debug.Log("Cube was hit by ray and destroyed.");
    }
}
