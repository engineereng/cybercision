using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChargeController : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    public moveBug[] bugScripts;
    public GameObject bugParentObject;

    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        
        bugScripts = bugParentObject.GetComponentsInChildren<moveBug>();
    }

    // Update is called once per frame
    void Update()
    {
        foreach (moveBug bug in bugScripts) {
            if (!bug.isShootable) {
                
            }
        }
        gameObject.transform.localScale += new Vector3(0, -50f);
        


    }
}
