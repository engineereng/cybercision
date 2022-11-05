using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotController : MonoBehaviour
{
    SpriteRenderer[] shots;
    private int curr = 0;
    public int seconds;

    // Start is called before the first frame update
    void Start()
    {
        shots = GetComponentsInChildren<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (curr == shots.Length) {
            Debug.Log("Clicked, test = " + curr);
            StartCoroutine(timearg(seconds));
            curr = 0;
        }
        else if (Input.GetMouseButtonDown(0)) {
            shots[curr++].color = Color.red;
            Debug.Log("Clicked, curr = " + curr);
        }
        
    }

    IEnumerator timearg(int secs)
    {
        yield return new WaitForSeconds(secs);
        Debug.Log("Clicked, trtdgd = " + curr);
        foreach (SpriteRenderer child in shots) {
            child.color = Color.yellow;
        }
        curr = 0;
    }
}
