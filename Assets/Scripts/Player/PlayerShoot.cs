using System;

using AsteroidsGame.Bullets;
using AsteroidsGame.Data.Types;
using AsteroidsGame.InputHandler;
using AsteroidsGame.Services;

using UnityEngine;

using Zenject;

namespace AsteroidsGame.PlayerShip
{
	public class PlayerShoot : ITickable
	{
		private float _fireCooldownRemaining = 0f;
		private bool CanShoot => _fireCooldownRemaining <= 0f;

		private readonly Settings _settings;
		private readonly IInputManager _inputManager;
		private readonly IPlayerVisibility _playerVisibility;
		private readonly IObjectPooler<IBullet> _bulletPooler;
		private readonly Bullet.Factory _bulletFactory;
		private readonly Transform[] _firePoints;

		public PlayerShoot(Settings settings,
			IInputManager input,
			IPlayerVisibility playerVisibility,
			IObjectPooler<IBullet> pooler,
			Bullet.Factory bulletFactory,
			Transform[] firePoints)
		{
			_settings = settings;
			_inputManager = input;
			_playerVisibility = playerVisibility;
			_bulletPooler = pooler;
			_bulletFactory = bulletFactory;
			_firePoints = firePoints;
		}

		public void Tick()
		{
			CheckForShoot();
			UpdateCooldown();
		}

		private void CheckForShoot()
		{
			if (_playerVisibility.IsDisabled)
				return;

			if (CanShoot)
			{
				if (_inputManager.GetFireButton())
				{
					Shoot();

					_fireCooldownRemaining = _settings.FireCooldown;
				}
			}
		}

		private void Shoot()
		{
			foreach (Transform point in _firePoints)
			{

				if (!_bulletPooler.GetObject(out IBullet bullet))
					bullet = _bulletFactory.Create();

				bullet.SetupBullet(point.position, point.rotation, _settings.BulletSpeed, _settings.BulletLifetime, _settings.EnemyType);
			}
		}

		private void UpdateCooldown()
		{
			if (_fireCooldownRemaining > 0f)
			{
				_fireCooldownRemaining -= Time.deltaTime;
			}
		}

		[Serializable]
		public class Settings
		{
			public float FireCooldown;
			public float BulletSpeed;
			public float BulletLifetime;
			public EnemyTypes EnemyType;
		}
	}
}