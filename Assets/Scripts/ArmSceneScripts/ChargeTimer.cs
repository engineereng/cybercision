using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChargeTimer : MonoBehaviour
{
    //https://answers.unity.com/questions/1206187/timer-progress-bar-1.html
    [SerializeField] private float timeRemaining = 0f;
    public float maximumTime = 20f;
    public Image sprite;


    void Start()
    {
        timeRemaining = maximumTime;
    }

    // Update is called once per frame
    void Update()
    {
        if (timeRemaining > 0)
        {
            timeRemaining -= Time.deltaTime;
            float percent = timeRemaining / maximumTime;
            if (percent < 0.25f) {
                sprite.color = Color.red;
            } else if (percent < 0.50f) {
                sprite.color = Color.yellow;
            } else {
                sprite.color = Color.green;
            }
            sprite.fillAmount = Mathf.Lerp(0, 1, percent);
        } else {
            FingerController controller = (FingerController) GetComponentInParent(typeof(FingerController));
            controller.setLost();
            Debug.Log("Time has run out!");
        }
    }
}
