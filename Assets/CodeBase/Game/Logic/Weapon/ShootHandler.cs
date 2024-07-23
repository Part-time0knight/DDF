using Game.Logic.Misc;
using System.Collections.Generic;
using UnityEngine;

public class ShootHandler
{
    protected readonly Bullet.Pool _bulletPool;
    protected readonly Transform _weapon;

    protected List<Bullet> _bullets = new();

    public ShootHandler(Bullet.Pool bulletPool, Transform weapon)
    { 
        _bulletPool = bulletPool;
        _weapon = weapon;
    }

    public void Shoot(Vector2 target)
    {
        _bullets.Add(_bulletPool.Spawn(target));
    }
}
