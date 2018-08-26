/* 
 * AudioFX.cs
 *
 * Script für Musik und Sounds.
 * Das Script ist an Player gebunden, die Sounds werden im Editor zugewiesen.
 * Musik und Sounds können extern angesteuert werden.
 *
 * Funktionen:
 *      AudioSource AddAudio(AudioClip clip, bool loop, bool playAwake, float volume)
 * 
 * Quellen:
 *  Sounds:             
 *      NAME            DATEI               LIZENZ          LINK                                                            URHEBER                 TITEL                           BEARBEITET
 *      clipBirds       00-birds-singing    CC BY 3.0       https://freesound.org/people/reinsamba/sounds/18765/            reinsamba               evening in the forest           JA, geschnitten
 *      clipFlattern    2-flattern          CC BY-NC 3.0    https://freesound.org/people/tigersound/sounds/9329/            tigersound              pigeon wings                    JA, geschnitten
 *      clipDamage      20-damage           CC BY 3.0       https://freesound.org/people/mwl500/sounds/54807/               mwl500                  good kick in the head sound     Nein
 *      clipShootEgg    17-egg-pop          CC0 1.0         https://freesound.org/people/oldedgar/sounds/97978/
 *      clipGameover    10-game-over        CC BY 3.0       https://freesound.org/people/deleted_user_877451/sounds/76376/  deleted_user_877451     Game_over                       Nein
 *      clipCollected   6-collect           CC BY 3.0       https://freesound.org/people/grunz/sounds/109663/               grunz                   success_low                     Nein
 *      clipSolved      5-success           CC0 1.0         https://freesound.org/people/fins/sounds/171671/
 *      clipError       4-error             CC BY 3.0       https://freesound.org/people/Autistic%20Lucario/sounds/142608/  Autistic Lucario        Error                           JA, geschnitten
 *      clipScore       15-scored           CC0 1.0         https://freesound.org/people/renatalmar/sounds/264981/
 *      clipSpawn       18-spawn            CC0 1.0         https://freesound.org/people/jeckkech/sounds/391658/
 *      clipDuck        22-duck             CC BY 3.0       https://freesound.org/people/dobroide/sounds/185134/            dobroide                20130403_duck.04                JA, geschnitten
 *      clipSheep       23-sheep            CC BY 3.0       https://freesound.org/people/n_audioman/sounds/321967/          n_audioman              Sheep_Bleat                     JA, geschnitten
 *      clipCow         21-cow              CC BY 3.0       https://freesound.org/people/Benboncan/sounds/58277/            Benboncan               Cow                             JA, geschnitten
 *      clipMegaCow     21-mega_cow         CC BY 3.0       https://freesound.org/people/Benboncan/sounds/58277/            Benboncan               Cow                             JA, geschnitten und transponiert
 *      clipStones      24-boom             CC BY 3.0       https://freesound.org/people/juskiddink/sounds/108640/          juskiddink              Distant explosion               Nein
 *      clipBing        9-bing              CC0 1.0         https://freesound.org/people/LittleRainySeasons/sounds/335908/
 *
 *  Code:
 *      Die Funktion AddAudio ist einem Beitrag von "adrianov" vom  28.01.2016 auf answers.unity.com entnommen. 
 *      Link: https://answers.unity.com/questions/240468/how-to-play-multiple-audioclips-from-the-same-obje.html
 * 
 * Stand 25.08.2018
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioFX : MonoBehaviour
{
    // AudioClips - Storage der Clips
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
    
    // AudioSource - Zum Ansteuern des angepassten Clips
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
    
    /*
     *  Clips anpassen und AudioSource setzen. 
     *  Quelle siehe Zeile 32.
     * 
     *  @clip:      Clip, der als AudioSource gesetzt werden soll
     *  @loop:      Ob Clip geloopt werden soll oder nicht
     *  @playAwake: Ob Clip on awake starten soll oder nicht
     *  @volume:    Lautstärke des Clips als float
     */
    public AudioSource AddAudio(AudioClip clip, bool loop, bool playAwake, float volume)
    {
        AudioSource setAudio = gameObject.AddComponent<AudioSource>();

        setAudio.clip           = clip;
        setAudio.loop           = loop;
        setAudio.playOnAwake    = playAwake;
        setAudio.volume         = volume;

        return setAudio;
    }
}