using UnityEngine;

namespace PlaneDestroyer
{
  [CreateAssetMenu(menuName = "WeaponSystem/Projectile", fileName = "Projectile")]
  public class ProjectileSO : ScriptableObject
  {
    [field: SerializeField] public Projectile Projectile { get; private set; }
    [field: SerializeField] public float Speed { get; private set; }
    [field: SerializeField] public int Damage { get; private set; }
  }
}