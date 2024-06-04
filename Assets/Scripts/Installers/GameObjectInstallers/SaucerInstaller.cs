using AsteroidsGame.Data;
using AsteroidsGame.Data.Types;
using AsteroidsGame.Enemies.Saucer;

using UnityEngine;

using Zenject;

namespace AsteroidsGame.Installers
{
	public class SaucerInstaller : Installer<SaucerInstaller>
	{
		readonly SaucerSize _size;
		readonly SaucerData _data;

		public SaucerInstaller(SaucerSize size,
			DiContainer container)
		{
			_size = size;
			_data = container.ResolveId<SaucerData>(size);
		}

		public override void InstallBindings()
		{
			Container.BindInstance(_data).AsSingle();
			Container.Bind<Transform>().FromComponentOnRoot().AsSingle();
			Container.BindInterfacesTo<SaucerHealth>().AsSingle();
			Container.BindInterfacesTo<SaucerMovement>().AsSingle();

			if (_size == SaucerSize.Big)
			{
				Container.BindInterfacesTo<SaucerBigShoot>().AsSingle();
			}
			else
				Container.BindInterfacesTo<SaucerSmallShoot>().AsSingle();
		}
	}
}