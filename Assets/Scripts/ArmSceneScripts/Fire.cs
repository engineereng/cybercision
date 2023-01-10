using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Make class
//create an object instance at random location within certain range of position (which would be the hand) every time interval
//Start timer when object instance is created; if not dealt with within time something bad happens
//use mousedown/mouseup to keep track of the number of clicks, delete object instance when clicks equal some preset number

public class Fire : MonoBehaviour
{
    private Vector3 initialposition = new Vector3(0f,10f,0f);
    private Vector3 fireposition;
    private int Health = 5;
    private float cooldown;
    // Start is called before the first frame update
    void Start()
    {
        transform.position = initialposition;
        fireposition = new Vector3(Random.Range(-3.5f, -0.5f), Random.Range(-1.5f, 0.8f),1f);
        cooldown = Random.Range(5f, 10f);
    }
    void OnMouseDown()
    {
            Health = Health - 1;
            if (Health == 0)
            {
                transform.position = initialposition;
                Health = 5;
                cooldown = Time.time + Random.Range(3f, 5f);
                fireposition = new Vector3(Random.Range(-3.5f, -0.5f), Random.Range(-1.5f, 0.8f),1f);
            }
    }
    // Update is called once per frame
    void Update()
    {
        if(Time.time >= cooldown) {
            transform.position = fireposition;
        }
    }
}
