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

namespace BovensteVerdieping.Tests {

    [TestClass]
    public class TuinModelTest : WebApplicationFactory<Startup> {

        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureServices(services =>{});
        }

        [TestMethod]
        public void CanGetTopRealEstateWithGarden() {
            var RealEstateServiceMock = new Mock<IRealEstateService>();
            var realEstateList = new List<KeyValuePair<int, RealEstate>>();
            realEstateList.Add(new KeyValuePair<int, RealEstate>(0, new RealEstate()));
            realEstateList.Add(new KeyValuePair<int, RealEstate>(1, new RealEstate()));
            realEstateList.Add(new KeyValuePair<int, RealEstate>(2, new RealEstate()));
            var taskResult = Task.FromResult(realEstateList);
            RealEstateServiceMock.Setup(s => s.GetTopRealEstatesWithGarden()).Returns(taskResult);
            var tuinModel = new TuinModel(null, RealEstateServiceMock.Object);

            var results = tuinModel.OnGetGetTopRealEstates();

            Assert.IsInstanceOfType(results.Result, typeof(JsonResult));
            Assert.AreEqual(((List<KeyValuePair<int, RealEstate>>)results.Result.Value).Count, 3);
        }

    }

}