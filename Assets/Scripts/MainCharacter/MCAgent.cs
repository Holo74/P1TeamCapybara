using System.Collections;
using System.Collections.Generic;
using UnityEditor.EditorTools;
using UnityEngine;

namespace MainCharacter
{
    public class MCAgent : MonoBehaviour
    {

        [SerializeField, Tooltip("The speed that the player will move with no other modifiers.")]
        private float playerSpeed;

        // Drag and drop the controller you want to use onto this
        [SerializeField, Tooltip("Drag the players character controller onto this field.")]
        private CharacterController characterController;

        void Update()
        {
            Vector2 characterCompleteMove = PlayerMovement();
            characterController.Move(new Vector3(characterCompleteMove.x, 0, characterCompleteMove.y));
        }

        /// <summary>
        /// Will return a vector2 that indicates the direction of the players movement based on user input.
        /// </summary>
        /// <returns>Vector2.  Positive indicates right and up. Negative is left and down.  Zero is no movement</returns>
        private Vector2 PlayerMoveDirection()
        {
            // Will only display the direction of the input.  Will not have modifiers used in final move.
            Vector2 outputDirection = Vector2.zero;
            outputDirection.x = Input.GetAxis("Horizontal");
            outputDirection.y = Input.GetAxis("Vertical");
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
