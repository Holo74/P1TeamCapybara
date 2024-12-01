using UnityEngine;
using System.Collections.Generic;
using System.Collections;


public class WeaponPickup : MonoBehaviour
{
    public WeaponData weaponData;  // Assign the weapon data in the inspector

    private bool isColliding;
    
    void OnTriggerEnter(Collider other)
    {        
        // if(isColliding) return;
        //     isColliding = true;

        if (other.CompareTag("Player"))  // Ensure the player has a "Player" tag
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
                    weaponManager.PickupWeapon(weaponData);
                    Debug.Log("Destroying the display weapon = "+gameObject.ToString());
                    Destroy(gameObject);  // Destroy the weapon pickup object after pickup
                }
                
            }
        }

       // StartCoroutine(Reset());  
    }


    // IEnumerator Reset()
    // {
    //     yield return new WaitForEndOfFrame();
    //     isColliding = false;
    // }

}


