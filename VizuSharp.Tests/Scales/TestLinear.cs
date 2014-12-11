using NUnit.Framework;
using System;
using System.Linq;
using VizuSharp.Scales;
using VizuSharp.Maths;

namespace VizuSharp.Tests.Scales
{
	[TestFixture ()]
	public class TestLinear
	{
		[TestCase (0,0)]
		[TestCase (1,1)]
		[TestCase (.5,.5)]
		[TestCase (.3,.3)]
		[TestCase (.6,.6)]
		public void TestIdentity (double domainValue, double rangeValue)
		{
			var scale = new LinearScale ();
			Assert.AreEqual (rangeValue, scale.Scale (domainValue));
		}

		[TestCase (0,1)]
		[TestCase (-1,0)]
		[TestCase (-.5,.5)]
		[TestCase (-.3,.7)]
		[TestCase (-.6,.4)]
		public void TestTranslateIdentityDomain (double domainValue, double rangeValue)
		{
			var scale = new LinearScale (new Bounds (-1, 0));
			Assert.AreEqual (rangeValue, scale.Scale (domainValue));
		}

		[TestCase (0,0)]
		[TestCase (-1,1)]
		[TestCase (-.5,.5)]
		[TestCase (-.3,.3)]
		[TestCase (-.6,.6)]
		public void TestInverseIdentityDomain (double domainValue, double rangeValue)
		{
			var scale = new LinearScale (new Bounds (0, -1));
			Assert.AreEqual (rangeValue, scale.Scale (domainValue));
		}

		[TestCase (0,0)]
		[TestCase (10,1)]
		[TestCase (5,.5)]
		[TestCase (3,.3)]
		[TestCase (6,.6)]
		public void TestDomain (double domainValue, double rangeValue)
		{
			var scale = new LinearScale (new Bounds (0, 10));
			Assert.AreEqual (rangeValue, scale.Scale (domainValue));
		}

		[TestCase (0,0)]
		[TestCase (1,10)]
		[TestCase (.5,5)]
		[TestCase (.3,3)]
		[TestCase (.6,6)]
		public void TestRange (double domainValue, double rangeValue)
		{
			var scale = new LinearScale (new Bounds (0, 1), new Bounds (0, 10));
			Assert.AreEqual (rangeValue, scale.Scale (domainValue));
		}

		[TestCase (0,0)]
		[TestCase (1,-1)]
		[TestCase (.5,-.5)]
		[TestCase (.3,-.3)]
		[TestCase (.6,-.6)]
		public void TestInverse (double domainValue, double rangeValue)
		{
			var scale = new LinearScale (new Bounds (0, 1), new Bounds (0, -1));
			Assert.AreEqual (rangeValue, scale.Scale (domainValue));
		}

		[TestCase (0,0)]
		[TestCase (1,100)]
		[TestCase (.501,50)]
		[TestCase (.305,30)]
		[TestCase (.607,61)]
		public void TestRound (double domainValue, double rangeValue)
		{
			var scale = new LinearScale (new Bounds (0, 1), new Bounds (0, 100));
			Assert.AreEqual (rangeValue, scale.ScaleRound (domainValue));
		}

		[TestCase (2,0,10)]
		[TestCase (6,0,2,4,6,8,10)]
		[TestCase (11,0,1,2,3,4,5,6,7,8,9,10)]
		public void TestTicks (int count, params double[] ticks)
		{
			var scale = new LinearScale (new Bounds (0, 1), new Bounds (0, 10));
			var t = scale.Ticks (count).ToArray ();
			Assert.AreEqual (ticks.Length, t.Length);
			for (int i = 0; i < count; i++) {
				Assert.AreEqual (ticks [i], t [i]);
			}
		}
	}
}

