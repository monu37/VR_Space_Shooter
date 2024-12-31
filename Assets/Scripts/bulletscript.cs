using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bulletscript : MonoBehaviour
{
    [SerializeField] float Speed;

    private void Update()
    {
        // move forward with speed
        transform.Translate(Vector3.forward * Speed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        GameObject hitobj = Gamemanager.instance.GetHitUIParticleObj();
        if (other.tag == "enemy") // if bullet collide with asteriod(enemy) 
        {
            if (Gamemanager.instance.GameStatus == Gamemanager.status.Traning)
            {
                Gamemanager.instance.checktraningstatus();
            }

            audiomanager.instance.asterioiddestroysound(); 

            other.GetComponent<EnemyScript>().Hit(); // call hit function from enemy(asteriods) script

            Destroy(gameObject);
        }

        if (other.tag == "startgame") // bullet collide with startgame tag i.e. before starting the game
        {
            Instantiate(hitobj, transform.position, Quaternion.identity);
            audiomanager.instance.clicksound();

            Gamemanager.instance.startgame();  //start game function will called
           
            Destroy(gameObject);
        }

        if (other.tag == "NextLevel") // bullet collide with startgame tag i.e. after game win to play the next level
        {
            Instantiate(hitobj, transform.position, Quaternion.identity);
            audiomanager.instance.clicksound();

            Gamemanager.instance.nextlevel();  
           
            Destroy(gameObject);
        }

        if (other.tag == "restartgame") // bullet collide with restartgame tag i.e. after game over to restart the game
        {
            Instantiate(hitobj, transform.position, Quaternion.identity);
            audiomanager.instance.clicksound();

            Gamemanager.instance.restartgame(); // restart function called
            
            Destroy(gameObject);
        }

        if (other.gameObject.tag == "boundary")  // if bullet collide with any boundary then it will destroy/disabled bullet
        {
            gameObject.SetActive(false);

            //Destroy(gameObject);
        }

    }
}
