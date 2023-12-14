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
    public class IndexModelTest : WebApplicationFactory<Startup> {

        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureServices(services =>{});
        }

        [TestMethod]
        public void CanGetTopRealEstate() {
            var RealEstateServiceMock = new Mock<IRealEstateService>();
            var realEstateList = new List<KeyValuePair<int, RealEstate>>();
            realEstateList.Add(new KeyValuePair<int, RealEstate>(0, new RealEstate()));
            var taskResult = Task.FromResult(realEstateList);
            RealEstateServiceMock.Setup(s => s.GetTopRealEstates(false)).Returns(taskResult);
            var indexModel = new IndexModel(null, RealEstateServiceMock.Object);

            var results = indexModel.OnGetGetTopRealEstates();

            Assert.IsInstanceOfType(results.Result, typeof(JsonResult));
            Assert.AreEqual(((List<KeyValuePair<int, RealEstate>>)results.Result.Value).Count, 1);
        }

    }

}