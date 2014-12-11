using System;
using System.Collections.Generic;

namespace VizuSharp.DataProviders
{
	public class DataSet<T> : DataProvider<T>
	{
		private ISet<T> _data;

		public override IEnumerable<T> Data {
			get {
				return _data;
			}
		}

		public DataSet ()
		{
			_data = new HashSet<T> ();
		}

		public void Add (T element)
		{
			_data.Add (element);
		}
	}
}

