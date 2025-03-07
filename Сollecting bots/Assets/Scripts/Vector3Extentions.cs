using UnityEngine;

public static class Vector3Extentions
{
    public static Vector3 Multiply(this Vector3 originalVector, Vector3 vectorMultiplyer)
    {
        float x = originalVector.x * vectorMultiplyer.x;
        float y = originalVector.y * vectorMultiplyer.y;
        float z = originalVector.z * vectorMultiplyer.z;
        return new Vector3(x, y, z);
    }
}
