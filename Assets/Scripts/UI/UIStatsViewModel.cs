using AsteroidsGame.PlayerShip;
using AsteroidsGame.Services;

using Zenject;

namespace AsteroidsGame.UI
{
	public class UIStatsViewModel : IFixedTickable
	{
		private readonly UIStatsView _view;
		private readonly IScore _score;
		private readonly IPlayer _player;

		public UIStatsViewModel(UIStatsView view,
			IScore score,
			IPlayer player)
		{
			_view = view;
			_score = score;
			_player = player;
		}

		public void FixedTick()
		{
			UpdateHealth();
			UpdateScore();
		}

		private void UpdateScore()
		{
			_view.m_TxtScoreCounter.text = _score.GetCurrentScore.ToString();
		}

		private void UpdateHealth()
		{
			_view.m_TxtHealthCounter.text = _player.Health.ToString();
		}
	}
}