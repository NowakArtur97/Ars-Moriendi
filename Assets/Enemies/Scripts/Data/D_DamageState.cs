using UnityEngine;

[CreateAssetMenu(fileName = "New Damage State Data", menuName = "Data/State Data/Damage State")]
public class D_DamageState : ScriptableObject
{
    public float damageHopSpeed = 9.0f;
    public float afterHopSpeed = 0.0f;
    public float timeBeforeNextDamage = 2.0f;
    public GameObject[] hitPartciles;
}
