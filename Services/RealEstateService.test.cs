using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.AspNetCore.Mvc.Testing;
using Moq;

using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using BovensteVerdieping.Services;
using BovensteVerdieping.Models;

namespace BovensteVerdieping.Tests {

    [TestClass]
    public class RealEstateServiceTest {

            private static List<string> REAL_ESTATE_NAME = new List<string> { "Name1","Name2" };

          [TestMethod]
          public void With_CacheEmpy_When_GetTopRealEstate_Then_GivesSortedApiResults() {
            var CacheServiceMock = new Mock<CacheService>();
            CacheServiceMock
                .Setup(s => s.get<List<KeyValuePair<int, RealEstate>>>(It.IsAny<string>()))
                .Returns((List<KeyValuePair<int, RealEstate>>)null);
            CacheServiceMock
                .Setup(s => s.set<List<KeyValuePair<int, RealEstate>>>(
                    It.IsAny<string>(),
                    It.IsAny<List<KeyValuePair<int, RealEstate>>>(),
                    It.IsAny<int>()
                ));

            var ApiServiceMock = new Mock<ApiService>();
            List<BovensteVerdieping.Object> apiHouses = new List<BovensteVerdieping.Object>();

            var house = new BovensteVerdieping.Object();
            house.MakelaarId = 1;
            house.MakelaarNaam = REAL_ESTATE_NAME[0];
            apiHouses.Add(house);
            house = new BovensteVerdieping.Object();
            house.MakelaarId = 2;
            house.MakelaarNaam = REAL_ESTATE_NAME[1];
            apiHouses.Add(house);
            house = new BovensteVerdieping.Object();
            house.MakelaarId = 2;
            house.MakelaarNaam = REAL_ESTATE_NAME[1];
            apiHouses.Add(house);
            
            var taskResult = Task.FromResult(apiHouses);
            ApiServiceMock.Setup(s => s.GetHouses(It.IsAny<bool>())).Returns(taskResult);

            var realEstateService = new RealEstateService(ApiServiceMock.Object, CacheServiceMock.Object);
            Task<List<KeyValuePair<int, RealEstate>>> result = realEstateService.GetTopRealEstates(false);

            Assert.AreEqual(2, ((List<KeyValuePair<int, RealEstate>>)result.Result).Count);
            // first element
            Assert.IsInstanceOfType(
                ((List<KeyValuePair<int, RealEstate>>) result.Result)[0].Value, 
                typeof(RealEstate)
            );
            Assert.AreEqual(
                ((List<KeyValuePair<int, RealEstate>>) result.Result)[0].Value.id,
                2
            );
            Assert.AreEqual(
                ((List<KeyValuePair<int, RealEstate>>) result.Result)[0].Value.name,
                REAL_ESTATE_NAME[1]
            );
            Assert.AreEqual(
                ((List<KeyValuePair<int, RealEstate>>) result.Result)[0].Value.houses,
                2
            );
            // second element
            Assert.IsInstanceOfType(
                ((List<KeyValuePair<int, RealEstate>>) result.Result)[1].Value, 
                typeof(RealEstate)
            );
            Assert.AreEqual(
                ((List<KeyValuePair<int, RealEstate>>) result.Result)[1].Value.id,
                1
            );
            Assert.AreEqual(
                ((List<KeyValuePair<int, RealEstate>>) result.Result)[1].Value.name,
                REAL_ESTATE_NAME[0]
            );
            Assert.AreEqual(
                ((List<KeyValuePair<int, RealEstate>>) result.Result)[1].Value.houses,
                1
            );

          }

          [TestMethod]
          public void With_CacheHasHouses_When_GetTopRealEstate_Then_GivesCachedResults() {
            var CacheServiceMock = new Mock<CacheService>();
            var realEstateList = new List<KeyValuePair<int, RealEstate>>();
            realEstateList.Add(new KeyValuePair<int, RealEstate>(0, new RealEstate(2, REAL_ESTATE_NAME[1], 2)));
            realEstateList.Add(new KeyValuePair<int, RealEstate>(1, new RealEstate(1, REAL_ESTATE_NAME[0], 1)));
            CacheServiceMock
                .Setup(s => s.get<List<KeyValuePair<int, RealEstate>>>(It.IsAny<string>()))
                .Returns(realEstateList);
            CacheServiceMock
                .Setup(s => s.set<List<KeyValuePair<int, RealEstate>>>(
                    It.IsAny<string>(),
                    It.IsAny<List<KeyValuePair<int, RealEstate>>>(),
                    It.IsAny<int>()
                ));

            var ApiServiceMock = new Mock<ApiService>();
            List<BovensteVerdieping.Object> apiHouses = new List<BovensteVerdieping.Object>();            
            var taskResult = Task.FromResult(apiHouses);
            ApiServiceMock.Setup(s => s.GetHouses(It.IsAny<bool>())).Returns(taskResult);

            var realEstateService = new RealEstateService(ApiServiceMock.Object, CacheServiceMock.Object);
            Task<List<KeyValuePair<int, RealEstate>>> result = realEstateService.GetTopRealEstates(false);

            Assert.AreEqual(2, ((List<KeyValuePair<int, RealEstate>>)result.Result).Count);
            // first element
            Assert.IsInstanceOfType(
                ((List<KeyValuePair<int, RealEstate>>) result.Result)[0].Value, 
                typeof(RealEstate)
            );
            Assert.AreEqual(
                ((List<KeyValuePair<int, RealEstate>>) result.Result)[0].Value.id,
                2
            );
            Assert.AreEqual(
                ((List<KeyValuePair<int, RealEstate>>) result.Result)[0].Value.name,
                REAL_ESTATE_NAME[1]
            );
            Assert.AreEqual(
                ((List<KeyValuePair<int, RealEstate>>) result.Result)[0].Value.houses,
                2
            );
            // second element
            Assert.IsInstanceOfType(
                ((List<KeyValuePair<int, RealEstate>>) result.Result)[1].Value, 
                typeof(RealEstate)
            );
            Assert.AreEqual(
                ((List<KeyValuePair<int, RealEstate>>) result.Result)[1].Value.id,
                1
            );
            Assert.AreEqual(
                ((List<KeyValuePair<int, RealEstate>>) result.Result)[1].Value.name,
                REAL_ESTATE_NAME[0]
            );
            Assert.AreEqual(
                ((List<KeyValuePair<int, RealEstate>>) result.Result)[1].Value.houses,
                1
            );

          }

    }

}