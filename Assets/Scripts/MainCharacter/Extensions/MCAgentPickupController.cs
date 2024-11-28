using System;
using Environment.Interactables;
using UnityEngine;
using MainCharacter;

namespace MainCharacter.Extensions
{
    public class MCAgentPickupController : MonoBehaviour
    {
        [SerializeField, Tooltip("The animation for the players picking up and placing.")]
        private Animator animationBehaviour;

        public GameObject HeldObject { get; private set; }

        [SerializeField, Tooltip("Raycast for detecting objects that can be picked up.")]
        private Data.SRaycast sPickupRayCast;

        [SerializeField, Tooltip("This is the main character link.  It should be the parent object.")]
        private MCAgent mCAgent;

        private GameObject lastHitObject { get; set; }

        /// <summary>
        /// Trigger the event when player attempts to pickup an object and pass whether they succeed at it.
        /// </summary>
        public event Action<bool, GameObject> PlayerPickUpAttempt;

        /// <summary>
        /// Triggers when the player attempts to place the small object.
        /// </summary>
        public event Action<bool, GameObject> PlayerPlaceAttempt;

        // This is a bool within the animator
        private const string animatorParameterObjectHeld = "ObjectHeld";
        // This is a trigger in the animator
        private const string animatorParameterFailedTask = "FailedTask";

        void Start()
        {
            PlayerPickUpAttempt += PlayPickupAnimation;
            PlayerPlaceAttempt += PlayPlaceAnimation;
        }

        void OnDestroy()
        {
            PlayerPickUpAttempt -= PlayPickupAnimation;
            PlayerPlaceAttempt -= PlayPlaceAnimation;
        }

        private void PlayPickupAnimation(bool pickedUp, GameObject passedObject)
        {
            if (!pickedUp)
            {
                animationBehaviour.SetTrigger(animatorParameterFailedTask);
            }
            animationBehaviour.SetBool(animatorParameterObjectHeld, pickedUp);
        }

        private void PlayPlaceAnimation(bool placed, GameObject passedObject)
        {
            if (!placed)
            {
                animationBehaviour.SetTrigger(animatorParameterFailedTask);
            }
            animationBehaviour.SetBool(animatorParameterObjectHeld, !placed);
        }

        /// <summary>
        /// Detects when an object is a small pickup and attempts to pick it up.
        /// </summary>
        /// <param name="mCAgent">The main character mCAgent.</param>
        // The logic found here might be useful for other things that pick up objects, but as no such entities exist yet I'll leave it here for now.
        public void PickupObject()
        {

            RaycastHit hitInfo;
            if (Data.SRaycast.CastRayUsingSRaycast(sPickupRayCast, mCAgent.transform.position, mCAgent.transform, out hitInfo))
            {

                if (hitInfo.transform.CompareTag(Data.Globals.StaticTagStrings.SMALL_BLOCK))
                {
                    SmallObjectPickupable smallObjectPickupable = hitInfo.transform.gameObject.GetComponent<SmallObjectPickupable>();
                    if (smallObjectPickupable is null)
                    {
                        Debug.LogErrorFormat("${0} does not contain the Small Object Pickupable script", hitInfo.transform.name);
                        return;
                    }
                    if (!smallObjectPickupable.CanPickupObject(gameObject))
                    {
                        PlayerPickUpAttempt(false, smallObjectPickupable.gameObject);
                        return;
                    }

                    smallObjectPickupable.PickupObject(gameObject);
                    HeldObject = smallObjectPickupable.gameObject;
                    HeldObject.transform.SetParent(transform, true);
                    PlayerPickUpAttempt(true, smallObjectPickupable.gameObject);
                    return;
                }
            }
        }

        /// <summary>
        /// Attempts to place the object that the main character is carrying.
        /// </summary>
        /// <param name="mCAgent">The main character in question.</param>
        public void PlaceSmallObject()
        {
            IPickupObject pickedUpObjectComponent = HeldObject.GetComponent<IPickupObject>();

            if (pickedUpObjectComponent is null)
            {
                return;
            }

            RaycastHit hitInfo;
            Data.SRaycast.CastRayUsingSRaycast(sPickupRayCast, mCAgent.transform.position, mCAgent.transform, out hitInfo);

            lastHitObject = hitInfo.transform.gameObject;

            Vector3 placeLocation = mCAgent.transform.forward + mCAgent.transform.position;
            if (pickedUpObjectComponent.CanPlaceObject(hitInfo.transform.gameObject, placeLocation))
            {
                pickedUpObjectComponent.PlaceObject(hitInfo.transform.gameObject, placeLocation);
                PlayerPlaceAttempt(true, HeldObject.gameObject);
                return;
            }

            PlayerPlaceAttempt(false, HeldObject.gameObject);
            return;
        }

        // Unity's animator events can't take a bool value.  0 for false and 1 for true.
        public void PickupObjectCompleted(int taskSucceed)
        {

        }

        // Unity's animator events can't take a bool value.  0 for false and 1 for true.
        public void PlacedObjectCompleted(int taskSucceed)
        {
            if (taskSucceed == 1)
            {
                HeldObject.GetComponent<IPickupObject>().PlaceObject(lastHitObject, mCAgent.transform.forward + mCAgent.transform.position);
                HeldObject.transform.SetParent(null);
                HeldObject = null;
            }
        }
    }
}

