using System;

using AsteroidsGame.PlayerShip;

using UnityEngine;

using Zenject;

namespace AsteroidsGame.Installers
{
	public class PlayerInstaller : MonoInstaller
	{
		[SerializeField] private Settings _Settings;

		public override void InstallBindings()
		{
			Container.Bind<Transform>().FromComponentOnRoot().AsSingle();

			Container.BindInterfacesTo<PlayerHealth>().AsSingle();
			Container.BindInterfacesTo<PlayerMovement>().AsSingle().WithArguments(_Settings.RBody);
			Container.BindInterfacesTo<PlayerShoot>().AsSingle().WithArguments(_Settings.PlayerShootPoints);
			Container.BindInterfacesTo<PlayerVisibility>().AsSingle().WithArguments(_Settings.SpriteRenderer, _Settings.TrailVFXObj, _Settings.Collider);
			Container.BindInterfacesTo<PlayerWarpDrive>().AsSingle();
		}

		[Serializable]
		public class Settings
		{
			public Rigidbody2D RBody;
			public Collider2D Collider;
			public SpriteRenderer SpriteRenderer;
			public GameObject TrailVFXObj;
			public Transform[] PlayerShootPoints;
		}
	}
}