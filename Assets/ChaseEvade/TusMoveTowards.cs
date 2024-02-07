using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TusMoveTowards : MonoBehaviour
{
    public GameObject target;
    public float speed;


    // Start is called before the first frame update
    void Start()
    {


    }

    // Update is called once per frame
    void Update()
    {
        // Position of the target: target.transform.position
        // Position of ther NPC: transform.position
        float speedDelta = speed * Time.deltaTime;

        Vector3 newPosition = tusMoveTowards(target.transform.position, speedDelta);

        transform.position = newPosition;
    }

    Vector3 tusMoveTowards(Vector3 pos, float sd)
    {
        Vector3 targetPos = pos;
        Vector3 enemyPos = transform.position; //3,0,0 // -3.24, -1.56, 0
        
        Vector3 rangeToClose = targetPos - enemyPos;
        Debug.DrawRay(enemyPos, rangeToClose, Color.cyan);
        Debug.DrawRay(enemyPos, rangeToClose.normalized, Color.red);


        //Debug.Log("Vector rangeToClose magnitude is: " + rangeToClose.magnitude + " on object: " + gameObject.name);


        //Calculate how far enemy moves this frame

        Vector3 delta = rangeToClose.normalized * sd;
        return transform.position + delta; // return the position of the enemy + the vector rangeToClose normalized scaling it by the speed set
    }
}
