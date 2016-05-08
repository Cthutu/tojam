using UnityEngine;
using System.Collections.Generic;


[System.Serializable()]
public class AudioDefinitions : SerializableDictionaryBase<string, AudioClip> { }

public class ActorSounds : MonoBehaviour {

    [SerializeField()]
    public AudioDefinitions m_eventClips;
    private Dictionary<string, AudioSource> m_audioSources;
	// Use this for initialization
	void Start () {
        m_audioSources = new Dictionary<string, AudioSource>();

        foreach (var pairIt in m_eventClips)
        {
            m_audioSources[pairIt.Key] = gameObject.AddComponent<AudioSource>();
            m_audioSources[pairIt.Key].clip = pairIt.Value;
        }
	}

	// Update is called once per frame
	void Update () {
	
	}


    public void TriggerSound(string eventName)
    {
        m_audioSources[eventName].Play();
    }
}
