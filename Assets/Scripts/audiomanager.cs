using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class audiomanager : MonoBehaviour
{
    public static audiomanager instance;

    [SerializeField] AudioSource BgSource;
    [SerializeField] AudioSource GunShootSource;
    [SerializeField] AudioSource AsterioidHitSource;
    [SerializeField] AudioSource GameUiSource;

    [Header("Clips")]
    [SerializeField] AudioClip IntroClip;
    [SerializeField] AudioClip GameClip;
    [SerializeField] AudioClip GunShootClip;
    [SerializeField] AudioClip AsterioidHitClip;
    [SerializeField] AudioClip GameOverClip;
    [SerializeField] AudioClip ClickClip;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        intromusic();
    }

    // intro music
    public void intromusic()
    {
        BgSource.clip = IntroClip;
        BgSource.Play();
        BgSource.loop = true;
    }

    // game main music
    public void gamemusic()
    {
        BgSource.clip = GameClip;
        BgSource.Play();
        BgSource.loop = true;
    }

    // UI shoot sound
    public void clicksound()
    {
        GameUiSource.PlayOneShot(ClickClip);
    }
    
    // bullet firing sound
    public void gunshootsound()
    {
        GunShootSource.PlayOneShot(GunShootClip);
    }

    //asteriods destroying sound
    public void asterioiddestroysound()
    {
        AsterioidHitSource.PlayOneShot(AsterioidHitClip);
    }

    // game over sound
    public void gameoversound()
    {
        GameUiSource.PlayOneShot(GameOverClip);
    }

}
