using System.Collections.Generic;
using System;
using BovensteVerdieping.Models;

namespace BovensteVerdieping.Services {

    public class SampleDataService {

        public SampleDataService() {}

        public List<KeyValuePair<int, RealEstate>> GetRealEstates() {
            List<KeyValuePair<int, RealEstate>> realEstates = new List<KeyValuePair<int, RealEstate>>();
            realEstates.Add(new KeyValuePair<int, RealEstate>(1, new RealEstate(1, "Maakelar Ein", 12)));
            realEstates.Add(new KeyValuePair<int, RealEstate>(2, new RealEstate(2, "Maakelar Twee", 9)));
            realEstates.Add(new KeyValuePair<int, RealEstate>(3, new RealEstate(3, "Maakelar Drie", 7)));
            realEstates.Add(new KeyValuePair<int, RealEstate>(4, new RealEstate(4, "Maakelar View", 5)));
            realEstates.Add(new KeyValuePair<int, RealEstate>(5, new RealEstate(5, "Maakelar Vijf", 3)));
            realEstates.Add(new KeyValuePair<int, RealEstate>(6, new RealEstate(6, "Maakelar Zes", 2)));
            realEstates.Add(new KeyValuePair<int, RealEstate>(7, new RealEstate(7, "Maakelar Zeven", 2)));
            realEstates.Add(new KeyValuePair<int, RealEstate>(8, new RealEstate(8, "Maakelar Acht", 1)));
            realEstates.Add(new KeyValuePair<int, RealEstate>(9, new RealEstate(9, "Maakelar Negen", 1)));
            realEstates.Add(new KeyValuePair<int, RealEstate>(10, new RealEstate(10, "Maakelar Tien", 1)));
            return realEstates;
        }

    }

}