using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class VideoManager : MonoBehaviour
{
    public VideoClip[] clips;
    public VideoPlayer BPMPlayer;
    public VideoPlayer HeartPlayer;

    public VideoClip heartBeatClip;

    void Start(){
        HeartPlayer.clip = heartBeatClip;
    }

    public void Healthy(){
        print("Healthy");
        BPMPlayer.playbackSpeed = 1f;
        BPMPlayer.SetDirectAudioVolume(0, 0.1f);
    }
    public void NotWell(){
        print("Not Well");
        BPMPlayer.playbackSpeed = 1.5f;
        BPMPlayer.SetDirectAudioVolume(0, 0.3f);
    }
    public void Dying(){
        print("Dying");
        BPMPlayer.playbackSpeed = 2f;
        BPMPlayer.SetDirectAudioVolume(0, 0.5f);
    }
    public void Flatline(){
        BPMPlayer.clip = clips[1];
    }
    public bool checkOver() {
       long playerCurrentFrame = BPMPlayer.frame;
       long playerFrameCount = System.Convert.ToInt64(BPMPlayer.frameCount);
    //    print(playerCurrentFrame + " : " + playerFrameCount);
       if(playerCurrentFrame < playerFrameCount - 1){
            return false;
       }
       return true;
    }

    public void Pause(){
        BPMPlayer.Pause();
    }
    public void Play(){
        BPMPlayer.Play();
    }

    public void HeartBeat(){
        HeartPlayer.Play();
    }
}
