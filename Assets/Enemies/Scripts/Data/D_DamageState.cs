using UnityEngine;

[CreateAssetMenu(fileName = "_DamageStateData", menuName = "Data/Enemy State Data/Damage State")]
public class D_DamageState : ScriptableObject
{
    public float damageHopSpeed = 2.0f;
    public float afterHopSpeed = 0.0f;
    public float timeBeforeNextDamage = 2.0f;
    public GameObject[] hitPartciles;
}
