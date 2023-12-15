using DG.Tweening;
using System;
using UnityEngine;

using Random = UnityEngine.Random;

public enum BubbleInstantiateType
{
    WideStair = 0,
    TwistBall = 1,

}

[System.Serializable]
public class BubbleInstantiateTypeOfPrefab
{
    [SerializeField]
    private GameObject _prefab;
    public GameObject prefab => _prefab;

    [SerializeField]
    private BubbleInstantiateType _type;
    public BubbleInstantiateType type => _type;
}

public class BubbleInstantiate : MonoBehaviour
{
    [SerializeField]
    private BubbleInstantiateTypeOfPrefab[] bubbleInstantiateTypeOfPrefabs = new BubbleInstantiateTypeOfPrefab[2];

    [SerializeField]
    private Transform _instantiateStartPosition, _instantiateEndPosition;

    [SerializeField]
    [Range(0,3)]
    private float _instantiateDuration = 2f;


    private void Start()
    {
        RandomInstantiate();
    }

    public void RandomInstantiate()
    {
        BubbleInstantiateType bubbleInstantiateRandomType = RandomBubbleInstantiateEnum();
        TargetInstantiate(bubbleInstantiateRandomType);
    }

    private void TargetInstantiate(BubbleInstantiateType targetType)
    {
        foreach (var item in bubbleInstantiateTypeOfPrefabs)
        {
            if (item.type == targetType)
            {
                GameObject prefab = Instantiate(item.prefab);
                InstantiateAnimation(prefab.transform);
                break;
            }
        }
    }

    private BubbleInstantiateType RandomBubbleInstantiateEnum()
    {
        var bubbleInstantiateValues = Enum.GetValues(typeof(BubbleInstantiateType));
        var randomIndex = Random.Range(0, bubbleInstantiateValues.Length);

        return (BubbleInstantiateType)bubbleInstantiateValues.GetValue(randomIndex);
    }

    private void InstantiateAnimation(Transform targetTransform)
    {
        targetTransform.position = _instantiateStartPosition.position;
        targetTransform.DOMove(_instantiateEndPosition.position, _instantiateDuration);
    }

}
