using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System;

public class CollectableObject : MonoBehaviour, ICollectable
{
    public static event Action OnGemCollected;

    [Header("Collectable Animation Settings")]
    public GameObject collectableEffectPrefab; // The prefab to instantiate
    public int quantity = 1; // How many to spawn
    public float animationDuration = 1f; // Time it takes to reach the end position

    public float animationDelay = .2f;
    public Ease animationEaseType = Ease.InOutQuad; // The easing type

    public Transform spawnPoint;

    [Header("UI Target")]
    public RectTransform collectableDestinationPoint; // The UI element that the collectable should move to

    private void Start()
    {
        if (UIManager.Instance != null)
        {
            collectableDestinationPoint = UIManager.Instance.collectableDestinationPoint;
            Debug.Log("UI Manager found");
        }
        else
        {
            Debug.LogWarning("UIManager not found in the scene, check Canvas Object");
        }
    }

    public void Collect()
    {
        Debug.Log("GEM COLLECTED");

        OnGemCollected?.Invoke();
        SpawnAndAnimateCollectableEffect();

        Destroy(gameObject);
    }

    private void SpawnAndAnimateCollectableEffect()
    {
        if (collectableEffectPrefab == null || collectableDestinationPoint == null)
        {
            Debug.LogWarning("MISSING collectableEffectPrefab of destinationPoint");
            return;
        }

        for (int i = 0; i < quantity; i++)
        {
            GameObject effect = Instantiate(collectableEffectPrefab, transform.position, Quaternion.identity);
            Debug.Log("Collectable effect " + i + " spawned");

            Vector2 startPosition = spawnPoint.position;

            //Debug.Log(collectableDestinationPoint.position);
            //Debug.Log(Camera.main.ScreenToWorldPoint(collectableDestinationPoint.position));

            Vector2 endPosition = Camera.main.ScreenToWorldPoint(collectableDestinationPoint.position); //

            // float effectDelay = animationDelay * i;

            effect.transform.DOMove(endPosition, animationDuration)
                .SetEase(animationEaseType)
                .SetDelay(animationDelay * i)
                .OnComplete(() => OnEffectDestroy(effect));
        }
    }

    private void OnEffectDestroy(GameObject effect)
    {
        Destroy(effect);
        Debug.Log("game object destroyed");
    }
}
