using EDictionary.Core.Data;
using EDictionary.Core.Data.Factory;
using EDictionary.Core.Models;
using EDictionary.Core.Utilities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EDictionary.Core.DataLogic
{
	public class HistoryLogic
	{
		private HistoryAccess historyAccess;

		public HistoryLogic()
		{
			historyAccess = new HistoryAccess();
		}

		public void UpdateMaxHistory(int maxHistory)
		{
			//historyAccess.UpdateMaxHistory(maxHistory);
		}

		public void SaveHistory<T>(History<T> history)
		{
			history.Trim();

			historyAccess.SaveHistory(history);
		}

		public History<T> LoadHistory<T>()
		{
			var result = historyAccess.LoadHistory<T>();

			if (result.Status == Status.Success)
			{
				return result.Data;
			}

			return History<T>.Default;
		}

		public List<T> GetCollection<T>()
		{
			var result = historyAccess.LoadHistory<T>();

			if (result.Status == Status.Success)
			{
				return result.Data.Collection;
			}

			return new List<T>();
		}

		public async Task<History<T>> LoadHistoryAsync<T>()
		{
			return await Task.Run(() => this.LoadHistory<T>());
		}

		public async Task SaveHistoryAsync<T>(History<T> history)
		{
			await Task.Run(() => this.SaveHistory<T>(history));
		}
	}
}
