using UnityEngine;
using System.Collections.Generic;

public class RAGVisualizer : MonoBehaviour
{
    [Header("Cars (Processes)")]
    public Transform p1; // Zippy
    public Transform p2; // Taxi Tom
    public Transform p3; // Big Bus
    public Transform p4; // Suvi
    public Transform p5; // Ambulance

    [Header("Boxes (Resources)")]
    public Transform r1; // Box NW
    public Transform r2; // Box NE
    public Transform r3; // Box SE
    public Transform r4; // Box SW

    private Material amberMat;
    private Material redMat;

    void Start()
    {
        // Create materials for the lines
        amberMat = new Material(Shader.Find("Sprites/Default"));
        amberMat.color = new Color(1f, 0.75f, 0f); // Amber

        redMat = new Material(Shader.Find("Sprites/Default"));
        redMat.color = Color.red;

        // Draw the 4 Request Arrows (Car -> Box it wants next)
        DrawArrow(p1, r2, amberMat); // P1 wants R2
        DrawArrow(p2, r3, amberMat); // P2 wants R3
        DrawArrow(p3, r4, amberMat); // P3 wants R4
        DrawArrow(p4, r1, amberMat); // P4 wants R1

        // Draw the 4 Allocation Arrows (Box -> Car currently inside)
        DrawArrow(r1, p1, redMat); // R1 allocated to P1
        DrawArrow(r2, p2, redMat); // R2 allocated to P2
        DrawArrow(r3, p3, redMat); // R3 allocated to P3
        DrawArrow(r4, p4, redMat); // R4 allocated to P4
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

        // Raise the lines slightly so they float above the grid
        Vector3 startPos = start.position + Vector3.up * 0.3f;
        Vector3 endPos = end.position + Vector3.up * 0.3f;

        lr.SetPosition(0, startPos);
        lr.SetPosition(1, endPos);
    }
}