using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.IO.Pipes;
using UnityEngine;

public class BubbleShoot : MonoBehaviour
{
    [SerializeField]
    private LineRenderer lineRenderer;

    [SerializeField]
    private ShootBall _shootBallPrefab;

    [SerializeField]
    private Transform _firePosition;

    
    public void LineRenderSetActive(bool state)
    {
        lineRenderer.enabled = state;
    }

    public void DrawLine(Vector3 start, Vector3 direction)
    {
        Vector3 endPosition = start + direction.normalized * Screen.width * 1;

        lineRenderer.positionCount = 2;

        lineRenderer.SetPosition(0, start);
        lineRenderer.SetPosition(1, endPosition);

        lineRenderer.startWidth = 0.1f; 
        lineRenderer.endWidth = 0.1f;
    }

    public void InstantiateBall(Vector2 direction)
    {
        ShootBall shootBall = Instantiate(_shootBallPrefab, _firePosition.position, Quaternion.identity);
        shootBall.VelocityForceDirection(direction);
    }
}
