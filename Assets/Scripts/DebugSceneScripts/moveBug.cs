using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class moveBug : MonoBehaviour
{
    public float speed = 5f;
    public double constraintLeftDown;
    public double constraintRightUp;
    public moveBug bugScript;

    public int seconds;
    public bool isShootable;

    public bool isVertical;

    bool switc = true;

    private SpriteRenderer spriteRenderer;

    void Awake() {
        bugScript = this;
    }
    
    // Start is called before the first frame update
    void Start()
    {
        isShootable = true;
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void OnMouseDown() {
        if (isShootable) {
            gameObject.transform.position = new Vector3(transform.position.x, transform.position.y, 10);

            StartCoroutine(timearg(seconds));
        } else {
            Debug.Log("NOT SHOOTABLE");
        }
        
    }

    IEnumerator timearg(int secs)
    {
        yield return new WaitForSeconds(secs);
        gameObject.transform.position = new Vector3(transform.position.x, transform.position.y, -1);

    }

    // Update is called once per frame
    void Update()
    {
        if (!isVertical) {
            if (switc) {
                moveright();
            }
            if (!switc) {
                moveleft();
            }
            if (transform.position.x >= constraintRightUp) {
                switc = false;
            }
            if (transform.position.x <= constraintLeftDown) {
                switc = true;
            }
        } else {
            if (switc) {
                movedown();
            }
            if (!switc) {
                moveup();
            }
            if (transform.position.y >= constraintRightUp) {
                switc = true;
            }
            if (transform.position.y <= constraintLeftDown) {
                switc = false;
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
