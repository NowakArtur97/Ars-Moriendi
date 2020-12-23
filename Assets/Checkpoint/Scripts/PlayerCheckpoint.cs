using System;
using UnityEngine;

public class PlayerCheckpoint : MonoBehaviour
{
    public Action<Vector2> CheckpointEvent;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            CheckpointEvent?.Invoke(transform.position);
        }
    }
}
