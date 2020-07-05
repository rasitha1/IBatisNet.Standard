using System;
using SqlBatis.DataMapper.Utilities.Objects;
using NUnit.Framework;


using SqlBatis.DataMapper.Test.Domain;

namespace SqlBatis.DataMapper.Test.NUnit.CommonTests.Utilities
{
	/// <summary>
	/// Summary description for ReflectionInfoTest.
	/// </summary>
	[TestFixture]
	public class ReflectionInfoTest
	{
		/// <summary>
		/// Test multiple call to factory
		/// </summary>
		[Test]
		public void TestReflectionInfo()
		{

			ReflectionInfo info = ReflectionInfo.GetInstance(typeof (AbstractDocument));
			
			Type type = info.GetGetterType("PageNumber");
		}

        /// <summary>
        /// Test Finding properties on interfaces which "inherites" other interfaces
        /// </summary>
        [Test]
        public void TestJIRA210OnGet()
        {
            ReflectionInfo info = ReflectionInfo.GetInstance(typeof(IAddress));

            Type type = info.GetGetterType("Id");
            Assert.IsNotNull(type);
        }

        /// <summary>
        /// Test Finding properties on interfaces which "inherites" other interfaces
        /// </summary>
        [Test]
        public void TestJIRA210OnSet()
        {
            ReflectionInfo info = ReflectionInfo.GetInstance(typeof(IAddress));

            Type type = info.GetSetterType("Id");
            Assert.IsNotNull(type);
        }
	}
}
