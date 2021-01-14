using UnityEngine;

[CreateAssetMenu(fileName = "_DissolveEffecteData", menuName = "Data/Effect Data/Dissolve")]
public class D_DissolveEffect : ScriptableObject
{
    public string propertyName = "_Fade";
    public float startValue = 1.0f;
    public float timeBeforeDissolving = 0.1f;
    public bool activeOnStart = true;
}
