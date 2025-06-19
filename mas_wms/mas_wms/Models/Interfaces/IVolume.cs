namespace mas_wms.Model.Interfaces;

public interface IVolume {
    double Height { get; protected set; }
    double Width { get; protected set; }
    double Length { get; protected set; }

    double CalculateVolume(bool inMeters = false) {
        var cmResult = (this.Height * this.Width * this.Length);
        if (inMeters) return (cmResult / 1_000_000);
        return cmResult;
        
    }

}
