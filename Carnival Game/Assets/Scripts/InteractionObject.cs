using System.Collections;
using System.Collections.Generic;
using UnityEngine.Serialization;
using UnityEngine.Events;
using UnityEngine;

public class InteractionObject : MonoBehaviour {

    // Whether this object should be added to the player's inventory
    [Tooltip("Whether this object should be added to the player's inventory")]
    public bool isPickup;

    // Whether the object can be interacted with or "Activated"
    private bool interactable = true;


    // Created this class so that people can set up sounds in the inspector
    // without having to created a crap-ton of AudioSource components.
    [System.Serializable]
    public class InteractionAudio
    {
        public AudioClip audioClip;
        public bool loop = false;

        [Range(0f,1f)]
        public float volume = 0.5f;

        public float delay = 0.0f;
    }


    [Tooltip("Object required to interact with this successfully.")]
    public GameObject[] requiredObjects;

    [Tooltip("Sounds played when this object is interacted with.")]
    public InteractionAudio[] interactionSounds;

    [Tooltip("Methods invoked when this object is interacted with.")]
    public UnityEvent interactionSuccessTriggers;

    [Tooltip("Methods invoked when object interaction fails.")]
    public UnityEvent interactionFailTriggers;

    // Array of audioSources mainly used to keep a corrospondence between 
    // interactions sounds and audio sources.
    private List<AudioSource> audioSources;

    private void Awake()
    {
        audioSources = new List<AudioSource>();

        // Generate AudioSource components
        for (int i = 0; i < interactionSounds.Length; i++)
        {
            InteractionAudio IAudio = interactionSounds[i];

            if(IAudio.audioClip != null)
            {
                AudioSource audioSrc = this.gameObject.AddComponent<AudioSource>();
                audioSrc.clip = IAudio.audioClip;
                audioSrc.volume = IAudio.volume;
                audioSrc.loop = IAudio.loop;
                audioSources.Add(audioSrc);
            }
        }
    }

    // Called when this object is being interacted with.
    public bool OnInteract(bool meetsRequirements)
    {

        // Return if the object can't be interacted with
        if (!interactable)
        {
            return false;
        }

        if(meetsRequirements)
        {
            InteractSuccess();
            return true;
        }
        else
        {
            InteractFailure();
            return false;
        }

    }

    private void InteractFailure()
    {
        Debug.Log("Interaction Failure!");
        // Invoke specified interaction failure methods
        interactionFailTriggers.Invoke();
    }

    private void InteractSuccess()
    {
        Debug.Log("Interaction Success!");
        // Play all interaction sounds.
        if (interactionSounds.Length > 0)
        {
            for (int i = 0; i < audioSources.Count; i++)
            {
                audioSources[i].PlayDelayed(interactionSounds[i].delay);
            }
        }

        // Invoke specified interaction triggers
        interactionSuccessTriggers.Invoke();
    }

    public void SetInteractable(bool boolean)
    {
        interactable = boolean;
    }

    public bool GetInteractable()
    {
        return interactable;
    }
	
}
