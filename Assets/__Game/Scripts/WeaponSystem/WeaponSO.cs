using UnityEngine;

namespace PlaneDestroyer
{
  [CreateAssetMenu(menuName = "WeaponSystem/Weapon", fileName = "Weapon")]
  public class WeaponSO : ScriptableObject
  {
    [field: SerializeField] public ProjectileSO Projectile { get; private set; }
    [field: SerializeField] public ParticleHandler MuzzleFlare { get; private set; }
    [field: SerializeField] public float FireRate { get; private set; }
  }
}