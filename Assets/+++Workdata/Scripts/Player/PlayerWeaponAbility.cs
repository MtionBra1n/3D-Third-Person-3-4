using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerWeaponAbility : MonoBehaviour
{
    public enum WeaponState{ Unarmed, TwoHandSword, OneHandSword}
    public WeaponState weaponState;
    
    private PlayerControllerCharakterController playerController;
    private InputAction equipUnequipAction;

    public GameObject bigSwordBack;
    public GameObject bigSwordHand;

    
    private void Awake()
    {
        playerController = GetComponent<PlayerControllerCharakterController>();
    }

    private void OnEnable()
    {
        StartCoroutine(DelayReference());
    }

    private void OnDisable()
    {
        equipUnequipAction.performed -= WeaponEquipUnequip;
    }
    
    IEnumerator DelayReference()
    {
        yield return null;
        equipUnequipAction = playerController.inputActions.Player.WeaponDrawn;
        equipUnequipAction.performed += WeaponEquipUnequip;
    }

    public void WeaponEquipUnequip(InputAction.CallbackContext ctx)
    {
        switch (weaponState)
        {
            case WeaponState.Unarmed:
                //... which weapon?
                playerController.AnimationsWeaponEquip(1);
                break;
            
            case WeaponState.OneHandSword:

                break;
            
            case WeaponState.TwoHandSword:
                playerController.AnimationsWeaponUnequip(0);
                break;
        }
        

    }

    public void WeaponSwitch()
    {
        switch (weaponState)
        {
            case WeaponState.Unarmed:
                bigSwordBack.SetActive(false);
                bigSwordHand.SetActive(true);
                weaponState = WeaponState.TwoHandSword;

                break;
            
            case WeaponState.OneHandSword:

                break;
            
            case WeaponState.TwoHandSword:
                bigSwordBack.SetActive(true);
                bigSwordHand.SetActive(false);
                weaponState = WeaponState.Unarmed;

                break;
        }
    }

}
