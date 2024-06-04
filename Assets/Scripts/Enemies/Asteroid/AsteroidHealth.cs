using System;

using AsteroidsGame.Bullets;
using AsteroidsGame.Data;
using AsteroidsGame.Data.Types;
using AsteroidsGame.Events;
using AsteroidsGame.PlayerShip;
using AsteroidsGame.Services;

using UnityEngine;

using Zenject;

namespace AsteroidsGame.Enemies.Asteroid
{
	public class AsteroidHealth : IEnemyHealth, IInitializable
	{
		readonly Settings _settings;
		readonly IEnemy _asteroid;
		readonly Transform _transform;
		readonly IEnemySpawner _spawner;
		readonly SignalBus _signalBus;
		readonly DiContainer _container;

		public AsteroidHealth(AsteroidData asteroidData,
			IEnemy asteroid,
			Transform transform,
			IEnemySpawner spawner,
			SignalBus signalBus,
			DiContainer container)
		{
			_settings = asteroidData.HealthSettings;
			_asteroid = asteroid;
			_transform = transform;
			_spawner = spawner;
			_signalBus = signalBus;
			_container = container;
		}

		public void Initialize()
		{
			_signalBus.Fire<AsteroidCreatedSignal>();
		}

		public void CheckForDamage(Collider2D col)
		{
			IBullet bullet = col.GetComponent<IBullet>();

			if (bullet != null)
			{
				OnDeath();
				bullet.OnHit();
			}
			else if (col.GetComponent<IPlayer>() != null)
			{
				OnDeath();
			}
		}

		private void OnDeath()
		{
			_signalBus.Fire(new AsteroidDiedSignal(_settings.ScoreReward));

			SpawnSmallerAsteroids();
			_asteroid.Destroy();
		}

		private void SpawnSmallerAsteroids()
		{
			AsteroidEnemy.Factory _AsteroidFactory = _container.ResolveId<AsteroidEnemy.Factory>(_settings.AsteroidSizeToSpawn);

			if (_AsteroidFactory != null)
			{
				_spawner.SpawnInsideCircle(_AsteroidFactory, _settings.AsteroidSizeToSpawn, _transform.position, _settings.AsteroidsToSpawnAmount, true);
			}
		}

		[Serializable]
		public class Settings
		{
			public AsteroidSize AsteroidSizeToSpawn;
			public int AsteroidsToSpawnAmount;
			public int ScoreReward;
		}
	}
}