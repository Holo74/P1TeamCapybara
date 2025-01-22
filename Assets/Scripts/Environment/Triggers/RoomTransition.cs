using Cinemachine;
using Data.Globals;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Environment.Triggers
{
    public class RoomTransition : MonoBehaviour
    {
        [SerializeField, Tooltip("The scenes which will be loaded")]
        private Object[] transitionScenes;

        [SerializeField, Tooltip("Virtual camera to use in this room")]
        private CinemachineVirtualCamera roomCamera;

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag(StaticTagStrings.PLAYER))
            {
                roomCamera.enabled = true;
                roomCamera.Follow = other.transform;

                SceneLoader.loader.LoadScenes(transitionScenes.Select(scene => scene.name));
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag(StaticTagStrings.PLAYER))
            {
                roomCamera.enabled = false;
                roomCamera.Follow = null;
            }
        }

        // Start is called before the first frame update
        void Start()
        {
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}

