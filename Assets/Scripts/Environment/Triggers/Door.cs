using Data.Globals;
using MainCharacter;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Environment.Triggers
{
    public class Door : MonoBehaviour
    {
        [SerializeField, Tooltip("When the trigger is entered, the player will move here")]
        private Transform Destination;

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        /// <summary>
        /// When a player enters a door, they will be forced to the next room
        /// </summary>
        /// <param name="other"></param>
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag(StaticTagStrings.PLAYER))
            {
                other.GetComponent<MCAgent>().ForceMovement(Destination.position);
            }
        }
    }
}
