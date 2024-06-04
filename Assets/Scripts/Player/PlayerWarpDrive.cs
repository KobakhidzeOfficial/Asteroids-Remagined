using System;

using AsteroidsGame.InputHandler;

using UnityEngine;

using Zenject;

namespace AsteroidsGame.PlayerShip
{
	public class PlayerWarpDrive : ITickable
	{
		private float _cooldownRemaining = 0f;

		private bool CanWarp => _cooldownRemaining <= 0f && !_playerVisibility.IsDisabled;

		private readonly Settings _settings;
		private readonly IInputManager _inputManager;
		private readonly IPlayerVisibility _playerVisibility;
		private readonly Camera _camera;
		private readonly Transform _playerTransform;

		public PlayerWarpDrive(Settings settings,
			IInputManager input,
			IPlayerVisibility playerVisibility,
			Camera camera,
			Transform playerTransform)
		{
			_settings = settings;
			_inputManager = input;
			_playerVisibility = playerVisibility;
			_camera = camera;
			_playerTransform = playerTransform;
		}

		public void Tick()
		{
			TryWarp();
			UpdateCooldown();
		}

		private void TryWarp()
		{
			if (_inputManager.GetWarpDriveButton())
			{
				if (CanWarp)
				{
					Warp();
					_cooldownRemaining = _settings.WarpCooldown;
				}
			}
		}

		private void Warp()
		{
			Vector3 _Target = _camera.ScreenToWorldPoint(_inputManager.GetAimingPosition());
			_Target.z = 0;

			_playerTransform.position = _Target;
		}

		private void UpdateCooldown()
		{
			if (!CanWarp)
			{
				_cooldownRemaining -= Time.deltaTime;
			}
		}

		[Serializable]
		public class Settings
		{
			public float WarpCooldown;
		}
	}
}