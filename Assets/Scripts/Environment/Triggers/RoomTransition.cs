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

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag(StaticTagStrings.PLAYER))
            {
                SceneLoader.loader.LoadScenes(transitionScenes.Select(scene => scene.name));
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

