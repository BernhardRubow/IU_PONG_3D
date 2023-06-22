using UnityEngine;

public static class ExtensionMethods
{
    public static AudioClip GetRandom(this AudioClip[] clips)
    {
        var index = Random.Range(0, clips.Length);
        return clips[index];
    }
}