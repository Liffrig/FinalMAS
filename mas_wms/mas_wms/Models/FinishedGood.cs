using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using mas_wms.Model.Meta;

namespace mas_wms.Model;

[Table("Items")]
public class FinishedGood : Item
{
    [Column(TypeName = "decimal(18,2)")]
    public override double BaseValue { get; set; }
    [Column(TypeName = "decimal(18,2)")]
    public override double MaxTemperature { get; set; }
    
    [Column(TypeName = "decimal(18,3)")]
    [Range(0.001, double.MaxValue, ErrorMessage = "Weight must be positive")]
    public double Weight { get; set; }


    protected FinishedGood() { }

    public FinishedGood(
        string itemNumber,
        string description,
        double weight,
        double height,
        double width,
        double length,
        double baseValue,
        double maxTemperature)
        : base(itemNumber, description, height, width, length)
    {
        BaseValue = baseValue;
        MaxTemperature = maxTemperature;
        Weight = weight;
    }
}
