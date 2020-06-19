using UnityEngine;

[System.Serializable]
public class BossAbility
{
    [Header("Activate objects")]
    public GameObject[] objectsToActivate;

    [Header("Activate animations")]
    public string animationNameToPlay;

    [Header("Animation stats")]
    public float abilityDuration;
    public float DurationUntilEnd;
}
