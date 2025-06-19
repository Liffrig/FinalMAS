using System.Text.Json.Serialization;
using mas_wms.Model.Meta;

namespace mas_wms.Model;

[Serializable]
public class Rack : BinLocation {
    
    [JsonIgnore]
    public static double MaxCapacityUtilization = 0.5;
    
    public Rack(string code, string zone,double height, double width, double length): base(code, zone, height, width, length) {
        
    }

    [JsonConstructor]
    private Rack(string code, string zone, bool isBlocked,double height, double width, double length ): base(code, zone, height, width, length) {
        this.IsBlocked = isBlocked;
      
    }


    public override double CalculateMaxCapacity() {
        throw new NotImplementedException();
    }
    public override bool CheckIfCanBeStored() {
        throw new NotImplementedException();
    }
 
}
