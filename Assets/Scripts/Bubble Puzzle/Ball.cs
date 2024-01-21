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
            // �� �ƴϸ�, ����
            if (!collider.CompareTag("Ball"))
                continue;

            // ��ũ�帮��Ʈ ��� ä���
            if(nextBall == null)
                nextBall = collider.GetComponent<Ball>();   
            else if(previousBall == null)
                previousBall = collider.GetComponent<Ball>();

        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // �÷��̾ �� �� �ƴҽ�, ����
        if (!collision.CompareTag("ShootBall"))
            return;

        // ��ũ�� �ִ� ������Ʈ�� �������
        BubbleManager.Instance.LinkedListDestroy(this);
        Destroy(gameObject);
    }

}
