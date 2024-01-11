using UnityEngine;

public static class MathUtils
{
    public static Vector2 AverageVector2(Vector2 vector1, Vector2 vector2)
    {
        return (vector1 + vector2) / 2;
    }
        
    public static Vector2 RandomPositionInHalfCircle(Vector2 center, float radius)
    {
        Vector2 randomPoint = Random.insideUnitCircle * radius;
        return center + new Vector2(randomPoint.x, Mathf.Abs(randomPoint.y));
    }
}