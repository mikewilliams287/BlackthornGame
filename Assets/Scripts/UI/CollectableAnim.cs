using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Jobs;

namespace CollectableAnimProj.Scripts
{
    public class CollectableAnim : MonoBehaviour
    {
        [Min(1)]
        [SerializeField] private int MaxItemsCount = 30;
        [SerializeField] private GameObject collectablePrefab;
        private Camera m_camera;
        [SerializeField] private float moveDuration = 0.5f;
        [SerializeField] private float moveDelay = 0.02f;

        [SerializeField] private Transform destinationPoint;
        public Ease moveEase = Ease.OutBack;


        private void Awake()
        {
            m_camera = Camera.main; // Reference to the main camera in the scene
        }
        public void PlayAnim(int count, Vector2 worldPosition)
        {
            count = Mathf.Min(count, MaxItemsCount);

            Vector2 startPosition = m_camera.WorldToScreenPoint(worldPosition);
            Vector2 endPosition = destinationPoint.position; // 

            for (int i = 0; i < count; i++)
            {
                GameObject collectable = Instantiate(collectablePrefab, startPosition, Quaternion.identity, transform); // Spawn the prefab at the defined start position
                collectable.transform.SetAsFirstSibling();

                collectable.transform.DOMove(endPosition, moveDuration) // Move the prefab from its start position to the end position using the defined easing
                    .SetEase(moveEase)
                    .SetDelay(moveDelay * i)
                    .OnComplete(() => Destroy(collectable))
                    .OnComplete(() => Debug.Log("Collectable " + i + " destroyed"))
                    .Play();
                //Debug.Log("Collectable " + i + " destroyed");
            }





        }

    }
}
