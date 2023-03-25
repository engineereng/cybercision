using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CurrentNode : MonoBehaviour
{

    public Sprite hoveredSprite;

    public CurrentSpawner spawner;

    public float objectSpeed = 100f;

    private bool used;

    // Start is called before the first frame update
    void Start()
    {
        used = false;
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 mousePosition = Input.mousePosition;
        
        if(Vector2.Distance(mousePosition, Camera.main.WorldToScreenPoint(transform.position)) < 50 && !used)
        {
            Image image = GetComponent<Image>();
            image.sprite = hoveredSprite;
            used = true;
            spawner.ChargeBattery(0.5f);
        }

        MoveCurrent();
    }

    void MoveCurrent()
    {
        if (transform.position.x < Camera.main.pixelWidth + 50)//Add 50 for leeway
        {
            transform.Translate(Vector3.right * Time.deltaTime * objectSpeed, Space.World);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
