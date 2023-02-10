using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class SliderScript : MonoBehaviour
{
    public Rigidbody2D sliderBody;
    public int SLIDER_SPEED;
    private int direction;
    private float time;
    public bool alive;
    private float INCREMENT_ON_TIME = 5;
    private int INCREMENT_SPEED = 1;

    private bool missClick = false;
    bool canClick;

    public LogicScript logic;

    // Start is called before the first frame update
    void Start()
    {
        logic = GameObject.FindGameObjectWithTag("Logic").GetComponent<LogicScript>();
        direction = 1;
        SLIDER_SPEED = 3;  
        canClick = false;
        alive = true;
    }

    // Update is called once per frame
    void Update()
    {
        print(logic.isWin());
        if(alive && !logic.isWin()){
            time += Time.deltaTime;
            if(time == INCREMENT_ON_TIME){
                SLIDER_SPEED += INCREMENT_SPEED;
                time = 0;
            }
            sliderBody.velocity = Vector2.right * SLIDER_SPEED * direction;
        }
        else sliderBody.velocity *= 0;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        direction *= -1;
    }
    
    private void OnTriggerEnter2D(Collider2D Node)
    {
        if(Node.gameObject.tag == "Reverse"){
            int r = Random.Range(0, 4);
            if(r == 1)direction *= -1;   
        }
        else {
            canClick = true;
            missClick = false;
        }
    }
    private void OnTriggerStay2D(Collider2D Node)
    {
        if(canClick){
            if(Node.gameObject.tag == "Square"){
                if(Input.GetMouseButton(1)){
                    canClick = false;
                }
                else if (Input.GetMouseButton(0)) {
                    canClick = false;
                    missClick = true;
                }
            }
            if(Node.gameObject.tag == "Circle"){
                if(Input.GetMouseButton(0)){
                    canClick = false;
                }
                else if (Input.GetMouseButton(1)) {
                    canClick = false;
                    missClick = true;
                }
            }
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if(canClick || missClick){
            logic.decreaseBPM();
            if(logic.isGameOver()){
                alive = false;
            }
            canClick = false;
        }
        else {
            if(other.gameObject.tag != "Reverse")logic.increaseBPM();
        }
    }
}
