using UnityEngine;

///<summary>
/// Utility methods that are no part of the Mathf libraries in Unity or C#.
///</summary>
public static class MathfExtensions
{
  public static float ClampAngle(float angle, float min, float max)
  {
    if (min < 0f && max > 0f && (angle > max || angle < min))
    {
      angle -= 360f;
      if (angle > max || angle < min)
      {
        return Mathf.Abs(Mathf.DeltaAngle(angle, min)) < Mathf.Abs(Mathf.DeltaAngle(angle, max)) ? min : max;
      }
    }
    else if (min > 0 && (angle > max || angle < min))
    {
      angle += 360f;
      if (angle > max || angle < min)
      {
        return Mathf.Abs(Mathf.DeltaAngle(angle, min)) < Mathf.Abs(Mathf.DeltaAngle(angle, max)) ? min : max;
      }
    }

    if (angle < min) return min;
    else if (angle > max) return max;
    else return angle;
  }
}
