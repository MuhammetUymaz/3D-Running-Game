using UnityEngine;

public class audioManager : MonoBehaviour
{
    public static audioManager instance;
    [Header("Audio Sources")]
    public AudioSource walkingAudioSource;
    [SerializeField] AudioSource gettingGoalAudioSource;
    [SerializeField] AudioSource jumpingAudioSource; 
    [SerializeField] AudioSource backgroundMusic;

    [Header("Settings")]
    [SerializeField] const float walkingPitchNormal = 0.75f, walkingPitchSprint = 1;

    void Awake()
    {
        //Singleton
        if(instance == null)
            instance = this;
    }
    public void PlayMovingSprintingSound(bool isGrounded)
    {
        //If the player is on the ground
        if(isGrounded)
            walkingAudioSource.UnPause();
    }

    public void ChangeWalkingAudioSourcePitch(bool normal)
    {
        if(normal) //Walking
            walkingAudioSource.pitch = walkingPitchNormal;

        else //Sprinting
            walkingAudioSource.pitch = walkingPitchSprint;
    }

    public void JumpingSoundPlay() => jumpingAudioSource.Play();

    public void GettingGoalSoundEffectPlay() => gettingGoalAudioSource.Play();
}
