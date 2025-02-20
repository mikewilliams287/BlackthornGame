using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace CollectableAnimProj.Scripts
{
    public class CollectItemButton : MonoBehaviour
    {
        public Transform WorldPoint;
        public CollectableAnim collectableAnim;
        public Button button;
        public int numberOfCollectables;

        private void Awake() =>
            button.onClick.AddListener(CollectItem);

        private void CollectItem()
        {
            collectableAnim.PlayAnim(numberOfCollectables, WorldPoint.position);
        }

    }
}