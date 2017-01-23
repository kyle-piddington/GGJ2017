﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySFX : MonoBehaviour {

    public AudioSource enemySource;
    public AudioSource enemyTransition;
    public AudioClip Intro;
    public AudioClip IntroEscalate;
    public AudioClip LevelTwoDecrease;
    public AudioClip LevelTwo;
    public AudioClip LevelTwoEscalate;
    public AudioClip LevelThreeDecrease;
    public AudioClip LevelThree;

    private GameObject player;
    private GameObject enemy;

    public float globalVolume;
    public float maxSoundDist;
    public float midSoundDist;
    public float minSoundDist;
    private bool inTransition;

	// Use this for initialization
	void Start () {
        // Set up our starting sound
        // Initialize player object
        player = GameObject.FindGameObjectWithTag("Player");
        // Initialize enemy object
        enemy = GameObject.FindGameObjectWithTag("Enemy");
        // Not currently switching between tracks

        if (getPlayerDist() > midSoundDist)
            enemySource.clip = Intro;
        else if (getPlayerDist() > minSoundDist)
            enemySource.clip = LevelTwo;
        else
            enemySource.clip = LevelThree;
        enemySource.loop = true;
        enemySource.maxDistance = maxSoundDist;
        enemySource.volume = globalVolume;
        enemySource.Play();
        
        enemyTransition.loop = false;
        enemyTransition.maxDistance = maxSoundDist;
        enemyTransition.volume = globalVolume;
        inTransition = false;
    }
	
	// Update is called once per frame
	void Update () {
        float playerDist;

        playerDist = getPlayerDist();
        
        if (playerDist > midSoundDist) {
                
            if (enemySource.clip == LevelTwo && !enemyTransition.isPlaying)
                swapSounds(Intro, LevelTwoDecrease);
        }
        else {

            if (playerDist > minSoundDist) {

                if (enemySource.clip == Intro && !enemyTransition.isPlaying)
                    swapSounds(LevelTwo, IntroEscalate);
                else if (enemySource.clip == LevelThree && !enemyTransition.isPlaying)
                    swapSounds(LevelTwo, LevelThreeDecrease);
            }
            else {

                if (enemySource.clip == LevelTwo && !enemyTransition.isPlaying)
                    swapSounds(LevelThree, LevelTwoEscalate);
            }
        }
	}

    float getPlayerDist() {
        float playerDist;
        Transform enemyPost;
        Transform playerPost;

        enemyPost = enemy.transform;
        playerPost = player.transform;

        playerDist = Vector3.Distance(enemyPost.position, playerPost.position);

        return playerDist;
    }

    void swapSounds (AudioClip to, AudioClip transition) {
        // Stop our current clip, load & play the transition
        enemySource.Stop();
        enemyTransition.clip = transition;
        enemyTransition.Play();

        // Load our next clip and play
        enemySource.clip = to;
        enemySource.PlayDelayed(9.922f);
        // No longer switching between tracks, reset bool
    }
}
