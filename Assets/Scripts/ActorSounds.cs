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
        AudioSource audioSource = m_audioSources[eventName];
        if (eventName == "footstep")
        {
            audioSource.pitch = Random.Range(0.98f, 1.02f);
            audioSource.volume = Random.Range(0.8f, 1.0f);
        }
        audioSource.Play();
    }
}
