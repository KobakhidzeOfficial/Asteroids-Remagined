using AsteroidsGame.Data;
using AsteroidsGame.Data.Types;
using AsteroidsGame.Enemies.Asteroid;

using UnityEngine;

using Zenject;

namespace AsteroidsGame.Installers
{
	public class AsteroidInstaller : Installer<AsteroidInstaller>
	{
		readonly AsteroidData _data;

		public AsteroidInstaller(AsteroidSize size,
			DiContainer container)
		{
			_data = container.ResolveId<AsteroidData>(size);
		}

		public override void InstallBindings()
		{
			Container.BindInstance(_data).AsSingle();
			Container.Bind<Transform>().FromComponentOnRoot().AsSingle();
			Container.BindInterfacesTo<AsteroidHealth>().AsSingle();
			Container.BindInterfacesTo<AsteroidMovement>().AsSingle();
		}
	}
}