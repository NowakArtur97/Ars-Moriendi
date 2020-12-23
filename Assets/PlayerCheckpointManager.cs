using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCheckpointManager : MonoBehaviour
{
    [SerializeField]
    private List<Transform> _checkpoints;
    [SerializeField]
    private GameObject _playerPrefab;

    private Transform _lastCheckpoint;

    private void Start()
    {
        _lastCheckpoint = _checkpoints[0];
        FindObjectOfType<Player>().StatsManager.DeathEvent += OnPlayerDeath;
    }

    private void OnPlayerDeath() => Instantiate(_playerPrefab, _lastCheckpoint.position, Quaternion.identity);
}
