using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{

    [Header("Background Music")]
    public AudioClip officeBGMClip;
    public AudioClip museumBGMClip;
    public AudioClip guardHouseBGMClip;
    public AudioClip blackMarketBGMClip;
    public AudioClip artist1HouseBGMClip;
    public AudioClip artist2HouseBGMClip;
    public AudioClip suburbsBGMClip;

    [Header("SFX")]
    public AudioClip playerBarkSfx;
    public AudioClip museumOwnerBarkSfx;
    public AudioClip securityGuardBarkSfx;
    public AudioClip fanBarkSfx;
    public AudioClip artist1BarkSfx;
    public AudioClip artist2BarkSfx;
    public AudioClip thiefCatMeowSfx;
    public AudioClip fatCatMeowSfx;
    public AudioClip goodestBoyBarkSfx;

    public static AudioManager instance;

    AudioSource audioSource;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);

            SceneManager.sceneLoaded += OnSceneLoaded;
            audioSource = GetComponent<AudioSource>();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void PlayClip(AudioClip clip)
    {
        if (!clip)
        {
            return;
        }

        GameObject go = new GameObject();
        AudioSource audioS = go.AddComponent<AudioSource>();
        audioS.clip = clip;
        DontDestroyOnLoad(go);
        Destroy(go, clip.length * 1.1f);
        audioS.Play();
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode loadSceneMode)
    {

        AudioClip clip;

        switch (scene.name)
        {
            case "office":
            case "office_end":
            case "crossroad_start":
                clip = officeBGMClip;
                break;
            case "museum":
                clip = museumBGMClip;
                break;
            case "guard_house":
                clip = guardHouseBGMClip;
                break;
            case "blackmarket":
                clip = blackMarketBGMClip;
                break;
            case "artist1_house":
                clip = artist1HouseBGMClip;
                break;
            case "artist2_house":
                clip = artist2HouseBGMClip;
                break;
            case "suburbs":
                clip = suburbsBGMClip;
                break;
            default:
                Debug.LogWarning("No BGM for scene " + scene.name);
                return;
        }

        audioSource.clip = clip;
        audioSource.Play();
    }

    public void PlaySfxForCharacter(string characterId)
    {
        AudioClip clip;

        switch (characterId)
        {
            case "player":
                clip = playerBarkSfx;
                break;
            case "museum_owner":
                clip = museumOwnerBarkSfx;
                break;
            case "security_guard":
                clip = securityGuardBarkSfx;
                break;
            case "artist_1":
                clip = artist1BarkSfx;
                break;
            case "artist_2":
                clip = artist2BarkSfx;
                break;
            case "fan":
                clip = fanBarkSfx;
                break;
            case "thief_cat":
                clip = thiefCatMeowSfx;
                break;
            case "fat_cat":
                clip = fatCatMeowSfx;
                break;
            case "goodest_doggo":
                clip = goodestBoyBarkSfx;
                break;
            default:
                Debug.LogWarning("Unknown clip id " + characterId);
                return;
        }

        AudioSource.PlayClipAtPoint(clip, Camera.main.transform.position);
    }

}
