using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace CollectableAnimProj.Scripts
{
    public class CollectItemButton : MonoBehaviour
    {
        public Transform startPosition;
        public CollectableAnim animationScript;
        public Button button;
        public int numberOfCollectables;

        private void Awake() =>
            button.onClick.AddListener(CollectItem);

        private void CollectItem()
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