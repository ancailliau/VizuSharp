using System;
using System.Collections.Generic;
using System.Collections;

namespace VizuSharp.DataProviders
{
	public abstract class DataProvider<T> : IEnumerable<T>
	{
		public abstract IEnumerable<T> Data {
			get;
		}

		IEnumerator<T> IEnumerable<T>.GetEnumerator ()
		{
			return Data.GetEnumerator ();
		}

		IEnumerator IEnumerable.GetEnumerator ()
		{
			return Data.GetEnumerator ();
		}
	}
}

