
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


namespace CollectableAnimProj.Scripts
{
    public class CollectItemAnimation : MonoBehaviour
    {
        public Transform startPosition;
        public CollectableAnim animationScript;
        //public Button button;
        public int numberOfCollectables;

        // Subscribe to the event when game object is enabled
        private void OnEnable()
        {
            Gem.OnGemCollected += AnimateCollectedItem;
        }

        //Unsubscribe when game object disabled
        private void OnDisable()
        {
            Gem.OnGemCollected -= AnimateCollectedItem;
        }


        private void AnimateCollectedItem()
        {
            if (animationScript != null)
            {
                if (startPosition != null)
                {
                    animationScript.PlayAnim(numberOfCollectables, startPosition.position);
                }
                else
                {
                    Debug.LogError("Start position object in not set");

                }
            }
            else
            {
                Debug.LogError("Animation script object is not set");
            }
        }

    }
}
//}

