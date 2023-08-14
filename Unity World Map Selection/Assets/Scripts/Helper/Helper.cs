using System.Collections.Generic;
using UnityEngine;

public static class Helper
{
    public static Vector3 CalculateDifference(Transform start, Transform end)
    {
        return (start.position - end.position);
    }

    public static Vector3 CalculateDifferenceNormalized(Transform start, Transform end)
    {
        return (start.position - end.position).normalized;
    }

    /// <summary>
    /// Destroy all child objects of this transform (Unintentionally evil sounding).
    /// Use it like so:
    /// <code>
    /// transform.DestroyChildren();
    /// </code>
    /// </summary>
    public static void DestroyChildren(this Transform t)
    {
        foreach (Transform child in t) Object.Destroy(child.gameObject);
    }

    public static float NormalizeSameAngle(float value)
    {
        return Mathf.RoundToInt(Mathf.Repeat(value + 180, 360) - 180);
    }

    /**
     * Returns a number expressed as euler angles with +-180 notation:
     * 
     * 500 -> 140
     * 450 -> 90
     * 270 -> -90
     * 0 -> 0
     * 180 -> 180
     * -180 -> 180
     */
    public static float ToEulerAngles(float number)
    {
        // POSITIVE NUMBERS
        if (number >= 0)
        {
            // [0, 180] -> Return number
            if (number >= 0 && number <= 180)
            {
                return number;
            }

            // (180, 360] -> Return number - 360
            else if (number > 180 && number <= 360)
            {
                return number - 360;
            }

            // (360, inf) -> Recursive calculation with number % 360
            else
            { /* if (number > 360) */
                return ToEulerAngles(number % 360);
            }
        }

        // NEGATIVE NUMBERS
        else
        {
            // (0, -180) -> Return number
            if (number < 0 && number >= -180)
            {
                return number;
            }

            // -180 -> 180
            else if (number == -180)
            {
                return 180;
            }

            // (180, 360] -> Return number + 360
            else if (number < -180 && number >= -360)
            {
                return number + 360;
            }

            // (-inf, -360) -> Recursive calculation with number % 360
            else
            { /* if (number < 360) */
                return ToEulerAngles(number % 360);
            }
        }
    }

    public static Vector3[] MakeSmoothCurve(Vector3[] arrayToCurve, float smoothness)
    {
        List<Vector3> points;
        List<Vector3> curvedPoints;
        int pointsLength = 0;
        int curvedLength = 0;

        if (smoothness < 1.0f) smoothness = 1.0f;

        pointsLength = arrayToCurve.Length;

        curvedLength = (pointsLength * Mathf.RoundToInt(smoothness)) - 1;
        curvedPoints = new List<Vector3>(curvedLength);

        float t = 0.0f;
        for (int pointInTimeOnCurve = 0; pointInTimeOnCurve < curvedLength + 1; pointInTimeOnCurve++)
        {
            t = Mathf.InverseLerp(0, curvedLength, pointInTimeOnCurve);

            points = new List<Vector3>(arrayToCurve);

            for (int j = pointsLength - 1; j > 0; j--)
            {
                for (int i = 0; i < j; i++)
                {
                    points[i] = (1 - t) * points[i] + t * points[i + 1];
                }
            }

            curvedPoints.Add(points[0]);
        }
        return (curvedPoints.ToArray());
    }

    public static Transform GetChildTransformByName(Transform parent, string targetName)
    {
        foreach (Transform child in parent)
        {
            if (child.name == targetName)
                return child;

            foreach (Transform grandchild in child)
            {

                if (grandchild.name == targetName)
                    return grandchild;
            }
        }
        Debug.LogWarning($"No {targetName} 'TRANSFORM' with this name was found in {parent.name}");
        return null;
    }
}
