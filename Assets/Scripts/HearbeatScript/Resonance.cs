using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Resonance : MonoBehaviour
{
    public float speed;

    public Vector3 scale;


    public Image resonance;

    public Color color;
    // Start is called before the first frame update
    
    void Start()
    {
        speed = 50f;
    }

    // Update is called once per frame
    void Update()
    {
        scale = transform.localScale;
        scale.x += speed*Time.deltaTime;
        scale.y += speed*Time.deltaTime;
        transform.localScale = scale;
        resonance = GetComponent<Image>();
        color = resonance.color;
        color.a -= 3.5f*Time.deltaTime;
        resonance.color = color;
        if(color.a <= 0){
            Destroy(gameObject);
        }
    }
}
