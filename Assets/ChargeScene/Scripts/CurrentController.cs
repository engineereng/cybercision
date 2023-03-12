using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CurrentController : MonoBehaviour
{

    public float chargeSpeed;
    public float objectSpeed;

    public float chargeDistance;

    public GameObject chargeIndicator; 
    public TextMeshProUGUI scoreText;

    private AudioSource electricHum;
    
    private float chargePercentage;

    // Start is called before the first frame update
    void Start()
    {
        electricHum = GetComponent<AudioSource>();
        chargeSpeed *= 0.001f;
        chargePercentage = 0;
        chargeDistance = 3f;
        
        electricHum.Play();
        electricHum.Pause();


        RectTransform image = chargeIndicator.GetComponent<RectTransform>();

        image.sizeDelta = new Vector2(0.0f, image.sizeDelta.y);

    }

    // Update is called once per frame
    void Update()
    {

        Vector2 closest = GetComponent<EdgeCollider2D>().ClosestPoint(Input.mousePosition);
        Vector2 mousePosition = Input.mousePosition;

        float distance = Vector2.Distance(mousePosition, closest);

        if (distance < chargeDistance)
        {

            float chargeAmount = (chargeDistance - distance) * chargeSpeed;

            //Speed up or slow down the charge sound depending on how close we are!
            electricHum.pitch = 1.0f + chargeAmount;
            electricHum.UnPause();

            ChargeBattery(chargeAmount);
        }
        else
        {
            electricHum.Pause();
        }
        
        MoveCurrent();
    }

    void ChargeBattery(float amount) {
        chargePercentage += amount;

        if(chargePercentage > 100.0f)
        {
            chargePercentage = 100.0f;
        }

        RectTransform image = chargeIndicator.GetComponent<RectTransform>();

        //image.transform.localScale = new Vector3((float)chargePercentage, image.transform.localScale.y, image.transform.localScale.z);

        image.sizeDelta = new Vector2(chargePercentage, image.sizeDelta.y);

        scoreText.text = chargePercentage.ToString("#.00");   
    }

    void MoveCurrent() {

        if(transform.position.x < Camera.main.pixelWidth + 50)//Add 50 for leeway
        {
            transform.Translate(Vector3.right * Time.deltaTime * objectSpeed, Space.World);
        }

    }
}
