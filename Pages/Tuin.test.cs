using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.AspNetCore.Mvc.Testing;
using Moq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Hosting;
using System.Threading.Tasks;
using System.Collections.Generic;
using BovensteVerdieping;
using Services;
using BovensteVerdieping.Pages;
using BovensteVerdieping.Models;


namespace BovensteVerdieping.Tests {

    [TestClass]
    public class TuinModelTest : WebApplicationFactory<Startup> {
        
        private static List<string> REAL_ESTATE_NAME = new List<string> { "Name1","Name2","Name3" };

        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureServices(services =>{});
        }

        [TestMethod]
        public void CanGetTopRealEstateWithGarden() {
            var RealEstateServiceMock = new Mock<IRealEstateService>();
            var realEstateList = new List<KeyValuePair<int, RealEstate>>();
            realEstateList.Add(new KeyValuePair<int, RealEstate>(0, new RealEstate(1, REAL_ESTATE_NAME[0], 1)));
            realEstateList.Add(new KeyValuePair<int, RealEstate>(1, new RealEstate(2, REAL_ESTATE_NAME[1], 2)));
            realEstateList.Add(new KeyValuePair<int, RealEstate>(2, new RealEstate(3, REAL_ESTATE_NAME[2], 3)));
            var taskResult = Task.FromResult(realEstateList);
            RealEstateServiceMock.Setup(s => s.GetTopRealEstatesWithGarden()).Returns(taskResult);
            var tuinModel = new TuinModel(null, RealEstateServiceMock.Object);

            var results = tuinModel.OnGetGetTopRealEstates();

            Assert.IsInstanceOfType(results.Result, typeof(JsonResult));
            Assert.AreEqual(((List<KeyValuePair<int, RealEstate>>)results.Result.Value).Count, 3);
            for ( int i = 0; i < 3; i++ ) {
                Assert.IsInstanceOfType(
                    ((List<KeyValuePair<int, RealEstate>>) results.Result.Value)[i].Value, 
                    typeof(RealEstate)
                );
                Assert.AreEqual(
                    ((List<KeyValuePair<int, RealEstate>>) results.Result.Value)[i].Value.id,
                    i + 1
                );
                Assert.AreEqual(
                    ((List<KeyValuePair<int, RealEstate>>) results.Result.Value)[i].Value.name,
                    REAL_ESTATE_NAME[i]
                );
                Assert.AreEqual(
                    ((List<KeyValuePair<int, RealEstate>>) results.Result.Value)[i].Value.houses,
                    i + 1
                );
            }
        }

    }

}