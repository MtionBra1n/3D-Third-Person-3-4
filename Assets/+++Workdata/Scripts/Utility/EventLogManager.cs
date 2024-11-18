using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EventLogManager : MonoBehaviour
{
    /*
     * VARIABLES
     * 
     * TextMeshPro
     * (GameObject für den Text)
     * Animator / DoTweenAnim
     * Zeit fürs ausblenden
     *
     * ________
     * LOGIC
     *
     * SetEventLog Method
     * - Text einblenden und setzen / Event anzeigen
     * - Text ausblenden 
     * 
     * ________
     *
     * Wir interagieren mit einem Objekt und wenn ein Event Log ausgegeben werden soll,
     * soll A) der Text gesetzt werden, und B) eingeblendet werden. Nach einer Zeit X wird der Text wieder ausgeblendet.
     *
     * 
     */
    private Animator anim;
    private TextMeshProUGUI eventLog_Text;

    public float visibleTime = 2f;
    private Coroutine _coroutine;
    
    private void Awake()
    {
        anim = GetComponent<Animator>();
        eventLog_Text = GetComponentInChildren<TextMeshProUGUI>();
    }

    public void SetEventLogText(string textValue)
    {
        if (_coroutine == null)
        {
            if (!anim.enabled)
                anim.enabled = true;

            eventLog_Text.SetText(textValue);

            _coroutine = StartCoroutine(InitiateEventLog());
        }
    }

    IEnumerator InitiateEventLog()
    {
        anim.Play("FadeIn");
        yield return new WaitForSeconds(visibleTime);
        anim.Play("FadeOut");
        yield return new WaitForSeconds(.5f);
        _coroutine = null;
    }

}
