using UnityEngine;
using UnityEngine.Audio;
using System.Collections.Generic;

public class AudioHandler : MonoBehaviour {

    public static AudioHandler instance = null;

    private GameHandler gh;
    private SceneHandler sh;

    #region Volume Controls
    [SerializeField]
    [Range(0.0f, 1.0f)]
    private float masterVolume;
    public float MasterVolume { get { return masterVolume; } set { masterVolume = Mathf.Clamp01(value); } }

    [SerializeField]
    [Range(0.0f, 1.0f)]
    private float uiVolume;
    public float UIVolume { get { return uiVolume; } set { uiVolume = Mathf.Clamp01(value); } }

    [SerializeField]
    [Range(0.0f, 1.0f)]
    private float sfxVolume;
    public float SfxVolume { get { return sfxVolume; } set { sfxVolume = Mathf.Clamp01(value); } }

    [SerializeField]
    [Range(0.0f, 1.0f)]
    private float musicVolume;
    public float MusicVolume { get { return musicVolume; } set { musicVolume = Mathf.Clamp01(value); } }
    #endregion

    #region Audio Mixer / Groups
    [SerializeField]
    private AudioMixer mainMixer;
    [SerializeField]
    private AudioMixerGroup masterMixerGroup;
    [SerializeField]
    private AudioMixerGroup uiMixerGroup;
    [SerializeField]
    private AudioMixerGroup sfxMixerGroup;
    [SerializeField]
    private AudioMixerGroup musicMixerGroup;
    #endregion

    [SerializeField]
    public List<Sound> uiSoundsToLoad;
    private IDictionary<string, Sound> uiSounds;
    [SerializeField]
    private List<Sound> sfxSoundsToLoad;
    private IDictionary<string, Sound> sfxSounds;
    [SerializeField]
    private List<Sound> musicSoundsToLoad;
    private IDictionary<string, Sound> musicSounds;

    private void Awake()
    {
        //Confirm Singleton/only instance.
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        gh = FindObjectOfType<GameHandler>();
        sh = FindObjectOfType<SceneHandler>();

        InitialiseAudioMixers();

        //Initialise audio assets
        InitialiseAudioAssets();

        //Ensures object remains present between scenes.
        DontDestroyOnLoad(this.gameObject);
    }

    void InitialiseAudioMixers()
    {
        //Initialise mixer audio settings
        SetMixerVolume(masterMixerGroup, masterVolume);
        SetMixerVolume(uiMixerGroup, uiVolume);
        SetMixerVolume(sfxMixerGroup, sfxVolume);
        SetMixerVolume(musicMixerGroup, musicVolume);
    }

    void InitialiseAudioAssets()
    {
        //Init dictionaries
        uiSounds = new Dictionary<string, Sound>();
        sfxSounds = new Dictionary<string, Sound>();
        musicSounds = new Dictionary<string, Sound>();

        //initialise ui files
        if (uiSoundsToLoad.Count != 0)
        {
            foreach (Sound s in uiSoundsToLoad)
            {
                s.source = gameObject.AddComponent<AudioSource>();
                s.source.outputAudioMixerGroup = uiMixerGroup;
                s.source.clip = s.clip;
                uiSounds.Add(s.name, s);
            }
        }
        else{ Debug.LogWarning("No UI Audio listed to be loaded. Check Audio Manager."); }

        //initialise sfx files
        if (sfxSoundsToLoad.Count != 0)
        {
            foreach (Sound s in sfxSoundsToLoad)
            {
                s.source = gameObject.AddComponent<AudioSource>();
                s.source.outputAudioMixerGroup = sfxMixerGroup;
                s.source.clip = s.clip;
                sfxSounds.Add(s.name, s);
            }
        }
        else { Debug.LogWarning("No SFX Audio listed to be loaded. Check Audio Manager."); }

        //initialise music files
        if (musicSoundsToLoad.Count != 0)
        {
            foreach (Sound s in musicSoundsToLoad)
            {
                s.source = gameObject.AddComponent<AudioSource>();
                s.source.outputAudioMixerGroup = musicMixerGroup;
                s.source.clip = s.clip;
                musicSounds.Add(s.name, s);
            }
        }
        else { Debug.LogWarning("No Music Audio listed to be loaded. Check Audio Manager."); }

    }

    private void SetMixerVolume(AudioMixerGroup mixer, float volume)
    {
        mixer.audioMixer.SetFloat("volume", Mathf.Clamp01(volume));
    }

    //TODO add checking/loading for when saving code is sorted.

    // Update is called once per frame
    void Update()
    {

    }

    //public play functions
    public void PlayUISound(string name)
    {
        PlayDictSound(name, uiSounds);
    }

    public void PlaySFX(string name)
    {
        PlayDictSound(name, sfxSounds);
    }

    public void PlayMusic(string name)
    {
        PlayDictSound(name, musicSounds);
    }

    //internal play function that can be overloaded for additional parameters like looping, pitch etc.
    private void PlayDictSound(string name, IDictionary<string, Sound> dict)
    {
        Sound s;
        dict.TryGetValue(name, out s);
        if (s == null)
        {
            Debug.LogWarning("Sound file -- \"" + name +
            "\" -- is not available. Please check assets and loading in Audio Manager");
            return;
        }
        s.source.Play();
    }

}
