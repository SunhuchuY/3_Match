using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Device;

public class ShootBall : MonoBehaviour
{
    private const string _sideScreen = "SideScreen";
    private const string _topScreen = "TopScreen";

    [SerializeField]
    private float _ballSpeed = 5f;

    private Rigidbody2D _rigidyBody2D;


    private void Awake()
    {
        _rigidyBody2D = GetComponent<Rigidbody2D>();
        VelocityForceDirection(new Vector2(0.3f, 0.6f));
    }

    public void VelocityForceDirection(Vector2 direction)
    {
        direction.Normalize();
        _rigidyBody2D.velocity = direction * _ballSpeed;      
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag(_sideScreen))
        {
            Vector2 reflextionDirection;
            reflextionDirection.x = -1 * _rigidyBody2D.velocity.x;
            reflextionDirection.y = _rigidyBody2D.velocity.y;

            VelocityForceDirection(reflextionDirection); 
        }
        else if (collision.CompareTag(_topScreen))
        {
            Vector2 reflextionDirection;
            reflextionDirection.x = _rigidyBody2D.velocity.x;
            reflextionDirection.y = -1 * _rigidyBody2D.velocity.y;


            VelocityForceDirection(reflextionDirection);
        }
    }   
}
