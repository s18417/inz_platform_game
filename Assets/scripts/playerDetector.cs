using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerDetector : MonoBehaviour
{
    [field: SerializeField] public bool PlayerDetected { get; private set; }
    public Vector2 DirectionToTarget => target.transform.position - detectorOrigin.position;

    [Header("OverlapBox params")]
    [SerializeField]
    private Transform detectorOrigin;
    public Vector2 detectorSize = Vector2.one;
    public Vector2 detectorOriginOffset = Vector2.zero;

    public float detectionDelay = 0.3f;

    public LayerMask detectorLayerMask;

    [Header("Gizmo")] 
    public Color gizmoIdleColor = Color.green;
    public Color gizmoDetectedColor = Color.red;
    public bool showGizmos = true;

    private GameObject target;

    public bool flipped = false;
    public GameObject Target
    {
        get => target;
        private set
        {
            target = value;
            PlayerDetected = target != null;
        }
    }
    void Start()
    {
        StartCoroutine(DetectionCouroutine());
    }

    private void Update()
    {
        if (flipped  && detectorOrigin.rotation.y == 0)
        {
            detectorOriginOffset.x *= -1;
            flipped = false;
        } 
        if (flipped == false && detectorOrigin.rotation.y == 1)
        {
            flipped = true;
            detectorOriginOffset.x *= -1;
        }
    }

    IEnumerator DetectionCouroutine()
    {
        yield return new WaitForSeconds(detectionDelay);
        PerformDetection();
        StartCoroutine(DetectionCouroutine());
    }

    public void PerformDetection()
    {
       

        Collider2D collider = Physics2D.OverlapBox((Vector2) detectorOrigin.position + detectorOriginOffset,
            detectorSize, 0, detectorLayerMask);
        
        if (collider!=null)
        {
            Target = collider.gameObject;
        }
        else
        {
            Target = null;
            

        }
        
    }

    private void OnDrawGizmos()
    {
        if (showGizmos && detectorOrigin != null)
        {
            Gizmos.color = gizmoIdleColor;
            if (PlayerDetected)
                Gizmos.color = gizmoDetectedColor;
            Gizmos.DrawCube((Vector2)detectorOrigin.position+detectorOriginOffset,detectorSize);
        }
    }
}
