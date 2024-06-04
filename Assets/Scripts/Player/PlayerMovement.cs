using System;

using AsteroidsGame.InputHandler;

using UnityEngine;

using Zenject;

namespace AsteroidsGame.PlayerShip
{
	public class PlayerMovement : ITickable, IFixedTickable
	{
		private readonly Settings _settings;
		private readonly IInputManager _inputManager;
		private readonly IPlayerVisibility _playerVisibility;
		private readonly Camera _camera;
		private readonly Rigidbody2D _rBody;
		private readonly Transform _playerTransform;

		public PlayerMovement(Settings settings,
			IInputManager input,
			IPlayerVisibility playerVisibility,
			Camera camera,
			Rigidbody2D rbody,
			Transform playerTransform)
		{
			_settings = settings;
			_inputManager = input;
			_playerVisibility = playerVisibility;
			_camera = camera;
			_rBody = rbody;
			_playerTransform = playerTransform;
		}

		public void Tick()
		{
			Rotate();
		}

		public void FixedTick()
		{
			Move();
			CapSpeed();
		}

		public void Rotate()
		{
			if (_playerVisibility.IsDisabled)
			{
				return;
			};

			Vector3 targetPos = _camera.ScreenToWorldPoint(_inputManager.GetAimingPosition());
			targetPos.z = 0f;

			if (Vector3.Distance(_playerTransform.position, targetPos) > _settings.RotationDeadzone)
			{
				_playerTransform.rotation = Quaternion.LookRotation(_playerTransform.forward, targetPos - _playerTransform.position);
			}
		}

		public void Move()
		{
			if (_playerVisibility.IsDisabled)
			{
				return;
			};

			if (_inputManager.GetThrottle() > _settings.ThrottleDeadzone)
			{
				_rBody.AddForce(_playerTransform.up * _settings.Acceleration * _inputManager.GetThrottle());
			}
			else
			{
				_rBody.velocity -= _rBody.velocity.normalized * _settings.Deceleration * Time.deltaTime;
			}
		}

		public void CapSpeed()
		{
			if (_playerVisibility.IsDisabled)
			{
				_rBody.velocity = Vector3.zero;
				return;
			}

			float sqrMaxSpeed = _settings.MaxSpeed * _settings.MaxSpeed;
			float sqrMinSpeed = _settings.MinSpeed * _settings.MinSpeed;

			if (_rBody.velocity.sqrMagnitude > sqrMaxSpeed)
			{
				_rBody.velocity = _rBody.velocity.normalized * _settings.MaxSpeed;
			}
			else if (_rBody.velocity.sqrMagnitude < sqrMinSpeed)
			{
				_rBody.velocity = _rBody.velocity.normalized * _settings.MinSpeed;
			}
		}

		public void ApplyMaxSpeed()
		{
			_rBody.velocity = _playerTransform.up * _settings.MaxSpeed;
		}

		[Serializable]
		public class Settings
		{
			public float MaxSpeed;
			public float MinSpeed;
			public float Acceleration;
			public float Deceleration;
			public float ThrottleDeadzone;
			public float RotationDeadzone;
		}
	}
}