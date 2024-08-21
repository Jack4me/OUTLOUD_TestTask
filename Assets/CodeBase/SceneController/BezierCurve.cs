using UnityEngine;

public class BezierCurve : MonoBehaviour
{
    public Transform point0; 
    public Transform point1; 
    public Transform point2; 
    public Transform point3; 
    public float duration = 5f; 
    private float time = 0f; 

    void Update()
    {
        if (time < 1)
        {
            time += Time.deltaTime / duration;
            transform.position = CalculateBezierPoint(time, point0.position, point1.position, point2.position, point3.position);
        }
    }

    Vector3 CalculateBezierPoint(float t, Vector3 p0, Vector3 p1, Vector3 p2, Vector3 p3)
    {
        float u = 1 - t;
        float tt = t * t;
        float uu = u * u;
        float uuu = uu * u;
        float ttt = tt * t;

        Vector3 p = uuu * p0; // Начальная точка
        p += 3 * uu * t * p1; // Первая контрольная точка
        p += 3 * u * tt * p2; // Вторая контрольная точка
        p += ttt * p3; // Конечная точка

        return p;
    }
}