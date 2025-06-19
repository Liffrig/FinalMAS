using mas_wms.Model.Interfaces;
using mas_wms.Model.Meta;
namespace mas_wms.Model;

public abstract class BinLocation: IVolume {
    public string Code { get; private set; }
    public string Zone { get; private set; }
    public bool IsBlocked { get; protected set; }
    
    public double Height { get; set; }
    public double Width { get; set; }
    public double Length { get; set; }
    
    protected BinLocation(string code, string zone, double height, double width, double length) {
        this.Code = code;
        this.Zone = zone;
        this.IsBlocked = false;
        this.Height = height;
        this.Width = width;
        this.Length = length;
    }

    public abstract double CalculateMaxCapacity();
    public abstract bool CheckIfCanBeStored();

    public void BlockBin() {
        this.IsBlocked = !IsBlocked;
    }


}
