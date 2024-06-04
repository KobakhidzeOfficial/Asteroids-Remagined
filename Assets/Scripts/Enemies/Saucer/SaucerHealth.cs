using System;

using AsteroidsGame.Bullets;
using AsteroidsGame.Data;
using AsteroidsGame.Data.Types;
using AsteroidsGame.Events;
using AsteroidsGame.PlayerShip;

using UnityEngine;

using Zenject;

namespace AsteroidsGame.Enemies.Saucer
{
	public class SaucerHealth : IEnemyHealth, IInitializable
	{
		readonly Settings _settings;
		readonly IEnemy _saucer;
		readonly SignalBus _signalBus;

		public SaucerHealth(SaucerData saucerData,
			IEnemy saucer,
			SignalBus signalBus)
		{
			_settings = saucerData.HealthSettings;
			_saucer = saucer;
			_signalBus = signalBus;
		}

		public void Initialize()
		{
			_signalBus.Fire<SaucerCreatedSignal>();
		}

		public void CheckForDamage(Collider2D col)
		{
			IBullet bullet = col.GetComponent<IBullet>();

			if (bullet != null)
			{
				if (bullet.OriginType != EnemyTypes.Saucer)
				{
					OnDeath();
					bullet.OnHit();
				}
			}
			else if (col.GetComponent<IPlayer>() != null)
			{
				OnDeath();
			}
		}

		private void OnDeath()
		{
			_signalBus.Fire(new SaucerDiedSignal(_settings.ScoreReward));
			_saucer.Destroy();
		}

		[Serializable]
		public class Settings
		{
			public int ScoreReward;
		}
	}
}