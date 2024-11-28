using System;
using System.Collections;
using System.Collections.Generic;
using Data;
using Environment.Interactables;
using UnityEditor.EditorTools;
using UnityEngine;
using UnityEngine.InputSystem;

namespace MainCharacter
{
    public class MCAgent : MonoBehaviour
    {
        // Used when referencing the player character
        public static MCAgent MCAGENT { get; private set; }

        [SerializeField, Tooltip("The speed that the player will move with no other modifiers.")]
        private float playerSpeed;

        [SerializeField, Tooltip("Drag the players character controller onto this field.")]
        private CharacterController characterController;


        [SerializeField, Tooltip("The data for the pushing raycast.")]
        private Data.SRaycast sPushRayCast;


        [SerializeField, Tooltip("The component that is able to handle picking up logic.")]
        private Extensions.MCAgentPickupController pickupController;

        // This region only accepts inputs that the player will be using.  It will only pull from the global input system.
        // Set these on Start.
        #region Input Action
        // This action is directing the players movement.
        private InputAction movementAction { get; set; }

        // This is named as such so that it follows the documentation in the GDD.
        private InputAction actionAction { get; set; }
        #endregion




        void Start()
        {
            MCAGENT = this;
            movementAction = InputSystem.actions.FindAction("Move");
            actionAction = InputSystem.actions.FindAction("Action");
        }

        void Update()
        {
            // Collect the move data here
            Vector2 characterCompleteMoveV2 = PlayerMovement();
            Vector3 characterCompleteMoveV3 = new Vector3(characterCompleteMoveV2.x, 0, characterCompleteMoveV2.y);

            // Using the sqrMagnitude because it avoids using sqrt which is a costly calculation
            // Use this section to activate anything that needs the players movement to occur.
            // Does not include external movement.
            if (characterCompleteMoveV3.sqrMagnitude > 0.01f)
            {
                // This will be snappy.  Can update it later to be more smooth.
                // Look at calculation might not be needed either.
                transform.LookAt(transform.position + characterCompleteMoveV3);

                PushBlock();
            }


            // Code block for picking up and dropping small objects in the scene.


            characterController.Move(characterCompleteMoveV3);
        }

        /// <summary>
        /// Logic for pushing blocks.  Currently only handles small blocks.
        /// </summary>
        private void PushBlock()
        {
            RaycastHit hitInfo;
            if (Physics.Raycast(sPushRayCast.Origin(transform.position), sPushRayCast.Direction(transform), out hitInfo, sPushRayCast.MaxDistance, sPushRayCast.LayerMask))
            {
                if (hitInfo.transform.CompareTag(Data.Globals.StaticTagStrings.SMALL_BLOCK))
                {
                    // Pushing block logic goes here.  Call to the block and trigger the function when it gets programmed.
                    // hitInfo.collider.GetComponent()
                }
            }
        }

        /// <summary>
        /// Will return a vector2 that indicates the direction of the players movement based on user input.
        /// </summary>
        /// <returns>Vector2.  Positive indicates right and up. Negative is left and down.  Zero is no movement</returns>
        private Vector2 PlayerMoveDirection()
        {
            // Will only display the direction of the input.  Will not have modifiers used in final move.
            Vector2 outputDirection = movementAction.ReadValue<Vector2>();
            return outputDirection;
        }

        /// <summary>
        /// This takes into account any modifiers applied to the players move direction.
        /// </summary>
        /// <returns>Move with modifiers.  Includes the time delta as well.</returns>
        private Vector2 PlayerMovement()
        {
            // Set to zero in case the re assignment doesn't happen later.
            Vector2 outMove = Vector2.zero;

            outMove = PlayerMoveDirection() * playerSpeed;

            return outMove * Time.fixedDeltaTime;
        }
    }

}
