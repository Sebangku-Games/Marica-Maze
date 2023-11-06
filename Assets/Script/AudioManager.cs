using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    public AudioSource audioSound;

    public AudioClip button;
    public AudioClip pageFlip;
    public AudioClip pageTurn;
    public AudioClip gameOverFail;
    public AudioClip gameOverSuccess;
    //public AudioClip rotate;
    public AudioClip walk2Seconds;
    public AudioClip walk1Seconds;

    private void Awake()
    {
        if (instance != null && instance != this)
            Destroy(this);
        else
            instance = this;

        audioSound = GetComponent<AudioSource>();
    }

    private void Start()
    {
        StartCoroutine(CheckSound());
    }

    IEnumerator CheckSound()
    {
        yield return new WaitForEndOfFrame();
        if (!GameData.InstanceData.onSound)
        {
            audioSound.Stop();
        }
    }

    public void soundSettings(bool on){
        if (on)
        {
            audioSound.Play();
        }
        else
        {
            audioSound.Stop();
        }
    }

    public void PlayButton(){
        if (GameData.InstanceData.onSound)
            audioSound.PlayOneShot(button);
    }

    public void PlayPageFlip(){
        if (GameData.InstanceData.onSound)
            audioSound.PlayOneShot(pageFlip);
    }

    public void PlayGameOverFail(){
        if (GameData.InstanceData.onSound)
            audioSound.PlayOneShot(gameOverFail);
    }

    public void PlayGameOverSuccess(){
        if (GameData.InstanceData.onSound)
            audioSound.PlayOneShot(gameOverSuccess);
    }

    public void PlayRotateRoad(){
        if (GameData.InstanceData.onSound)
            // random use between pageFlip and PageTurn
            audioSound.PlayOneShot(Random.Range(0, 2) == 0 ? pageFlip : pageTurn);
    }

    public void PlayWalk2(){
        if (GameData.InstanceData.onSound)
            audioSound.PlayOneShot(walk2Seconds);
    }

    public void PlayWalk1(){
        if (GameData.InstanceData.onSound)
            audioSound.PlayOneShot(walk1Seconds);
    }
}
