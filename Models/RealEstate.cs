

namespace BovensteVerdieping.Models {

    public class RealEstate {
        public int id { get; set; }
        public string name { get; set; }
        public int houses { get; set; }

        public RealEstate() {}

        public RealEstate(int id, string name, int houses) {
            this.id = id;
            this.name = name;
            this.houses = houses;
        }
    }

}