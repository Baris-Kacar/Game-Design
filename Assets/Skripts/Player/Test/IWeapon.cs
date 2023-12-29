using UnityEngine;

public interface IWeapon
{
    
    public void Shoot(Animator animator, Transform firePoint, GameObject bulletPrefab);
    public void Reload(Animator animator);

}