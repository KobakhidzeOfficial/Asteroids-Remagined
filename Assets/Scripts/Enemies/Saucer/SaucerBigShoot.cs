using AsteroidsGame.Bullets;
using AsteroidsGame.Data;
using AsteroidsGame.Services;

using UnityEngine;

namespace AsteroidsGame.Enemies.Saucer
{
	public class SaucerBigShoot : SaucerShootBase
	{
		public SaucerBigShoot(SaucerData settings,
			Transform transform,
			IObjectPooler<IBullet> bulletPooler,
			Bullet.Factory bulletFactory) : base(settings, transform, bulletPooler, bulletFactory)
		{

		}
	}
}