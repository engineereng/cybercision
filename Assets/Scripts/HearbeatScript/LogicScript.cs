using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class LogicScript : MonoBehaviour
{
    private int heartBPM ;
    public Text BPMText;

    public GameObject gameOverScreen;

    private float Timer;
    public Text TimerText;

    public GameObject winnerScreen;

    void Start(){
        heartBPM = 60;
        Timer = 10f;
    }

    void Update(){
        if(isGameOver())gameOverScreen.SetActive(true);
        else if(isWin()){
            winnerScreen.SetActive(true);
            TimerText.text = "TIME: " + Timer.ToString("0");
        }
        else {
            Timer -= Time.deltaTime;
            TimerText.text = "TIME: " + Timer.ToString("0");
            BPMText.text = "Beat Per Minute: " + heartBPM.ToString("0");
        }
    }


    public void decreaseBPM(){
        heartBPM -= 10;
        if(heartBPM <= 0){
            heartBPM = 0;
        }
    }

    public void increaseBPM(){
        heartBPM += 3;
        if(heartBPM > 100){
            heartBPM = 100;
        }
    }

    public void restartGame(){
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public bool isGameOver(){
        if(heartBPM == 0){
            BPMText.text = "Beat Per Minute: " + heartBPM.ToString("0");
            return true;
        };
        return false;
    }
    public bool isWin(){
        return Timer <= 0;
    }
}
