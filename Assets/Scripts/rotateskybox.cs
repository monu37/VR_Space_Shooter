using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rotateskybox : MonoBehaviour
{
    [SerializeField] float RotateSpeed;

    void Update()
    {
        RenderSettings.skybox.SetFloat("_Rotation", RotateSpeed * Time.time);
    }
}
