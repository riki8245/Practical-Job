﻿using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class AudioController : MonoBehaviour
{
    public static AudioController AudioInstance;
    public AudioClip menuMusic;
    public AudioClip gameMusic;
    public AudioClip playerSteps;
    public AudioClip enemySteps;
    public AudioClip pressurePlateClanks;
    public AudioClip selectButton;
    public AudioClip trespassPortal;
    public AudioClip doorOpen;
    public AudioClip doorClose; 
    public AudioClip menuPop;
    public AudioClip confirmSelection;
    public AudioClip invalidSelection;
    public float MusicVolume;
    public float SfxVolume;

    bool menu = false;

    private IEnumerator coroutine;

    AudioSource [] emitter;

    void Awake(){
        MakeSingleton();
    }
    private void MakeSingleton()
    {
        if (AudioInstance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            AudioInstance = this;
            DontDestroyOnLoad(this.gameObject);
        }
    }

    void Start()
    {
        MusicVolume = GameManager.instance.MusicVolume;
        SfxVolume = GameManager.instance.SfxVolume;

        emitter = GetComponents<AudioSource>();
        emitter[0].clip = menuMusic;
        emitter[0].loop = true;
        emitter[1].clip = gameMusic;
        emitter[1].loop = true;
        emitter[2].clip = playerSteps;
        emitter[2].loop = true;
        emitter[3].clip = enemySteps;
        emitter[3].loop = true;
        emitter[4].clip = pressurePlateClanks;
        emitter[4].loop = false;
        emitter[5].clip = selectButton;
        emitter[5].loop = false;
        emitter[6].clip = trespassPortal;
        emitter[6].loop = false;
        emitter[7].clip = doorOpen;
        emitter[7].loop = false;
        emitter[8].clip = doorClose;
        emitter[8].loop = false;
        emitter[9].clip = menuPop;
        emitter[9].loop = false;
        emitter[10].clip = confirmSelection;
        emitter[10].loop = false;
        emitter[11].clip = invalidSelection;
        emitter[11].loop = false;
        for (int i = 0; i < 2; i++) emitter[i].volume = MusicVolume;
        for (int i = 2; i <= 11; i++) emitter[i].volume = SfxVolume;
        if (SceneManager.GetActiveScene().buildIndex == 0) GameObject.Find("UIController").GetComponent<UIControl>().SetSliders();
    }

    void Update()
    { 
        if (!menu && SceneManager.GetActiveScene().buildIndex == 0){
            menu = true;
            emitter[0].Play();
            coroutine = showSign(emitter[0]);
            StartCoroutine(coroutine);
            coroutine = showSign(emitter[1]);
            StartCoroutine(coroutine);
            //emitter[1].Stop();
        }
        else if(menu  && SceneManager.GetActiveScene().buildIndex != 0){
            menu = false;
            emitter[1].Play();
            coroutine = showSign(emitter[0]);
            StartCoroutine(coroutine);
            coroutine = showSign(emitter[1]);
            StartCoroutine(coroutine);
            //emitter[0].Stop();
        }
        
    }

    public void soundPlayerSteps(bool play)
    {
        if (play == true)
        {
            emitter[2].Play();
        }
        else 
        {
            emitter[2].Stop();
        }
    }

    public void soundEnemySteps(bool play)
    {
        if (play == true)
        {
            emitter[3].Play();
        }
        else 
        {
            emitter[3].Stop();
        }
    }

    public void soundPressurePlateClanks(bool play)
    {
        if (play == true)
        {
            emitter[4].Play();
        }
        /*else 
        {
            emitter[4].Stop();
        }*/
    }

    public void soundSelectButton(bool play)
    {
        if (play == true)
        {
            emitter[5].Play();
        }
        /*else 
        {
            emitter[5].Stop();
        }*/
    }

    public void soundTrespassPortal(bool play)
    {
        if (play == true)
        {
            emitter[6].Play();
        }
        /*else 
        {
            emitter[6].Stop();
        }*/
    }

    public void soundDoor(bool play)
    {
        if (play == true)
        {
            emitter[8].Stop();
            emitter[7].Play();
        }
        else 
        {
            emitter[7].Stop();
            emitter[8].Play();
        }
    }

    public void soundMenuPop(bool play)
    {
        if (play == true)
        {
            emitter[9].Play();
        }
        /*else 
        {
            emitter[9].Stop();
        }*/
    }

    public void soundConfirmSelection(bool play)
    {
        if (play == true)
        {
            emitter[10].Play();
        }
        /*else 
        {
            emitter[10].Stop();
        }*/
    }
    public void soundInvalidSelection(bool play)
    {
        if (play == true)
        {
            emitter[11].Play();
        }
        /*else 
        {
            emitter[11].Stop();
        }*/
    }

    public void StopAllSounds()
    {
        for(int i = 2; i <=11; i++)
        {
            emitter[i].Stop();
        }
    }


    public void SetMusicVolume(Slider slider)
    {
        GameManager.instance.MusicVolume = slider.value;
        for (int i = 0; i <= 11; i++) print("volume " + i + " " + emitter[i].volume);
        try
        {
            for (int i = 0; i < 2; i++) emitter[i].volume = slider.value;
        }
        catch(NullReferenceException ex){

        }
    }

    public void SetSfxVolume(Slider slider)
    {
        GameManager.instance.SfxVolume = slider.value;
        try
        {

            for (int i = 2; i <= 11; i++) emitter[i].volume = slider.value;
        }
        catch(NullReferenceException ex)
        {
            //Invoke("SfxVolume(slider)", 1f);
        }
    }

    private IEnumerator showSign(AudioSource emisor)
    {
        float init = menu == (emisor == emitter[0]) ? 0: 1;
        float target = init == 0 ? 1:0;
        float duration = 1.0f;
        float time = 0.0f;
        while(time <= duration){
            emisor.volume = Mathf.Lerp(init,target,time/duration);
            time += Time.deltaTime; 
            yield return null;
        } 
        if (target == 0)
        {
            emisor.Stop();
        }
    }
}
