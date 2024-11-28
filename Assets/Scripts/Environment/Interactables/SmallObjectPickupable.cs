using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Environment.Interactables
{
    /// <summary>
    /// This object will be carried over the players head with no powers needed.
    /// </summary>
    public class SmallObjectPickupable : MonoBehaviour, IPickupObject
    {
        // We just want the player to pick up the object for now.
        public bool CanPickupObject(GameObject agent)
        {
            return agent.CompareTag(Data.Globals.StaticTagStrings.PLAYER);
        }

        // We don't really need to do anything here
        public void PickupObject(GameObject agent)
        {

        }

        public bool CanPlaceObject(GameObject entity, Vector3 placedPosition)
        {
            if (entity is null)
            {
                return true;
            }
            return false;
        }

        public void PlaceObject(GameObject entity, Vector3 placedPosition)
        {

            transform.position = placedPosition;
        }
    }

}
