using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Make class
//create an object instance at random location within certain range of position (which would be the hand) every time interval
//Start timer when object instance is created; if not dealt with within time something bad happens
//use mousedown/mouseup to keep track of the number of clicks, delete object instance when clicks equal some preset number

public class Fire : MonoBehaviour
{
    private int Health = 5;
    private float timer = 5f;
    private ChargeTimer charge;
    // Start is called before the first frame update
    public void setHealth(int h)
    {
        Health = h;
    }
    public void setTime(float t)
    {
        timer = t;
    }
    public void setCharge(ChargeTimer c)
    {
        charge = c;
    }
    void Start()
    {
        
    }
    void OnMouseDown()
    {
            Health = Health - 1;
            if (Health == 0)
            {
                Destroy(gameObject);
            }
    }
    // Update is called once per frame
    void Update()
    {
        if (timer <= 0)
        {
            charge.fireDamage();
            Debug.Log("Fire damage!!!");
            Destroy(gameObject);
        }
        timer -= Time.deltaTime;
    }
}