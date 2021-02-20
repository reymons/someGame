using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BezierCurvePointData : Transform
{
    public Vector3 Position { get; }
    public Quaternion Rotation { get; }

    public BezierCurvePointData(Vector3 position, Quaternion rotation)
    {
        Position = position;
        Rotation = rotation;
    }

    public Vector3 LocalToWorldPosition(Vector3 localSpacePosition)
    {
        return Position + Rotation * localSpacePosition;
    }

    public Vector3 LocalToWorldVector(Vector3 localSpacePosition)
    {
        return Rotation * localSpacePosition;
    }
}
