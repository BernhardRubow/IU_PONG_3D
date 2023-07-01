using Assets.MainGame.Team.BR.Code.Enumerations;
using UnityEngine;

public static class ExtensionMethods
{
    public static AudioClip GetRandom(this AudioClip[] clips)
    {
        var index = Random.Range(0, clips.Length);
        return clips[index];
    }

    public static PlayerLocations WhichPlayer(this float x)
    {
        if (x < 0) return PlayerLocations.Left;
        return PlayerLocations.Right;
    }

    public static PlayerLocations WhichPlayer(this Transform t)
    {
        return t.position.x < 0 ? PlayerLocations.Left : PlayerLocations.Right;
    }
}