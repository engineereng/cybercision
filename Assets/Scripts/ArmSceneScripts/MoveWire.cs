using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveWire : MonoBehaviour
{
    // adapted from: https://forum.unity.com/threads/throwing-an-object-with-mouse.563218/
    [SerializeField] float moveSpeed;
    [SerializeField] float offset;

    Vector2 lastPosition;
    [SerializeField] float saveDelay = 0.2f;
    [SerializeField] float power = 5f;
    bool nextSave = true;

    [SerializeField] private bool following;
    Rigidbody2D rb;
    Vector2 dir;
    bool canBePushed;

    void Start()
    {
        following = false;
        //offset += 10;
        lastPosition = transform.position;
        rb = gameObject.GetComponent<Rigidbody2D>();
        // rb.AddForce(new Vector2(startingVelocity, startingVelocity));
    }

    // Update is called once per frame
    void Update()
    {

        if (false) { // CHANGE THIS to look at t (transform.position.x > 2 || transform.position.x < -2 || transform.position.y > 1 || transform.position.y < -1) {
            StartCoroutine(MoveBackToPosition());
            return;
        }
        if (Input.GetMouseButtonDown(0) && ((Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position).magnitude <= offset))
        {
            following = true;
        }
        if (Input.GetMouseButtonUp(0) && ((Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position).magnitude <= offset))
        {
            following = false;
            dir = (Vector2)transform.position - lastPosition;
            canBePushed = true;
        }
 
        if (following)
        {
            transform.position = Vector2.Lerp(transform.position, Camera.main.ScreenToWorldPoint(Input.mousePosition), moveSpeed);
        }
 
        if (nextSave)
        {
            StartCoroutine("SavePosition");
        }
    }
 
    IEnumerator MoveBackToPosition()
    {
        yield return new WaitForSeconds(0.5f);
        transform.position = Vector2.zero;
        following = false;
    }

    void FixedUpdate()
    {
        if (canBePushed)
        {
            canBePushed = false;
            rb.velocity = dir * power;
        }
    }
 
    IEnumerator SavePosition()
    {
        nextSave = false;
        lastPosition = transform.position;
        yield return new WaitForSeconds(saveDelay);
        nextSave = true;
    }    
}
