using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Environment.Interactables
{

    [RequireComponent(typeof(CharacterController))]
    public class AShoveable : MonoBehaviour
    {
        private CharacterController shoveableController { get; set; }

        [SerializeField, Tooltip("A higher resistance mod will make the object move slower compared to the player.")]
        [Range(.01f, 1000f)]
        private float ResistanceMod;

        void Start()
        {
            shoveableController = GetComponent<CharacterController>();
        }

        // This will be called in the Update when the player moves
        public virtual Vector3 Shoving(Vector3 direction)
        {
            Vector3 moveWithResistance = direction / ResistanceMod;
            shoveableController.Move(moveWithResistance * Time.deltaTime);
            return moveWithResistance;

        }
    }
}

