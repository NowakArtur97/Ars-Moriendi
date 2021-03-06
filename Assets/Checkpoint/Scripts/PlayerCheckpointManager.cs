﻿using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerCheckpointManager : MonoBehaviour
{
    private static PlayerCheckpointManager _instance;

    [SerializeField]
    private Transform startingPosition;

    public Vector2 LastCheckpoint { get; private set; }

    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(_instance);
            LastCheckpoint = startingPosition.position;
        }
        else
        {
            Destroy(this);
        }
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnLevelLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnLevelLoaded;
    }

    private void OnLevelLoaded(Scene scene, LoadSceneMode mode)
    {
        FindObjectOfType<Player>().StatsManager.DeathEvent += OnPlayerDeath;

        for (int i = 0; i < transform.childCount; i++)
        {
            PlayerCheckpoint checkpoint = transform.GetChild(i).GetComponent<PlayerCheckpoint>();
            checkpoint.CheckpointEvent += OnPlayerCheckpoint;
        }
    }

    // TODO: Move to some kind Game Session/Scene Manager
    private void OnPlayerDeath() => SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

    private void OnPlayerCheckpoint(Vector2 checkpointPosition) => LastCheckpoint = checkpointPosition;
}
