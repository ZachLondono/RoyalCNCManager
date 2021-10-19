using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoyalCNCTrackerLib.DAL {
	public interface IRepository<T> where T : BaseRepoClass {

		T Insert(T entity);

		void Remove(int id);

		void Update(T entity);

		T GetById(int id);

	}

	public class BaseRepoClass {
		public int Id { get; set; }
	}

}
