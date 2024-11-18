using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorBehaviour : MonoBehaviour
{
    /*
     * VARIABLES
     * Audio CLips - Mehrere Audio Sounds Öffnen, Schließen, Bewegung - Zufällig anspielen
     * Audio Source
     * Animation
     * Collider der Tür
     * Einen Bool für den aktuellen Zustand der Tür!
     *
     * LOGIC
     * Eine Methode für die Türsteuerung
     *
     * A) Ist die Tür offen?
     * - Erst die Animation und den Bewegungssound abspielen, dann das Schließgeräusch
     *
     * B) Ist die Tür geschlossen?
     * - Erst das Öffnengeräusch und dann die Aniamtion und den Bewegungssound
     *
     * METHODS
     * Door Toggle - mit einer Coroutine
     * Evtl. Zufälligen Sound abspielen
     */

    public AudioSource audioSource;

    public AudioClip doorOpenSound, doorCloseSound, doorMovementSound;

    public Animator[] anim;

    public bool isOpen;

    public float delayTimeOpen, delayTimeClose;

    public Collider[] _colliders;

    public void ToggleDoor()
    {
        if (isOpen)
        {
            // Close Door
            StartCoroutine(InitiateCloseDoor());
        }
        else
        {
            // Open Door
            StartCoroutine(InitiateOpenDoor());
        }

        isOpen = !isOpen;
    }

    IEnumerator InitiateOpenDoor()
    {
        if(doorOpenSound != null)
            audioSource.PlayOneShot(doorOpenSound);
        
        yield return new WaitForSeconds(delayTimeOpen);

        for (int i = 0; i < _colliders.Length; i++)
        {
            _colliders[i].enabled = false;
        }
        
        if(doorMovementSound != null)
            audioSource.PlayOneShot(doorMovementSound);

        if (anim[0].enabled == false)
        {
            for (int i = 0; i < anim.Length; i++)
            {
                anim[i].enabled = true;
            }
        }
        
        
        for (int i = 0; i < anim.Length; i++)
        {
            anim[i].Play("OpenDoor");
        }

        yield return new WaitForSeconds(delayTimeClose);
        
        for (int i = 0; i < _colliders.Length; i++)
        {
            _colliders[i].enabled = true;
        }
    }
    
    IEnumerator InitiateCloseDoor()
    {
        for (int i = 0; i < _colliders.Length; i++)
        {
            _colliders[i].enabled = false;
        }
        if(doorMovementSound != null)
            audioSource.PlayOneShot(doorMovementSound);
        
        
        if (anim[0].enabled == false)
        {
            for (int i = 0; i < anim.Length; i++)
            {
                anim[i].enabled = true;
            }
        }
        
        for (int i = 0; i < anim.Length; i++)
        {
            anim[i].Play("CloseDoor");
        }
        
        yield return new WaitForSeconds(delayTimeClose);
        if(doorCloseSound != null)
            audioSource.PlayOneShot(doorCloseSound);
        
        for (int i = 0; i < _colliders.Length; i++)
        {
            _colliders[i].enabled = true;
        }
        //animation + movementsound
        //warten
        //close Sound abspielen
    }
    
    
}
