using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    [SerializeField]
    private BallType _type;
    public BallType type => _type;

    public Ball previousBall { get; private set; }
    public Ball nextBall { get; private set; }

    private float _circleRadius = 100f;


    private void Start()
    {
        LinkNode(transform.position, _circleRadius);
    }

    private void LinkNode(Vector3 center, float radius)
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(center, radius);

        Debug.Log(colliders.Length);

        foreach (var collider in colliders) 
        {
            Debug.Log(collider.tag);
            // 볼 아니면, 종료
            if (!collider.CompareTag("Ball"))
                continue;

            // 링크드리스트 노드 채우기
            if(nextBall == null)
                nextBall = collider.GetComponent<Ball>();   
            else if(previousBall == null)
                previousBall = collider.GetComponent<Ball>();

        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // 플레이어가 쏜 공 아닐시, 종료
        if (!collision.CompareTag("ShootBall"))
            return;

        // 링크가 있는 오브젝트들 모두제거
        BubbleManager.Instance.LinkedListDestroy(this);
        Destroy(gameObject);
    }

}
