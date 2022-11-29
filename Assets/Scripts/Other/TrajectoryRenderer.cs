using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrajectoryRenderer : MonoBehaviour
{
    [SerializeField] private LineRenderer lineRenderer;
    [SerializeField] private Player player;
    [SerializeField] private Transform originPoints;

    public void ShowTrajectory(Vector3 speed)
    {
        player.CanMove = false;
        player.Orientation = speed.x < 0;
        Vector3[] points = new Vector3[150];
        lineRenderer.positionCount = points.Length;
        Vector3 origin = originPoints.position;

        for (int i = 0; i < points.Length; i++)
        {
            float time = i * 0.01f;
            points[i] = origin + speed * time + Physics.gravity * time * time / 2f;

            if (points[i].y < -3)
            {
                lineRenderer.positionCount = i;
                break;
            }
        }
        lineRenderer.SetPositions(points);
    }

    public void StopShowTrajectory()
    {
        player.CanMove = true;
        lineRenderer.positionCount = 0;
    }
}
