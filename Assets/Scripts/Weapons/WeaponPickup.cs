using UnityEngine;
using System.Collections.Generic;
using System.Collections;


public class WeaponPickup : MonoBehaviour
{
    public WeaponData weaponData;  // Assign the weapon data in the inspector

    private bool isColliding;
    
    void OnTriggerEnter(Collider other)
    {        
        if (other.CompareTag(Constants.Tags.player))  // Ensure the player has a "Player" tag
        {
            PlayerWeaponManager weaponManager = other.GetComponent<PlayerWeaponManager>();
            if (weaponManager != null)
            {
                // check to see if player already has the current gameobject, if it does just skip the pickup
                if (weaponManager.currentWeaponData != null && weaponManager.currentWeaponData.weaponType == weaponData.weaponType)
                {
                    return; // no need to pickup same weapon 
                }
                else
                {
                    weaponManager.PickupWeapon(weaponData, true);
                    Debug.Log("Destroying the weapon in environment = "+gameObject.ToString());
                    Destroy(gameObject);  // Destroy the weapon pickup object after pickup
                }
                
            }
        }

    }
}


