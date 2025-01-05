using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    //Reference to the listener
    public AudioSource audioSource;
    //Reference to sound
    public AudioClip background1, background2, background3, background4, upgrade, mainButtonClick, choice, otherButtonClick, purchase, income;


    //It starts play background music
    private void Start()
    {
        StartCoroutine(windSound());
    }


    //Infinite loop to play background music
    private IEnumerator windSound()
    {
        while (true)
        {
            switch(UnityEngine.Random.Range(0, 4))
            {
                case 0:
                    PlayMusic(background1);
                    yield return new WaitForSeconds(121f);
                    break;
                case 1:
                    PlayMusic(background2);
                    yield return new WaitForSeconds(558f); 
                    break;
                case 2:
                    PlayMusic(background3);
                    yield return new WaitForSeconds(393f); 
                    break;
                case 3:
                    PlayMusic(background4);
                    yield return new WaitForSeconds(224f); 
                    break;
            }
        }
    }


    //This method strart play a music
    private void PlayMusic(AudioClip soundClip)
    {
        audioSource.clip = soundClip;
        audioSource.Play();
        Debug.Log("Playing music");
    }


    //This method start play short sounds
    public void PlayUpgradeSound()
    {
        audioSource.PlayOneShot(upgrade);
    }


    public void PlayMainButtonClickSound()
    {
        audioSource.PlayOneShot(mainButtonClick);
    }


    public void PlayChoiceSound()
    {
        audioSource.PlayOneShot(choice);
    }


    public void PlayClickSound()
    {
        audioSource.PlayOneShot(otherButtonClick);
    }


    public void PlayPurchaseSound()
    {
        audioSource.PlayOneShot(purchase);
    }


    public void PlayIncomeSound()
    {
        audioSource.PlayOneShot(income);
    }


    //This method stops all music
    public void StopSound()
    {
        if (audioSource.isPlaying)
            audioSource.Stop();
    }
}
