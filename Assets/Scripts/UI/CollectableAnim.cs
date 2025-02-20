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
        public Ease moveEase = Ease.OutBack;


        private void Awake()
        {
            m_camera = Camera.main;
        }
        public void PlayAnim(int count, Vector2 worldPosition)
        {
            count = Mathf.Min(count, MaxItemsCount);

            Vector2 startPosition = m_camera.WorldToScreenPoint(worldPosition);
            Vector2 endPosition = transform.position;

            GameObject collectable = Instantiate(collectablePrefab, startPosition, Quaternion.identity, transform);

            collectable.transform.DOMove(endPosition, moveDuration)
                .SetEase(moveEase)
                .SetDelay(moveDelay)
                .OnComplete(() => Destroy(collectable))
                .Play();





        }

    }
}
