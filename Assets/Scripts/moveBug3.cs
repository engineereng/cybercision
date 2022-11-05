using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class moveBug3 : MonoBehaviour
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
            moveright();
        }
        if (!switc) {
            moveleft();
        }
        if (transform.position.x >= -6f) {
            switc = false;
        }
        if (transform.position.x <= 6f) {
            switc = true;
        }
    }

    void moveright() {
        transform.Translate(speed*Time.deltaTime,0,0);
    }

    void moveleft() {
        transform.Translate(-speed*Time.deltaTime,0,0);
    }
}
