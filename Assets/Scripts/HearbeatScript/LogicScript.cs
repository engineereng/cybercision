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
    public Text patientStatus;
    public GameObject winnerScreen;
    private bool started;
    public GameObject startScreen;

    public GameObject audioSounds;

    void Start(){
        heartBPM = 60;
        Timer = 60f;
        started = false;
        FindObjectOfType<AudioManager>().Play("Theme");
    }

    void Update(){
        if(started){
            if(isGameOver()){
                gameOverScreen.SetActive(true);
                patientStatus.text = "Patient is " + status();
            }
            else if(isWin()){
                winnerScreen.SetActive(true);
                patientStatus.text = "Patient is " + status();
                TimerText.text = "TIME: " + Timer.ToString("0");
            }
            else {
                Timer -= Time.deltaTime;
                patientStatus.text = "Patient is " + status();
                TimerText.text = "TIME: " + Timer.ToString("0");
                BPMText.text = "Beat Per Minute: " + heartBPM.ToString("0");
            }
        }
        else {
            if(Input.GetMouseButton(0)){
                started = true;
                startScreen.SetActive(false);
            }
        }
    }


    public void decreaseBPM(){
        heartBPM -= 9;
        if(heartBPM <= 0){
            FindObjectOfType<AudioManager>().Play("Flatline");
            heartBPM = 0;
        }
    }

    public void increaseBPM(){
        heartBPM += 2;
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

    public string status(){ 
        if(heartBPM == 0){
            return "Dead";
        }
        else if(heartBPM < 30){
            return "Dying";
        }
        else if(heartBPM < 60){
            return "Not Well";
        }
        else if(heartBPM < 80){
            return "Okay";
        }
        else {
            return "Healthy";
        }
    }

    public bool getStarted(){
        return started;
    }
}
