using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationBehaviour : MonoBehaviour
{
    private PlayerControllerCharakterController playerController;

    private void Awake()
    {
        playerController = GetComponentInParent<PlayerControllerCharakterController>();
    }

    public void JumpEnd()
    {
        playerController.JumpEnd();
    }

    public void EndAction()
    {
        playerController.EndAction();
    }
}
