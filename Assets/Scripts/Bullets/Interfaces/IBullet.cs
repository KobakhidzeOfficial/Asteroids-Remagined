using AsteroidsGame.Data.Types;

using UnityEngine;

namespace AsteroidsGame.Bullets
{
	public interface IBullet
	{
		EnemyTypes OriginType { get; }

		void SetupBullet(Vector3 pos, Quaternion rot, float speed, float timeToLive, EnemyTypes originType);
		void OnHit();
	}
}