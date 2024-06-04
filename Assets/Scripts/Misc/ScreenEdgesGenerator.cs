using System;

using UnityEngine;

using Zenject;

namespace AsteroidsGame
{
	public class ScreenEdgesGenerator : IInitializable
	{
		readonly Settings _settings;
		readonly Camera _camera;

		public ScreenEdgesGenerator(Settings settings,
			Camera camera)
		{
			_settings = settings;
			_camera = camera;
		}

		public void Initialize()
		{
			Vector2 screenSize = new Vector2(Screen.width, Screen.height);
			Vector2 screenToWorld = Camera.main.ScreenToWorldPoint(screenSize);

			CreateCollider(new Vector2(screenToWorld.x + _settings.DistanceFromViewport, 0), new Vector2(_settings.ColliderWidth, screenToWorld.y * _settings.SizeMultiplier));
			CreateCollider(new Vector2(-screenToWorld.x - _settings.DistanceFromViewport, 0), new Vector2(_settings.ColliderWidth, screenToWorld.y * _settings.SizeMultiplier));
			CreateCollider(new Vector2(0, screenToWorld.y + _settings.DistanceFromViewport), new Vector2(screenToWorld.x * _settings.SizeMultiplier, _settings.ColliderWidth));
			CreateCollider(new Vector2(0, -screenToWorld.y - _settings.DistanceFromViewport), new Vector2(screenToWorld.x * _settings.SizeMultiplier, _settings.ColliderWidth));
		}

		private void CreateCollider(Vector2 position, Vector2 size)
		{
			GameObject colliderObject = new GameObject("ScreenEdgeCollider")
			{
				layer = LayerMask.NameToLayer("Boundary")
			};
			colliderObject.transform.position = position;

			BoxCollider2D collider = colliderObject.AddComponent<BoxCollider2D>();
			collider.size = size;
			collider.isTrigger = true;

			colliderObject.AddComponent<ScreenEdgeTrigger>().Setup(_camera);

			Rigidbody2D rBody = colliderObject.AddComponent<Rigidbody2D>();
			rBody.isKinematic = true;
			rBody.collisionDetectionMode = CollisionDetectionMode2D.Continuous;
		}

		[Serializable]
		public class Settings
		{
			public float ColliderWidth;
			public float SizeMultiplier;
			public float DistanceFromViewport;
		}
	}
}