using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelTeleport : MonoBehaviour
{
    // Start is called before the first frame update
    public Transform playerTransform;
    private Transform teleporter;
    public String nextLevel;
    void Start()
    {
        teleporter = GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        if (playerTransform.position.x > teleporter.position.x)
        {
            SceneManager.LoadScene(nextLevel);
        }
    }
}
