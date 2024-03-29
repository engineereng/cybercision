using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Video;
public class LogicScript : MonoBehaviour
{
    private int heartBPM;
    private string prevStatus;
    public Text BPMText;
    public GameObject gameOverScreen;
    private float Timer;
    public Text TimerText;
    public Text patientStatus;
    public GameObject winnerScreen;
    private bool started;
    public GameObject startScreen;
    public VideoManager VideoManager;

    private float delay;

    private float over;

    void Start(){
        VideoManager = FindObjectOfType<VideoManager>();
        heartBPM = 60;
        Timer = 30f;
        started = false;
        FindObjectOfType<AudioManager>().Play("Theme");
        prevStatus = "Healthy";
        delay = 0;
        VideoManager.HeartBeat();
    }

    void Update(){
        delay += Time.deltaTime;
        if(started){
            if(isGameOver()){
                over += Time.deltaTime;
                gameOverScreen.SetActive(true);
                patientStatus.text = "PATIENT STATUS: " + status();
                FindObjectOfType<AudioManager>().Mute("Theme");
                VideoManager.Flatline();
                if(over > 3){
                    MinigameManager.GetManager().FinishMinigame(false);
                }
            }
            else if(isWin()){
                over += Time.deltaTime;
                winnerScreen.SetActive(true);
                patientStatus.text = "PATIENT STATUS: " + status();
                TimerText.text = "TIME: " + Timer.ToString("0");
                FindObjectOfType<AudioManager>().Mute("Theme");
                if(over > 3){
                    MinigameManager.GetManager().FinishMinigame(true);
                }
            }
            else {
                Timer -= Time.deltaTime;
                patientStatus.text = "PATIENT STATUS: " + status();
                TimerText.text = "TIME: " + Timer.ToString("0");
                BPMText.text = "BEATS PER MINUTE: \n" + heartBPM.ToString("0");
            }
            if(delay >= 2 && VideoManager.checkOver() && statusChanged()){
                delay = 0;
                VideoManager.Pause();
                Debug.Log("checked");
                print("change animation");  
                if(status() == "Dying"){
                    VideoManager.Dying();
                }
                else if(status() == "Not Well"){
                    VideoManager.NotWell();
                }
                else{
                    VideoManager.Healthy();
                }
                Debug.Log("changed!!");
                VideoManager.Play();
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
            FindObjectOfType<AudioManager>().Play("GameOver");
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
            BPMText.text = "BEATS PER MINUTE: \n" + heartBPM.ToString("0");
            return true;
        };
        return false;
    }
    public bool isWin(){
        return Timer <= 0;
    }

    public string status(){ 
        if(heartBPM == 0){
            return "DEAD";
        }
        else if(heartBPM < 30){
            return "DYING";
        }
        else if(heartBPM < 60){
            return "NOT WELL";
        }
        else if(heartBPM < 80){
            return "OKAY";
        }
        else {
            return "HEALTHY";
        }
    }

    public bool getStarted(){
        return started;
    }

    public bool statusChanged(){
        if(status() != prevStatus){
            prevStatus = status();
            return true;
        }
        return false;
    }
}
