using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.AspNetCore.Mvc.Testing;
using System;
using System.Collections.Generic;
using BovensteVerdieping.Services;
using BovensteVerdieping.Models;

namespace BovensteVerdieping.Tests {

    [TestClass]
    public class SampleDataServiceTest {

        [TestMethod]
        public void When_GetSampleData_Then_GetIt() {
            var sampleDataService = new SampleDataService();
            List<KeyValuePair<int, RealEstate>> result = sampleDataService.GetRealEstates();

            Assert.AreEqual(10, result.Count);
            for ( int i = 0; i < 10; i++ ) {
                Assert.IsInstanceOfType( result[i].Value, typeof(RealEstate) );
                Assert.AreEqual( i + 1, result[i].Value.id);
                Assert.IsInstanceOfType( result[i].Value.name, typeof(string));
                Assert.IsNotNull( result[i].Value.houses );
            }
        }

    }

}