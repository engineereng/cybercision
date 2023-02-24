using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class VideoManager : MonoBehaviour
{
    public VideoClip[] clips;
    public VideoPlayer BPMPlayer;
    public void Healthy(){
        print("wow");
        BPMPlayer.clip = clips[0];
        BPMPlayer.playbackSpeed = 1f;
        BPMPlayer.SetDirectAudioVolume(0, 0.1f);
    }
    public void NotWell(){
        BPMPlayer.clip = clips[0];
        BPMPlayer.playbackSpeed = 1.2f;
        BPMPlayer.SetDirectAudioVolume(0, 0.15f);
    }
    public void Dying(){
        BPMPlayer.clip = clips[0];
        BPMPlayer.playbackSpeed = 1.4f;
        BPMPlayer.SetDirectAudioVolume(0, 0.3f);
    }
    public void Flatline(){
        BPMPlayer.clip = clips[1];
    }
    public bool checkOver() {
       long playerCurrentFrame = BPMPlayer.frame;
       long playerFrameCount = System.Convert.ToInt64(BPMPlayer.frameCount);
       if(playerCurrentFrame < playerFrameCount){
            return false;
       }
       else return true;
    }
}
