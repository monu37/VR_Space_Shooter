using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class hapticgun : MonoBehaviour
{
    [SerializeField] XRGrabInteractable GrabInteractable;

    private void OnEnable()
    {
        GrabInteractable.activated.AddListener(sendhaptic);
    }

    private void OnDisable()
    {
        GrabInteractable.activated.RemoveListener(sendhaptic);
    }

    private void sendhaptic(ActivateEventArgs arg0)
    {
        arg0.interactableObject.transform.GetComponent<XRController>().SendHapticImpulse(1, .4f);
    }

}
