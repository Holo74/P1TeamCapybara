using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerWeaponManager : MonoBehaviour
{
    public Transform weaponHolder;  // Assign an empty GameObject where weapons will be held

    public GameObject currentWeapon;


    public WeaponData currentWeaponData;


    // This method is called by the weapon as it allows it to pass weapondata as a reference to the Player
    // Once the player has the weapondata information it can do the job of creating a new instance 
    public void PickupWeapon(WeaponData newWeaponData, bool isDiffWeapon)
    {
        if (currentWeaponData != null)
        {
            if (isDiffWeapon)
            {
                // If a new weapon is being assigned then destroy the current weapon that player is holding
                Debug.Log("Destroying current weapon that player is holding: " + currentWeapon);
                Destroy(currentWeapon);
            }
        }

        Debug.Log("Creating an instance of the object in Pickup: " + newWeaponData.weaponName);
        // Instantiate the new weapon prefab and set it as a child of the weapon holder
        // and delete current weapon first
        
        currentWeapon = Instantiate(newWeaponData.weaponPrefab, weaponHolder.position, weaponHolder.rotation, weaponHolder);
        Debug.Log("New Weapon created: " + currentWeapon);
        currentWeaponData = newWeaponData;
    }

    // Example method to simulate attacking
    public void Attack()
    {
        if (currentWeaponData != null)
        {
            Debug.Log($"Attacking with {currentWeaponData.weaponName}, dealing {currentWeaponData.damage} damage.");
        }
    }
}

