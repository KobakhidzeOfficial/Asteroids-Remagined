using System;

using AsteroidsGame.Data;

using UnityEngine;

using Zenject;

using Random = UnityEngine.Random;

namespace AsteroidsGame.Enemies.Asteroid
{
	public class AsteroidMovement : IInitializable, ITickable
	{
		private Vector3 _direction = Vector3.zero;
		private float _speed = 0f;

		readonly Settings _settings;
		readonly Transform _transform;

		public AsteroidMovement(AsteroidData asteroidData,
			Transform transform)
		{
			_settings = asteroidData.MovementSettings;
			_transform = transform;
		}

		public void Initialize()
		{
			Setup();
		}

		public void Tick()
		{
			Move();
		}

		private void Setup()
		{
			_direction = Random.insideUnitCircle.normalized;
			_speed = Random.Range(_settings.MinSpeed, _settings.MaxSpeed);
		}

		private void Move()
		{
			_transform.position += _direction * _speed * Time.deltaTime;
		}

		[Serializable]
		public class Settings
		{
			public float MinSpeed;
			public float MaxSpeed;
		}
	}
}