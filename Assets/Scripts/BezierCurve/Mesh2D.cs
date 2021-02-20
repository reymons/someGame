using UnityEngine;

[CreateAssetMenu]
public class Mesh2D : ScriptableObject
{
    [System.Serializable]
    public class Vertex
    {
        public Vector3 Point;
        public Vector2 Normal;
        public int UV;
    }

    public int[] LineIndices;

    public Vertex[] Vertices;

    public int VertexCount => Vertices.Length;

    public int LineCount => LineIndices.Length;
}
