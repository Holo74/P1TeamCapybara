using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Environment.Interactables
{
    public class BrokenPillar : MonoBehaviour
    {

        private bool canPush { get; set; }

        public void EnablePushing()
        {
            canPush = true;
        }
    }
}

