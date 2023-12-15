using BovensteVerdieping;
using System.Collections.Generic;
using System.Threading.Tasks;
using BovensteVerdieping.Models;

namespace Services {
    public interface IRealEstateService {
        Task<List<KeyValuePair<int, RealEstate>>> GetTopRealEstates(bool withGarden = false);
        Task<List<KeyValuePair<int, RealEstate>>> GetTopRealEstatesWithGarden();
    }
}