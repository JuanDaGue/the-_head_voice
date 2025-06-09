[System.Serializable]
public class WeaponStats
{
    public float damage;
    public float fireRate;
    public float projectileSpeed;
    public int clipSize;

    public WeaponStats(float dmg, float rate, float speed, int size)
    {
        damage = dmg;
        fireRate = rate;
        projectileSpeed = speed;
        clipSize = size;
    }

    public WeaponStats Clone()
    {
        return new WeaponStats(damage, fireRate, projectileSpeed, clipSize);
    }
}