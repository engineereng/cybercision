using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class moveBug : MonoBehaviour
{
    public AudioSource bugSquashSound;

    public float speed = 5f;
    public double constraintLeftDown;
    public double constraintRightUp;
    private moveBug bugScript;
    public PlayerController scoreObject; 

    public int respawnTime;
    public bool isShootable;

    public bool isSideways;

    bool switc = true;

    private SpriteRenderer spriteRenderer;

    public int bugShotCount;

    void Awake() {
        bugScript = this;
        bugShotCount = 0;
    }

    // Start is called before the first frame update
    void Start()
    {
        isShootable = true;
        spriteRenderer = GetComponent<SpriteRenderer>();
        //bugSquashSound = GetComponent<AudioSource>();
    }

    private void OnMouseDown() {
        if (isShootable) {
            bugSquashSound.Play();
            scoreObject.AddCount();
            HideAndShow(respawnTime);
        }
    }

    private void HideAndShow(float delay)
    {
        
        gameObject.SetActive(false);

        // Call Show after delay seconds
        Invoke(nameof(Show), delay);
    }

    private void Show()
    {
        gameObject.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        // is sideways
        if (isSideways) {
            if (switc) {
                moveup();
            }
            if (!switc) {
                movedown();
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
