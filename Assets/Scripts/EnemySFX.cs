using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySFX : MonoBehaviour {

    public AudioSource Intro;
    public AudioSource IntroEscalate;
    public AudioSource LevelTwoDecrease;
    public AudioSource LevelTwo;
    public AudioSource LevelTwoEscalate;
    public AudioSource LevelThreeDecrease;
    public AudioSource LevelThree;

    private GameObject player;
    private GameObject enemy;

    public float globalVolume;

	// Use this for initialization
	void Start () {
        // Set the value for our global volume
        globalVolume = 0.2f;

        // Set up our starting sound
        Intro.clip = Resources.Load<AudioClip>("Assets/SFX/Monster Sounds/Monster_Intro");
        Intro.volume = globalVolume;
        Intro.playOnAwake = true;
        Intro.loop = true;

        // Initialize other sounds
        IntroEscalate.clip = Resources.Load<AudioClip>("Assets/SFX/Monster Sounds/Monster_Intro_Escalate");
        IntroEscalate.volume = globalVolume;
        IntroEscalate.playOnAwake = false;
        IntroEscalate.loop = false;

        LevelTwoDecrease.clip = Resources.Load<AudioClip>("Assets/SFX/Monster Sounds/Monster_2_Decrease");
        LevelThreeDecrease.volume = globalVolume;
        LevelTwoDecrease.playOnAwake = false;
        LevelTwoDecrease.loop = false;

        LevelTwo.clip = Resources.Load<AudioClip>("Assets/SFX/Monster Sounds/Monster_Sound_2");
        LevelTwo.volume = globalVolume;
        LevelTwo.playOnAwake = false;
        LevelTwo.loop = true;

        LevelTwoEscalate.clip = Resources.Load<AudioClip>("Assets/SFX/Monster Sounds/Monster_2_Escalate");
        LevelTwoEscalate.volume = globalVolume;
        LevelTwoEscalate.playOnAwake = false;
        LevelTwoEscalate.loop = false;

        LevelThreeDecrease.clip = Resources.Load<AudioClip>("Assets/SFX/Monster Sounds/Monster_3_Decrease");
        LevelThreeDecrease.volume = globalVolume;
        LevelThreeDecrease.playOnAwake = false;
        LevelThreeDecrease.loop = false;

        LevelThree.clip = Resources.Load<AudioClip>("Assets/SFX/Monster Sounds/Monster_Sound_3");
        LevelThree.volume = globalVolume;
        LevelThree.playOnAwake = false;
        LevelThree.loop = true;

        // Initialize player object
        player = GameObject.FindGameObjectWithTag("Player");
        // Initialize enemy object
        enemy = GameObject.FindGameObjectWithTag("Enemy");
    }
	
	// Update is called once per frame
	void Update () {
        float playerDist;

        playerDist = getPlayerDist();
        
        if (playerDist > 10.0f) {

            if (LevelTwo.isPlaying)
                swapSounds(LevelTwo, Intro, LevelTwoDecrease);
        }
        else {

            if (playerDist > 5.0f) {

                if (Intro.isPlaying)
                    swapSounds(LevelTwo, Intro, IntroEscalate);
                else if (LevelThree.isPlaying)
                    swapSounds(LevelTwo, LevelThree, LevelThreeDecrease);
            }
            else {

                if (LevelTwo.isPlaying)
                    swapSounds(LevelThree, LevelTwo, LevelTwoEscalate);
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

    void swapSounds (AudioSource to, AudioSource from, AudioSource transition) {
        double startTick;
        double nextTick;

        nextTick = startTick = AudioSettings.dspTime;

        transition.PlayDelayed(9.922f);

        while (startTick + 9.922f != nextTick)
            nextTick = AudioSettings.dspTime;

        from.Stop();
        nextTick = startTick = AudioSettings.dspTime;
        to.PlayDelayed(9.922f);

        while (startTick + 9.922f != nextTick)
            nextTick = AudioSettings.dspTime;

        transition.Stop();
    }
}
