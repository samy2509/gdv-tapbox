using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioFX : MonoBehaviour
{
    //AudioClips - Storage der Clips
    public AudioClip clipBirds;
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
    public AudioClip clipStones;
    public AudioClip clipBing;
    

    //AudioSource - Zum Ansteuern des angepassten AudioFX
    public AudioSource birds;
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
	public AudioSource stones;
    public AudioSource bing;

	public void Awake()
    {
        Scene scene = SceneManager.GetActiveScene();

        birds                   = AddAudio(clipBirds, true, true, 0.7f);
        level1Background        = AddAudio(clipLevel1, true, true, 0.7f);
        nebenlevelBackground    = AddAudio(clipNebenlevel, true, true, 0.5f);

        flattern    = AddAudio(clipFlattern, false, false, 1.0f);
        damage      = AddAudio(clipDamage, false, false, 1.0f);
        shootEgg    = AddAudio(clipShootEgg, false, false, 0.75f);
        gameover    = AddAudio(clipGameover, false, false, 1.0f);
        collected   = AddAudio(clipCollected, false, false, 1.0f);
        solved      = AddAudio(clipSolved, false, false, 1.0f);
        error       = AddAudio(clipError, false, false, 0.5f);
        score       = AddAudio(clipScore, false, false, 1.0f);
        spawn       = AddAudio(clipSpawn, false, false, 0.5f);
        duck        = AddAudio(clipDuck, false, false, 0.15f);
        sheep       = AddAudio(clipSheep, false, false, 0.15f);
        cow         = AddAudio(clipCow, false, false, 0.10f);
        megaCow     = AddAudio(clipMegaCow, false, false, 0.15f);
        stones      = AddAudio(clipStones, false, false, 1.0f);
        bing        = AddAudio(clipBing, false, false, 0.25f);
        

        if (scene.name == "Level1") 
        {
            nebenlevelBackground.Stop();
            level1Background.Play();
            birds.Play();

        }
        else if (scene.name == "Nebenlevel")  
        {
            birds.Stop();
            level1Background.Stop();
            nebenlevelBackground.Play();
        }
        else if (scene.name == "MainMenu")
        {
            nebenlevelBackground.Stop();
            level1Background.Stop();
            birds.Play();
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