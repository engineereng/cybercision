using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChargeTimer : MonoBehaviour
{
    //https://answers.unity.com/questions/1206187/timer-progress-bar-1.html
    [SerializeField] private float timeRemaining = 0f;
    public float maximumTime = 10;
    public Image sprite;
    private bool gameEnded = false;
    [SerializeField] private FingerController controller;
    [SerializeField] private FireCreator fireCreator;

    void Start()
    {
        timeRemaining = maximumTime;
    }

    public void fireDamage()
    {
        timeRemaining -= 5;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (timeRemaining > 0 && !gameEnded)
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
            gameEnded = true;
            controller.setLost();
            fireCreator.setLost();
            Debug.Log("Time has run out!");
        }
    }
}
