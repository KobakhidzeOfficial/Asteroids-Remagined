using System;

using AsteroidsGame.Bullets;
using AsteroidsGame.Data;
using AsteroidsGame.Data.Types;
using AsteroidsGame.Services;

using UnityEngine;

using Zenject;

using Random = UnityEngine.Random;

namespace AsteroidsGame.Enemies.Saucer
{
	public class SaucerShootBase : IInitializable, ITickable
	{
		private float _FireCooldownRemaining = 0f;
		private bool CanShoot => _FireCooldownRemaining <= 0f;

		readonly Settings _settings;
		readonly IObjectPooler<IBullet> _bulletPooler;
		readonly Bullet.Factory _bulletFactory;

		protected readonly Transform _transform;

		public SaucerShootBase(SaucerData saucerData,
			Transform transform,
			IObjectPooler<IBullet> bulletPooler,
			Bullet.Factory bulletFactory)
		{
			_settings = saucerData.ShootSettings;
			_transform = transform;
			_bulletPooler = bulletPooler;
			_bulletFactory = bulletFactory;
		}

		public void Initialize()
		{
			_FireCooldownRemaining = _settings.FireCooldown;
		}

		public void Tick()
		{
			CheckForShoot();
			UpdateCooldown();
		}

		private void CheckForShoot()
		{
			if (CanShoot)
			{
				Shoot();
				_FireCooldownRemaining = _settings.FireCooldown;
			}
		}

		private void UpdateCooldown()
		{
			if (_FireCooldownRemaining > 0f)
			{
				_FireCooldownRemaining -= Time.deltaTime;
			}
		}

		private void Shoot()
		{

			if (!_bulletPooler.GetObject(out IBullet bullet))
				bullet = _bulletFactory.Create();

			Vector3 direction = GetDir();
			Vector3 pos = direction * _settings.CircleRadius;
			pos += _transform.position;

			Quaternion rot = Quaternion.Euler(0f, 0f, GetRotationAngle(direction));

			bullet.SetupBullet(pos, rot, _settings.BulletSpeed, _settings.BulletLifetime, _settings.EnemyType);
		}

		internal virtual Vector3 GetDir()
		{
			return Random.insideUnitCircle.normalized;
		}

		private float GetRotationAngle(Vector2 direction)
		{
			float angle = Mathf.Atan2(direction.x, direction.y) * Mathf.Rad2Deg;
			return -angle;
		}

		[Serializable]
		public class Settings
		{
			public float CircleRadius;
			public float FireCooldown;
			public float BulletSpeed;
			public float BulletLifetime;
			public EnemyTypes EnemyType;
		}
	}
}