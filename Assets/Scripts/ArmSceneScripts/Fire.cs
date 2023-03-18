using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Make class
//create an object instance at random location within certain range of position (which would be the hand) every time interval
//Start timer when object instance is created; if not dealt with within time something bad happens
//use mousedown/mouseup to keep track of the number of clicks, delete object instance when clicks equal some preset number

public class Fire : MonoBehaviour
{
    [SerializeField] private int health = 5; // number of clicks
    [SerializeField] private float timer = 5f; // time in seconds until the fire expires and damages the timer
    [SerializeField] private ChargeTimer charge;
    // Start is called before the first frame update
    public void setCharge(ChargeTimer c)
    {
        charge = c;
    }
    void Start()
    {
        
    }
    void OnMouseDown()
    {
        health = health - 1;
        if (health == 0)
        {
            Destroy(gameObject);
        }
    }
    // Update is called once per frame
    void Update()
    {
        if (timer <= 0)
        {
            charge?.fireDamage();
            Debug.Log("Fire damage!!!");
            Destroy(gameObject);
        }
        timer -= Time.deltaTime;
    }
}
