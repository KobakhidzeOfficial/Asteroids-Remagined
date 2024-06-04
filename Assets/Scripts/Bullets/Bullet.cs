using AsteroidsGame.Data.Types;
using AsteroidsGame.Services;

using UnityEngine;

using Zenject;

namespace AsteroidsGame.Bullets
{
	public class Bullet : MonoBehaviour, IBullet
	{
		public EnemyTypes OriginType { get; private set; }

		private IObjectPooler<IBullet> _bulletPooler;
		private float _speed = 0.0f;

		[Inject]
		private void Construct(IObjectPooler<IBullet> pooler)
		{
			_bulletPooler = pooler;
		}

		private void Update()
		{
			MoveForward();
		}

		private void MoveForward()
		{
			transform.position += transform.up * _speed * Time.deltaTime;
		}

		public void SetupBullet(Vector3 pos, Quaternion rot, float speed, float timeToLive, EnemyTypes originType)
		{
			transform.SetPositionAndRotation(pos, rot);

			_speed = speed;
			OriginType = originType;

			gameObject.SetActive(true);

			Invoke(nameof(OnHit), timeToLive);
		}

		public void OnHit()
		{
			if (IsInvoking(nameof(OnHit)))
			{
				CancelInvoke(nameof(OnHit));
			}

			_bulletPooler.AddObjectToAvailable(this);

			this.gameObject.SetActive(false);
		}

		public class Factory : PlaceholderFactory<IBullet>
		{

		}
	}
}