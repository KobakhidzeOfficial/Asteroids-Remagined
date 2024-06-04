using System;

using AsteroidsGame.Bullets;
using AsteroidsGame.Data.Types;
using AsteroidsGame.Enemies;
using AsteroidsGame.Events;

using UnityEngine;

using Zenject;

namespace AsteroidsGame.PlayerShip
{
	public class PlayerHealth : IPlayerHealth, IInitializable
	{
		public int CurrentHealth { get; private set; }

		private readonly Settings _settings;
		private readonly IPlayerVisibility _playerVisibility;
		private readonly SignalBus _signalBus;

		public PlayerHealth(Settings settings,
			IPlayerVisibility playerVisibility,
			SignalBus signalBus)
		{
			_settings = settings;
			_playerVisibility = playerVisibility;
			_signalBus = signalBus;
		}

		public void Initialize()
		{
			CurrentHealth = _settings.Health;
		}

		public void CheckForDamage(Collider2D col)
		{
			IBullet bullet = col.GetComponent<IBullet>();

			if (bullet != null)
			{
				if (bullet.OriginType != EnemyTypes.Player)
				{
					OnDamageReceived();
					bullet.OnHit();
				}
			}
			else if (col.GetComponent<IEnemy>() != null)
			{
				OnDamageReceived();
			}
		}

		private void OnDamageReceived()
		{
			if (_playerVisibility.IsDisabled)
				return;

			_signalBus.Fire<PlayerDiedSignal>();

			CurrentHealth -= 1;
			CheckForDeath();
		}

		private void CheckForDeath()
		{
			if (CurrentHealth <= 0)
				OnDeath();
			else
				_playerVisibility.OnSoftDeath();
		}

		private void OnDeath()
		{
			_signalBus.Fire<GameOverSignal>();
			_playerVisibility.OnDeath();
		}

		[Serializable]
		public class Settings
		{
			public int Health;
		}
	}
}