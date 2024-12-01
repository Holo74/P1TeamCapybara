using UnityEngine;

[CreateAssetMenu(fileName = "NewWeapon", menuName = "Weapons/Weapon")]

public class WeaponData : ScriptableObject
{
    public string weaponName;
    public WeaponType weaponType;

    public float fireRate;

    public GameObject weaponPrefab; 
    public int damage; 

    public Sprite icon; // Optional: For UI representation

    public enum WeaponType { Axe, Knife, Sword, Bow }

}
