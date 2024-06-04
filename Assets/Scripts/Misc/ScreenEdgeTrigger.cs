using System.Collections;
using System.Collections.Generic;

using UnityEngine;

namespace AsteroidsGame
{
	public class ScreenEdgeTrigger : MonoBehaviour
	{
		private static readonly List<GameObject> _blacklistedObjs = new List<GameObject>();

		[SerializeField] private float _blacklistDuration = 0.2f;

		private Camera _camera;

		public void Setup(Camera camera)
		{
			_camera = camera;
		}

		private void OnTriggerExit2D(Collider2D other)
		{
			if (_blacklistedObjs.Contains(other.gameObject))
				return;

			WrapObject(other.gameObject);
		}

		void WrapObject(GameObject obj)
		{
			if (!this.gameObject.activeInHierarchy || _camera == null)
				return;

			if (obj != null)
			{
				Vector3 objPosition = obj.transform.position;
				Vector3 viewportPosition = _camera.WorldToViewportPoint(objPosition);

				if (IsOutsideViewport(viewportPosition))
				{
					_blacklistedObjs.Add(obj);
					StartCoroutine(UpdateBlacklist(obj));

					Vector3 newPosition = objPosition;

					if (viewportPosition.x > 1 || viewportPosition.x < 0)
					{
						newPosition.x = -newPosition.x;
					}

					if (viewportPosition.y > 1 || viewportPosition.y < 0)
					{
						newPosition.y = -newPosition.y;
					}

					obj.transform.position = newPosition;
				}
			}
		}

		bool IsOutsideViewport(Vector3 viewportPosition)
		{
			return viewportPosition.x > 1 || viewportPosition.x < 0 || viewportPosition.y > 1 || viewportPosition.y < 0;
		}

		IEnumerator UpdateBlacklist(GameObject obj)
		{
			yield return new WaitForSeconds(_blacklistDuration);
			_blacklistedObjs.Remove(obj);
		}
	}
}