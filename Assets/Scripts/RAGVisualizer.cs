using UnityEngine;
using System.Collections.Generic;

public class RAGVisualizer : MonoBehaviour
{
    [Header("Cars (Processes)")]
    public Transform p1, p2, p3, p4, p5;

    [Header("Boxes (Resources)")]
    public Transform r1, r2, r3, r4;

    private Material amberMat;
    private Material redMat;

    void Start()
    {
        Shader lineShader = Shader.Find("Universal Render Pipeline/Unlit");
        if (lineShader == null) lineShader = Shader.Find("Sprites/Default");

        amberMat = new Material(lineShader);
        amberMat.color = new Color(1f, 0.75f, 0f); // Amber

        redMat = new Material(lineShader);
        redMat.color = Color.red;

        // Draw Request Arrows (Car -> Box) - Amber
        DrawArrow(p1, r2, amberMat);
        DrawArrow(p2, r3, amberMat);
        DrawArrow(p3, r4, amberMat);
        DrawArrow(p4, r1, amberMat);

        // Draw Allocation Arrows (Box -> Car) - Red
        DrawArrow(r1, p1, redMat);
        DrawArrow(r2, p2, redMat);
        DrawArrow(r3, p3, redMat);
        DrawArrow(r4, p4, redMat);
    }

    void DrawArrow(Transform start, Transform end, Material mat)
    {
        GameObject lineObj = new GameObject("Arrow");
        lineObj.transform.SetParent(this.transform);
        LineRenderer lr = lineObj.AddComponent<LineRenderer>();

        lr.material = mat;
        lr.startWidth = 0.015f;
        lr.endWidth = 0.015f;
        lr.positionCount = 2;

        Vector3 startPos = start.position;
        Vector3 endPos = end.position;

        // If it's the red allocation arrow, we offset it so it's visible
        if (mat == redMat)
        {
            // Push the red arrow outwards and higher so it doesn't hide inside the car
            startPos = startPos + new Vector3(0.3f, 0.4f, 0.3f);
            endPos = endPos + new Vector3(0, 0.4f, 0);
        }
        else
        {
            // Keep yellow arrows floating slightly above the ground
            startPos += Vector3.up * 0.3f;
            endPos += Vector3.up * 0.3f;
        }

        lr.SetPosition(0, startPos);
        lr.SetPosition(1, endPos);
    }
}