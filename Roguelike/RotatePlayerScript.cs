using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatePlayerScript : MonoBehaviour
{
    // Look at Mouse test 2, reference youtube: Unity Nuggets: How to "Look At" Mouse in 2D Game

    private void Update()
    {
        var dir = Input.mousePosition - Camera.main.WorldToScreenPoint(transform.position);
        var angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis((-angle), Vector3.up);          // - angle fixes in this iteration, had to rotate muzzlepoint and bullet scale accordingly
    }
}
