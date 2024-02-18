using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class TusMoveTowards : MonoBehaviour
{
    public GameObject target;
    public float speed;
    public BasicPlayerMovement specialBonus;
    public PlayerTimePlaying gameTime;

    private Vector3 newPosition;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (gameObject != null)
        {
            // Position of the target: target.transform.position
            // Position of the NPC: transform.position
            float speedDelta = speed * Time.deltaTime;
            if (target != null)
            {
                if (gameTime.elapsedTime < 60f)
                {
                    speed += 0.01f * Time.deltaTime;
                }
                else // if the elapsed time of the game is smaller than a 3 minutes and bigger than 1 minute, multiply score by time by .03
                if (gameTime.elapsedTime > 60f && gameTime.elapsedTime < 180f)
                {
                    speed += 0.02f * Time.deltaTime;
                }
                else
                {
                    speed += 0.03f * Time.deltaTime;
                }
                newPosition = tusMoveTowards(target.transform.position, speedDelta);
                transform.position = newPosition;
            }
        }

    }

    Vector3 tusMoveTowards(Vector3 pos, float sd)
    {
        Vector3 targetPos = pos;
        Vector3 enemyPos = transform.position; //3,0,0 // -3.24, -1.56, 0
        
        Vector3 rangeToClose = targetPos - enemyPos;
        Debug.DrawRay(enemyPos, rangeToClose, Color.cyan);
        //Debug.DrawRay(enemyPos, rangeToClose.normalized, Color.red);
   

        //Debug.Log("Vector rangeToClose magnitude is: " + rangeToClose.magnitude + " on object: " + gameObject.name);


        //Calculate how far enemy moves this frame

        Vector3 delta = rangeToClose.normalized * sd;
        if (specialBonus.isBonusActive == true)
        {
            delta = -delta;
        }
        return transform.position + delta; // return the position of the enemy + the vector rangeToClose normalized scaling it by the speed set
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Colliding in TusMoveTowards, with: " + collision.name);
        // check if enemy collided with player, if so, destroy the object.
        if (collision.CompareTag("Player") && specialBonus.isBonusActive == false)
        {
            Destroy(collision.gameObject);
            Debug.Log("enter on isbonus false");

            // Maybe load an end game scene?
            
        } else if (collision.CompareTag("Player") && specialBonus.isBonusActive == true)
        {
            Destroy(gameObject);
            Debug.Log("enter on isbonus true");
        }
    }
}
