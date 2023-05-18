using System;
using UnityEngine;
using Random = Unity.Mathematics.Random;

public class Knife : MonoBehaviour
{
    [SerializeField] private Rigidbody2D _rb;
    private Action _hit;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Stump"))
        {
            _hit.Invoke();
            transform.parent = other.transform;
            gameObject.tag = "KnifeInStump";
            Destroy(_rb);
            Destroy(this);
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("KnifeInStump"))
        {
            _rb.velocity = Vector2.zero;
            var col = gameObject.GetComponent<BoxCollider2D>();
            Destroy(col);
            _rb.gravityScale = 1;
            _rb.AddForce(Vector2.right * 5, ForceMode2D.Impulse);
            _rb.AddTorque(-5, ForceMode2D.Impulse);
        }
    }

    public void Setup(Action hit)
    {
        _hit = hit;
    }
}
