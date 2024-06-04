using System;

using AsteroidsGame.Data;

using UnityEngine;

using Zenject;

using Random = UnityEngine.Random;

namespace AsteroidsGame.Enemies.Saucer
{
	public class SaucerMovement : IInitializable, ITickable
	{
		private float _timer = 0f;
		private Vector3 _direction = Vector3.zero;
		private float _speed = 0f;

		private bool CanChangeDir => _timer <= 0f;

		readonly Settings _settings;
		readonly Transform _transform;

		public SaucerMovement(SaucerData saucerData,
			Transform transform)
		{
			_settings = saucerData.MovementSettings;
			_transform = transform;
		}

		public void Initialize()
		{
			SetDirection();
		}

		public void Tick()
		{
			Move();
			CheckForDirectionChange();
		}

		private void Move()
		{
			_transform.position += _direction * _speed * Time.deltaTime;
		}

		private void SetDirection()
		{
			_timer = _settings.ChangeDirectionInterval;
			_direction = Random.insideUnitCircle.normalized;
			_speed = Random.Range(_settings.MinSpeed, _settings.MaxSpeed);
		}

		private void CheckForDirectionChange()
		{
			if (CanChangeDir)
				SetDirection();
			else
				_timer -= Time.deltaTime;
		}

		[Serializable]
		public class Settings
		{
			public float MinSpeed;
			public float MaxSpeed;
			public float ChangeDirectionInterval;
		}
	}
}