using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CurrentController : MonoBehaviour
{
    private double distance;
    public double chargeSpeed;
    public float objectSpeed;
    private double chargePercentage;
    public GameObject chargeIndicator; 
    public TextMeshProUGUI scoreText;

    AudioSource electricHum;

    // Start is called before the first frame update
    void Start()
    {
        electricHum = GetComponent<AudioSource>();
        chargeSpeed *= 0.001;
        chargePercentage = 0;
    }

    // Update is called once per frame
    void Update()
    {
        distance = Vector3.Distance(Input.mousePosition, this.GetComponent<EdgeCollider2D>().ClosestPoint(Input.mousePosition));
        chargeIndicator.transform.localScale = new Vector3((float) chargePercentage, chargeIndicator.transform.localScale.y, chargeIndicator.transform.localScale.z);
        
        if (distance < 3)
        {
            ChargeBattery();
        }
        else if (distance > 10) {
            electricHum.Play();
        }
        
        MoveCurrent();
    }

    void ChargeBattery() {
        double amountToAdd = (30 - distance);
        if (amountToAdd > 0)
            chargePercentage += (amountToAdd * 0.001);

        Debug.Log("Charge Percentage:" + chargePercentage);
        scoreText.text = chargePercentage.ToString("#.00");
    }

    void MoveCurrent() {
        // 1233 is the point where the renderer is out of scene
        if(this.transform.position.x < 1174)
        {
          this.transform.Translate(Vector3.right * Time.deltaTime * objectSpeed, Space.World);
        }
    }
}
