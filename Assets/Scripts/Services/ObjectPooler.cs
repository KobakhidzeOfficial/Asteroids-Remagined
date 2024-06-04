using System.Collections.Generic;

namespace AsteroidsGame.Services
{
	public class ObjectPooler<T> : IObjectPooler<T>
	{
		private List<T> _AvailableObjects = new List<T>();

		public bool IsAvailable => _AvailableObjects.Count > 0;

		public bool GetObject(out T obj)
		{
			obj = default;

			if (IsAvailable)
			{
				obj = _AvailableObjects[0];
				RemoveObjectFromAvailable(_AvailableObjects[0]);
				return true;
			}

			return false;
		}

		public void AddObjectToAvailable(T obj)
		{
			_AvailableObjects.Add(obj);
		}

		private void RemoveObjectFromAvailable(T obj)
		{
			_AvailableObjects.Remove(obj);
		}
	}
}