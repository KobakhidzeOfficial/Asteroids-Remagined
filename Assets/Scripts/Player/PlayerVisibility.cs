using System;
using System.Collections;

using AsteroidsGame.Services;

using UnityEngine;

using Zenject;

namespace AsteroidsGame.PlayerShip
{
	public class PlayerVisibility : IPlayerVisibility, IInitializable
	{
		public bool IsDisabled { get; private set; }

		private Vector3 _InitialPosition;

		readonly Settings _Settings;
		readonly CoroutineRunner _CoroutineRunner;
		readonly Transform _PlayerTransform;
		readonly SpriteRenderer _SpriteRenderer;
		readonly GameObject _TrailVFXObj;
		readonly Collider2D _Collider;

		public PlayerVisibility(Settings settings,
			CoroutineRunner coroutineRunner,
			Transform playerTransform,
			SpriteRenderer spriteRenderer,
			GameObject trailVFXObj,
			Collider2D collider)
		{
			_Settings = settings;
			_CoroutineRunner = coroutineRunner;
			_PlayerTransform = playerTransform;
			_SpriteRenderer = spriteRenderer;
			_TrailVFXObj = trailVFXObj;
			_Collider = collider;
		}

		public void Initialize()
		{
			_InitialPosition = _PlayerTransform.position;
			IsDisabled = false;
		}

		public void OnSoftDeath()
		{
			if (IsDisabled)
			{
				return;
			}

			IsDisabled = true;
			SetPlayerVisibility(false);

			_CoroutineRunner.StartCoroutine(IE_TryRespawn());
		}

		public void OnDeath()
		{
			IsDisabled = true;
			SetPlayerVisibility(false);
		}

		IEnumerator IE_TryRespawn()
		{
			_PlayerTransform.position = _InitialPosition;

			yield return new WaitForSeconds(_Settings.RespawnDelay);

			while (Physics2D.OverlapCircle(_PlayerTransform.position, _Settings.RespawnSafetyDistance))
			{
				yield return new WaitForSeconds(_Settings.RespawnSafetyCheckDelay);
			}

			IsDisabled = false;

			SetPlayerVisibility(true);
		}

		private void SetPlayerVisibility(bool status)
		{
			_SpriteRenderer.enabled = status;
			_TrailVFXObj.SetActive(status);
			_Collider.enabled = status;
		}

		[Serializable]
		public class Settings
		{
			public float RespawnDelay;
			public float RespawnSafetyCheckDelay;
			public float RespawnSafetyDistance;
		}
	}
}