using UnityEngine;

public static class CustomRandom
{
    public static bool GetBoolean(float trueChanse)
    {
        return Random.Range(0f, 1f) < trueChanse;
    }
}
