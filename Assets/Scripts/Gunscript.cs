using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class Gunscript : MonoBehaviour
{
    [SerializeField] Animator GunAnim;

    [SerializeField] Transform BulletSpawnPos;
    GameObject Bullet;
    //
    [SerializeField] Pose StartPos;
    XRGrabInteractable XrInteractable;

    private void Awake()
    {
        // assigning component
        XrInteractable = GetComponent<XRGrabInteractable>();
        StartPos.position = transform.position;
        StartPos.rotation = transform.rotation;
    }

    private void Start()
    {
        // geting bullet prefab reference
        Bullet = Gamemanager.instance.GetBulletObj();
    }

    private void OnEnable()
    {
        XrInteractable.selectExited.AddListener(gunreleased);

    }

    private void OnDisable()
    {
        XrInteractable.selectExited.RemoveListener(gunreleased);
    }

    //gun will be back to the origin position after some time t // t is delaytime
    private void gunreleased(SelectExitEventArgs arg0)
    {
        origin();
    }

    // set pos to the origin pos
    void origin()
    {
        transform.position = StartPos.position;
        transform.rotation = StartPos.rotation;
    }

    // fire bullet fnction
    public void firebullet()
    {
        // fire gun anim
        GunAnim.SetTrigger("Fire");

        //spawning bullet every time when fire button trigger
        Instantiate(Bullet, BulletSpawnPos.position, transform.rotation);
        audiomanager.instance.gunshootsound();

    }


}
