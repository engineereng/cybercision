using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class moveBug : MonoBehaviour
{
    public float speed = 5f;
    public double constraintLeft;
    public double constraintRight;

    public double constraintUp;
    public double constraintDown;

    public int seconds;
    public bool isShootable;

    public bool isVertical;

    bool switc = true;

    private SpriteRenderer spriteRenderer;

    // Start is called before the first frame update
    void Start()
    {
        isShootable = true;
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void OnMouseDown() {
        gameObject.transform.position = new Vector3(transform.position.x, transform.position.y, 10);

        StartCoroutine(timearg(seconds));
    }

    IEnumerator timearg(int secs)
    {
        yield return new WaitForSeconds(secs);
        gameObject.transform.position = new Vector3(transform.position.x, transform.position.y, -1);

    }

    // Update is called once per frame
    void Update()
    {
        if (isShootable) {
            if (!isVertical) {
                if (switc) {
                    moveright();
                }
                if (!switc) {
                    moveleft();
                }
                if (transform.position.x >= constraintRight) {
                    switc = false;
                }
                if (transform.position.x <= constraintLeft) {
                    switc = true;
                }
            } else {
                if (switc) {
                    movedown();
                }
                if (!switc) {
                    moveup();
                }
                if (transform.position.y >= constraintUp) {
                    switc = true;
                }
                if (transform.position.y <= constraintDown) {
                    switc = false;
                }
            }
        }
    }


    void moveright() {
        transform.Translate(speed*Time.deltaTime,0,0);
    }

    void moveleft() {
        transform.Translate(-speed*Time.deltaTime,0,0);
    }

    void moveup() {
        transform.Translate(0,speed*Time.deltaTime,0);
    }

    void movedown() {
        transform.Translate(0,-speed*Time.deltaTime,0);
    }
}
