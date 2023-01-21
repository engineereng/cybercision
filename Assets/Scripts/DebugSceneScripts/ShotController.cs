using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ShotController : MonoBehaviour
{
    SpriteRenderer[] shots;
    public moveBug[] bugScripts;
    public GameObject bugParentObject;
    private int curr = 0;
    public int seconds;

    public TextMeshProUGUI shootableText;

    // Start is called before the first frame update
    void Start()
    {
        shots = GetComponentsInChildren<SpriteRenderer>();
        bugScripts = bugParentObject.GetComponentsInChildren<moveBug>();
    }

    // Update is called once per frame
    void Update()
    {
        // if the iterator reaches 3 shots, bugs should not be shootable
        if (curr == shots.Length) {

            StartCoroutine(timearg(seconds));
            
        }
        if (Input.GetMouseButtonDown(0)) {
            shots[curr].color = Color.red;
            // increment curr
            curr++;
            Debug.Log("Curr is currently " + curr);
        }
    }

    IEnumerator timearg(int secs)
    {   
        // Debug.Log("Started Coroutine at timestamp : " + Time.time);
        foreach (moveBug bug in bugScripts) {
            bug.isShootable = false;
            shootableText.text = "false";
        }
        
        yield return new WaitForSecondsRealtime(secs);

        setYellow();
        setIsShootable();
        
        curr = 0;
        // Debug.Log("Finished Coroutine at timestamp : " + Time.time);
    }

    void setYellow() {
        foreach (SpriteRenderer child in shots) {
            child.color = Color.yellow;
        }
    }

    void setIsShootable() {
        foreach (moveBug bug in bugScripts) {
            bug.isShootable = true;
            shootableText.text = "true";
        } 
    }
}
