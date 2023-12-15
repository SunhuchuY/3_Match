using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BallType
{
    blue = 0,
    green = 1,
    yellow = 2,
    orange = 3,
}

public class BallSpriteOfType
{
    [SerializeField]
    private BallType _type;
    public BallType type => _type;

    [SerializeField]
    private Sprite _sprite;
    public Sprite sprite => _sprite;
}

public class BubbleManager : MonoBehaviour
{
    public static BubbleManager Instance;

    [SerializeField]
    private BallSpriteOfType[] _ballSprites = new BallSpriteOfType[4];


    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    public void LinkedListDestroy(Ball ball)
    {
        Ball currentBall = ball;
 
        while(currentBall.nextBall != null || currentBall.previousBall != null)
        {
            Destroy(currentBall.previousBall?.gameObject);
            Destroy(currentBall.nextBall?.gameObject);
        }
    }

    public Sprite GetBallSprite(BallType type)
    {
        foreach (var item in _ballSprites)
        {
            // 타입이 같지 않을 경우, 종료
            if (item.type != type)
                continue;

            // 타입이 같은 경우
            return item.sprite;
        }

        return null;
    }
}
