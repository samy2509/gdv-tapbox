using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioFX : MonoBehaviour
{
    //AudioClips - Zum Storen der Clips
    public AudioClip clipLevel1;
    public AudioClip clipNebenlevel;

    public AudioClip clipFlattern;
    public AudioClip clipDamage;
    public AudioClip clipShootEgg;
    public AudioClip clipGameover;
    public AudioClip clipCollected;
    public AudioClip clipSolved;
    public AudioClip clipError;
    public AudioClip clipScore;
    public AudioClip clipSpawn;
    public AudioClip clipDuck;
    public AudioClip clipSheep;
    public AudioClip clipCow;
    public AudioClip clipMegaCow;
    //public AudioClip clipStones;

    //AudioSource - Zum Ansteuern der angepassten AudioSpur
    public AudioSource level1Background;
    public AudioSource nebenlevelBackground;

    public AudioSource flattern;
    public AudioSource damage;
    public AudioSource shootEgg;
    public AudioSource gameover;
    public AudioSource collected;
    public AudioSource solved;
    public AudioSource error;
    public AudioSource score;
    public AudioSource spawn;
    public AudioSource duck;
    public AudioSource sheep;
    public AudioSource cow;
    public AudioSource megaCow;
	//public AudioSource stones;

	public void Awake()
    {
        Scene scene = SceneManager.GetActiveScene();

        level1Background        = AddAudio(clipLevel1, true, true, 0.7f);
        nebenlevelBackground    = AddAudio(clipNebenlevel, true, true, 0.5f);

        flattern    = AddAudio(clipFlattern, false, false, 1.0f);
        damage      = AddAudio(clipDamage, false, false, 1.0f);
        shootEgg    = AddAudio(clipShootEgg, false, false, 1.0f);
        gameover    = AddAudio(clipGameover, false, false, 1.0f);
        collected   = AddAudio(clipCollected, false, false, 1.0f);
        solved      = AddAudio(clipSolved, false, false, 1.0f);
        error       = AddAudio(clipError, false, false, 0.5f);
        score       = AddAudio(clipScore, false, false, 1.0f);
        spawn       = AddAudio(clipSpawn, false, false, 1.0f);
        duck        = AddAudio(clipDuck, false, false, 0.15f);
        sheep       = AddAudio(clipSheep, false, false, 0.15f);
        cow         = AddAudio(clipCow, false, false, 0.15f);
        megaCow     = AddAudio(clipMegaCow, false, false, 0.15f);
        //stones      = AddAudio(clipStones, false, false, 0.5f);

        if (scene.name == "Level1") 
        {
            nebenlevelBackground.Stop();
            level1Background.Play();

        }
        else if (scene.name == "Nebenlevel")  
        {
            level1Background.Stop();
            nebenlevelBackground.Play();
        }
        
    }

    public AudioSource AddAudio(AudioClip clip, bool loop, bool playAwake, float volume)
    {
        AudioSource setAudio = gameObject.AddComponent<AudioSource>();

        setAudio.clip = clip;
        setAudio.loop = loop;
        setAudio.playOnAwake = playAwake;
        setAudio.volume = volume;

        return setAudio;
    }
}