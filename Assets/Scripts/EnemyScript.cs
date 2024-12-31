using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    [SerializeField]
    Vector3 MovePos;

    Animator Anim;

    Rigidbody mybody;
    [SerializeField] float MinSpeed, MaxSpeed;
    float Speed;
    Vector3 rot;
    [SerializeField] float MinRotSpeed, MaxRotSpeed;
    float RotSpeed;

    [SerializeField] GameObject HitEffect;

    GameObject ScorePrefab;

    private void Awake()
    {
        Anim = GetComponent<Animator>();
    }

    private void Start()
    {
        // assigning speed to asteriod or enemy
        Speed = Random.Range(MinSpeed, MaxSpeed);


        //rotating the asteriods
        float Xrot = Random.Range(0, 360);
        float Yrot = Random.Range(0, 360);
        float Zrot = Random.Range(0, 360);
        rot = new Vector3(Xrot, Yrot, Zrot); 


        //assigning rotating speed
        RotSpeed = Random.Range(MinRotSpeed, MaxRotSpeed);

        // assigning scorepopup after destroyed this will popup
        ScorePrefab = Gamemanager.instance.GetScorePopup();


    }
    private void Update()
    {
        if (Gamemanager.instance.GameStatus == Gamemanager.status.Playing)
        {
            // move and rotate
            transform.Translate(MovePos * Time.deltaTime * Speed, Space.World);
            transform.Rotate(Vector3.up * RotSpeed * Time.deltaTime);
        }
        
    }

    // if bullet hit his object then this unction will called
    public void Hit()
    {
        // if game status is playing then only this function  call
        if (Gamemanager.instance.GetGameStatus() == Gamemanager.status.Playing)
        {
            Gamemanager.instance.updatetotalkill(1);

            // spawning destroy effect
            Instantiate(HitEffect, transform.position, transform.rotation);

            //score
            float distance = Vector3.Distance(transform.position, Vector3.zero);
            int score = ((int)distance) * 10;

            ScorePrefab.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "+" + score.ToString();

           // spawning scorepopup
            Instantiate(ScorePrefab, transform.position, Quaternion.identity);
           

            //updating game score
            Gamemanager.instance.updatescore(score);


        }
        Destroy(gameObject);
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "boundary") // if obj collide with boundary
        {
            Anim.SetBool("FadeOut", true);

            //gameObject.SetActive(false);// disabled the obj

            Invoke(nameof(disabledobj), 1f);
        }


    }


    void disabledobj()
    {
        gameObject.SetActive(false);// disabled the obj
    }
}
