using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshRenderer))]

[ExecuteInEditMode] // To Remove
public class BezierCurveMeshCreator : MonoBehaviour
{
    [SerializeField] private BezierCurve _bezierCurve;
    [SerializeField] private Mesh2D _meshToCreate;

    private Mesh _mesh;
    
    [SerializeField] private float _radius = 0.001f;

    void Awake()
    {
        _mesh = new Mesh() { name = "Wire segment" };
        GetComponent<MeshFilter>().sharedMesh = _mesh;

        transform.position = _bezierCurve.transform.position;
    }

    public void GenerateMeshSegment()
    {
        _mesh.Clear();

        // Vertices
        var vertices = new List<Vector3>();
        for (int i = 0; i < _bezierCurve.SegmentsCount; i++)
        {
            float t = i / (_bezierCurve.SegmentsCount - 1f);
            BezierCurvePointData data = _bezierCurve.DefinePointData(t);
            for (int j = 0; j < _meshToCreate.VertexCount; j++)
            {
                var meshNewPoint = data.LocalToWorldPosition(_meshToCreate.Vertices[j].Point * Mathf.Abs(_radius));
                vertices.Add(_bezierCurve.transform.InverseTransformPoint(meshNewPoint));
            } 
        }
        
        // Triangles
        var triIndices = new List<int>();
        for (int i = 0; i < _bezierCurve.SegmentsCount - 1; i++)
        {
            int rootIndex = i * _meshToCreate.VertexCount;
            int rootIndexNext = (i + 1) * _meshToCreate.VertexCount;
            for (int line = 0; line < _meshToCreate.LineCount; line += 2)
            {
                int lineIndexA = _meshToCreate.LineIndices[line];
                int lineIndexB = _meshToCreate.LineIndices[line + 1];

                int currentA = lineIndexA + rootIndex;
                int currentB = lineIndexB + rootIndex;

                int nextA = lineIndexA + rootIndexNext;
                int nextB = lineIndexB + rootIndexNext;

                triIndices.Add(currentA);
                triIndices.Add(nextA);
                triIndices.Add(nextB);

                triIndices.Add(currentA);
                triIndices.Add(nextB);
                triIndices.Add(currentB);
            }
        }

        _mesh.SetVertices(vertices);
        _mesh.SetTriangles(triIndices, 0);
        _mesh.RecalculateNormals();

        GetComponent<MeshCollider>().sharedMesh = _mesh;
    }

    private void Update()
    {
        GenerateMeshSegment();
    }
}
