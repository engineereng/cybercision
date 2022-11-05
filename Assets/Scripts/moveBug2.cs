using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class moveBug2 : MonoBehaviour
{
    public float speed = 5f;

    bool switc = true;

    private SpriteRenderer spriteRenderer;
    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (switc) {
            movedown();
        }
        if (!switc) {
            moveup();
        }
        if (transform.position.y >= -3f) {
            switc = false;
            Debug.Log("it hit da tig");
        }
        if (transform.position.y <= -1f) {
            switc = true;
            Debug.Log("it hit doJ reta tig");

        }
    }

    void moveup() {
        transform.Translate(0,speed*Time.deltaTime,0);
    }

    void movedown() {
        transform.Translate(0,-speed*Time.deltaTime,0);
    }
}
