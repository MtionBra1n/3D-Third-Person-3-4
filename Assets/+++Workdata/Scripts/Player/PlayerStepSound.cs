using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class PlayerStepSound : MonoBehaviour
{
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private float raycastDistance = 1.5f;
    [SerializeField] private Vector3 raycastPosition;
    [SerializeField] private AudioSource audioSource;
    
    
    [Header("Walk Sounds")]
    [SerializeField] private AudioClip[] grassWalkStepSounds;
    [SerializeField] private AudioClip[] stoneWalkStepSounds;
    [SerializeField] private AudioClip[] defaultWalkStepSounds;

    [Header("Run Sounds")]
    [SerializeField] private AudioClip[] grassRunStepSounds;
    [SerializeField] private AudioClip[] stoneRunStepSounds;
    [SerializeField] private AudioClip[] defaultRunStepSounds;
    
    [Header("Jump Start Sounds")]
    [SerializeField] private AudioClip[] grassJumpStartSounds;
    [SerializeField] private AudioClip[] stoneJumpStartSounds;
    [SerializeField] private AudioClip[] defaultJumpStartSounds;
    
    [Header("Jump Land Sounds")]
    [SerializeField] private AudioClip[] grassJumpLandSounds;
    [SerializeField] private AudioClip[] stoneJumpLandSounds;
    [SerializeField] private AudioClip[] defaultJumpLandSounds;
    
    public void PlayWalkStepSound()
    {

        Ray ray = new Ray(transform.position + raycastPosition, Vector3.down);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, raycastDistance, groundLayer))
        {
            print("Play Sound");
            string groundTag = hit.collider.tag;

            switch (groundTag)
            {
                case "Grass":
                    PlaySound(grassWalkStepSounds);
                    break;
                
                case "Stone":
                    PlaySound(stoneWalkStepSounds);
                    break;
                
                default:
                    PlaySound(defaultWalkStepSounds); 
                    break;
            }
        }
    }

    public void PlayRunStepSound()
    {
        Ray ray = new Ray(transform.position + raycastPosition, Vector3.down);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, raycastDistance, groundLayer))
        {
            string groundTag = hit.collider.tag;

            switch (groundTag)
            {
                case "Grass":
                    PlaySound(grassRunStepSounds);
                    break;
                
                case "Stone":
                    PlaySound(stoneRunStepSounds);
                    break;
                
                default:
                    PlaySound(defaultRunStepSounds); 
                    break;
            }
        }
    }

    public void PlayJumpStartSound()
    {
        Ray ray = new Ray(transform.position + raycastPosition, Vector3.down);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, raycastDistance, groundLayer))
        {
            string groundTag = hit.collider.tag;

            switch (groundTag)
            {
                case "Grass":
                    PlaySound(grassJumpStartSounds);
                    break;
                
                case "Stone":
                    PlaySound(stoneJumpStartSounds);
                    break;
                
                default:
                    PlaySound(defaultJumpStartSounds); 
                    break;
            }
        }
    }
    
    public void PlayJumpLandSound()
    {
        Ray ray = new Ray(transform.position + raycastPosition, Vector3.down);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, raycastDistance, groundLayer))
        {
            string groundTag = hit.collider.tag;

            switch (groundTag)
            {
                case "Grass":
                    PlaySound(grassJumpLandSounds);
                    break;
                
                case "Stone":
                    PlaySound(stoneJumpLandSounds);
                    break;
                
                default:
                    PlaySound(defaultJumpLandSounds); 
                    break;
            }
        }
    }
    
    void PlaySound(AudioClip[] audioClips)
    {
        int index = Random.Range(0, audioClips.Length);
        
        audioSource.PlayOneShot(audioClips[index]);
    }
}
