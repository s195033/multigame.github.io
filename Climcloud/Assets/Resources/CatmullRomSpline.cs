using UnityEngine;

public static class CatmullromSpline
{
    public static float Interpolate(float p0, float p1, float p2, float p3, float t)
    {
        float a = -p0 + 3f * p1 - 3f * p2 + p3;
        float b = 2f * p0 - 5f * p1 + 4f * p2 - p3;
        float c = -p0 + p2;
        float d = 2f * p1;
        return 0.5f * (t * (t * (t * a + b) + c) + d);
    }

    public static Vector2 Interpolate(Vector2 p0, Vector2 p1, Vector2 p2, Vector2 p3, float t)
    {
        return new Vector2(
            Interpolate(p0.x, p1.x, p2.x, p3.x, t),
            Interpolate(p0.y, p1.y, p2.y, p3.y, t)
        );
    }

    public static Vector3 Interpolate(Vector3 p0, Vector3 p1, Vector3 p2, Vector3 p3, float t)
    {
        return new Vector3(
            Interpolate(p0.x, p1.x, p2.x, p3.x, t),
            Interpolate(p0.y, p1.y, p2.y, p3.y, t),
            Interpolate(p0.z, p1.z, p2.z, p3.z, t)
        );
    }
}
