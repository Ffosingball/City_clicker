using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    //Reference to the listener
    public AudioSource musicSource, soundSource;
    //Reference to sound
    public AudioClip background1, background2, background3, background4, upgrade, mainButtonClick, choice, otherButtonClick, purchase, income, changeValue;
    private Coroutine changeValueSoundPlaying=null;


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
        musicSource.clip = soundClip;
        musicSource.Play();
        Debug.Log("Playing music");
    }


    //This method start play short sounds
    public void PlayUpgradeSound()
    {
        soundSource.PlayOneShot(upgrade);
    }


    public void PlayMainButtonClickSound()
    {
        soundSource.PlayOneShot(mainButtonClick);
    }


    public void PlayChoiceSound()
    {
        soundSource.PlayOneShot(choice);
    }


    public void PlayClickSound()
    {
        soundSource.PlayOneShot(otherButtonClick);
    }


    public void PlayPurchaseSound()
    {
        soundSource.PlayOneShot(purchase);
    }


    public void PlayIncomeSound()
    {
        soundSource.PlayOneShot(income);
    }


    public void PlayChangeValueSound()
    {
        if(changeValueSoundPlaying==null)
            changeValueSoundPlaying = StartCoroutine(ChangeValueSoundCourutine());
    }

    private IEnumerator ChangeValueSoundCourutine()
    {
        soundSource.PlayOneShot(changeValue);
        yield return new WaitForSeconds(1f); 
        changeValueSoundPlaying = null;
    }


    //This method stops all music
    public void StopSound()
    {
        if (soundSource.isPlaying)
            soundSource.Stop();
        
        if(musicSource.isPlaying)
            musicSource.Stop();
    }


    public void updateMusciVolume(float volume)
    {
        musicSource.volume = volume;
    }


    public void updateSoundVolume(float volume)
    {
        soundSource.volume = volume;
    }
}
