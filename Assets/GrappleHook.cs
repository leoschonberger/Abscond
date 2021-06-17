using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Presets;
using UnityEngine;

public class GrappleHook : MonoBehaviour
{
    public DistanceJoint2D joint2D;
    public Camera mainCamera;
    public LineRenderer ropeRenderer;
    public Transform playerTransform;
    public CircleCollider2D collider2D;

    public float secondsPerLine = 1f;
    public float maxLineLength = 10;

    private float timer = 0;

    private Vector2 currentLine = Vector2.zero;

    private float cooldownLength = 0.5f;
    private bool cooldown = false;

    private bool isLatched = false;
    // Start is called before the first frame update
    void Start()
    {
        ropeRenderer.enabled = false;
        //ContactFilter.SetLayerMask(Physics2D.GetLayerCollisionMask(gameObject.layer));
    }

    // Update is called once per frame
    void Update()
    {
        if (ropeRenderer.enabled && !isLatched)
        {
            DrawLine();
            return;
        }
        if (Input.GetKeyDown(KeyCode.LeftControl) && !cooldown)
        {
            var angle = GetMouseAngle.MouseAngle(playerTransform);
            currentLine = new Vector2((float) (Math.Cos(angle * Mathf.Deg2Rad)) * maxLineLength,
                (float) (Math.Sin(angle * Mathf.Deg2Rad)) * maxLineLength);
            //joint2D.connectedAnchor = mousePosition;
            //joint2D.enabled = true;
            
            //ropeRenderer.SetPosition(0, playerTransform.position);
            //ropeRenderer.SetPosition(1, mousePosition);
            ropeRenderer.enabled = true;
            collider2D.enabled = true;
            //frameCount++;
        }

        if (isLatched)
        {
            ropeRenderer.SetPosition(0, playerTransform.position);
        }

    }

    private void DrawLine()
    {
        Vector2 playerPosition = playerTransform.position;
        ropeRenderer.SetPosition(0, playerPosition);
        timer += Time.deltaTime;
        var percentageComplete = (timer) / secondsPerLine;
        
        var endOfRopeLocation = playerPosition + currentLine * percentageComplete;
        ropeRenderer.SetPosition(1,endOfRopeLocation);

        gameObject.transform.position = endOfRopeLocation;
        
        if (percentageComplete >= 1)
        {
            EndRopeAnimation();
        }
    }

    private void EndRopeAnimation()
    {
        gameObject.transform.position = playerTransform.position;
        ropeRenderer.enabled = false;
        collider2D.enabled = false;
        timer = 0;
        currentLine = Vector2.zero;
        StartCoroutine(CoolDownTimer(cooldownLength));
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("hello");
        switch (other.gameObject.layer)
        {
            case 0: 
                EndRopeAnimation();
                break;
            case 10:
                if (other.gameObject.CompareTag("Player"))
                    break;
                LatchedOn(); //latchesOnToEnemy
                break;

        }
    }
    private void LatchedOn()
    {
        isLatched = true;
    }

    private IEnumerator CoolDownTimer(float seconds)
    {
        cooldown = true;
        yield return new WaitForSeconds(seconds);
        cooldown = false;
    }
}
