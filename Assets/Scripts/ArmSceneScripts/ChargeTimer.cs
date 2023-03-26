using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ChargeTimer : MonoBehaviour
{
    //https://answers.unity.com/questions/1206187/timer-progress-bar-1.html
    [SerializeField] private float timeRemaining = 0f;
    public float maximumTime = 10;
    public Image sprite;
    private bool gameEnded = false;
    [SerializeField] private TMP_Text text;
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
            sprite.fillAmount = Mathf.Lerp(0, 1, percent);
            text.text = string.Format("{0:N0}", timeRemaining);
        } else {
            gameEnded = true;
            controller.setLost();
            fireCreator.setLost();
            Debug.Log("Time has run out!");
        }
    }
}
