using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioSource soundEffects;
    public AudioSource music;

    public AudioClip machine;
    public AudioClip item;
    public AudioClip order;
    public AudioClip levelComplete;
    public AudioClip machineFinished;
    public AudioClip negativeScore;
    public AudioClip nailHammer;

    private int currentPriority = 0;

    public void PlayMachine()
    {
        PlaySound(machine, 1);
    }

    public void PlayItem()
    {
        PlaySound(item, 1);
    }

    public void PlayOrder()
    {
        PlaySound(order, 2);
    }

    public void PlayLevelComplete()
    {
        PlaySound(levelComplete, 3);
    }

    public void PlayMachineComplete()
    {
        PlaySound(machineFinished, 1);
    }

    public void PlayNegativeScore()
    {
        PlaySound(negativeScore, 2);
    }

    public void PlayNailHammer()
    {
        PlaySound(nailHammer, 2);
    }

    public void PlayBreakItem()
    {
        PlaySound(negativeScore, 2);
    }

    private void PlaySound(AudioClip clip, int priority)
    {
        if (!soundEffects.isPlaying || priority >= currentPriority)
        {
            soundEffects.clip = clip;
            soundEffects.Play();
            currentPriority = priority;
        }
    }
}
