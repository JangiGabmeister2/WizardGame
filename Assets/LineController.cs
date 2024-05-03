using System.Collections.Generic;
using UnityEngine;

public class LineController : MonoBehaviour
{
    public List<Transform> points = new List<Transform>();
    public Transform lastPoints;
    public int maxPointDrawn = 2;

    private LineRenderer _lr => GetComponent<LineRenderer>();

    private void Update()
    {
        if (Input.GetMouseButton(0))
        {
            Vector2 worldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(worldPoint, Vector2.zero);

            if (hit.collider != null)
            {
                MakeLine(hit.collider.transform);
            }
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            points.Clear();
            _lr.positionCount = 0;
            lastPoints = null;
        }
    }

    private void MakeLine(Transform newPoint)
    {
        if (lastPoints == null)
        {
            lastPoints = newPoint;
            points.Add(newPoint);
        }
        else
        {
            int c = 0;

            for (int i = 0; i < points.Count; i++)
            {
                if (newPoint == points[i])
                {
                    c++;
                }
            }

            if (c < maxPointDrawn)
            {
                print(newPoint.name);
                points.Add(newPoint);
                _lr.enabled = true;

                SetupLine();
            }
            else
            {
                print($"You cannot draw over a point more than {maxPointDrawn} times.");
            }
        }
    }

    private void SetupLine()
    {
        int pointlength = points.Count;

        _lr.positionCount = pointlength;

        for (int i = 0; i < pointlength; i++)
        {
            _lr.SetPosition(i, points[i].position);
        }
    }
}
