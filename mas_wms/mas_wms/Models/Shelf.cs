using System.Text.Json.Serialization;
using mas_wms.Model.Meta;
namespace mas_wms.Model;

[Serializable]
public class Shelf : BinLocation {
    
    [JsonIgnore]
    public static double MaxCapacityUtilization = 0.3;
    
    private int _level;
    public int Level { 
        get => _level;
        set {
            if (value >= 0 && value < 4) {
                _level = value;
            }   
            else throw new ShelfLevelOutOfRangeException();
        } }


    public Shelf(string code, string zone, int level,double height, double width, double length ): base(code, zone, height, width, length) {
        this._level = level;
        
    }

    [JsonConstructor]
    private Shelf(string code, string zone, int level, bool isBlocked, double height, double width, double length ): base(code, zone, height, width, length) {
        this.Level = level;
        this.IsBlocked = isBlocked;
        
    }
    
    
    public override double CalculateMaxCapacity() {
        throw new NotImplementedException();
    }
    public override bool CheckIfCanBeStored() {
        throw new NotImplementedException();
    }

}
