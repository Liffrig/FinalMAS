using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using mas_wms.Model.Interfaces;
using mas_wms.Model.Meta;
using Microsoft.EntityFrameworkCore;

namespace mas_wms.Model;

[Table("Items")]
public abstract class Item :  IVolume {

    [System.ComponentModel.DataAnnotations.Key]
    [MaxLength(10)]
    public string ItemNumber { get; set; }
    
    [Required]
    [MaxLength(25)]
    public string Description { get; set; }
    
    [Column(TypeName = "decimal(18,2)")]
    public double Height { get; set; }
    [Column(TypeName = "decimal(18,2)")]
    public double Width { get; set; }
    [Column(TypeName = "decimal(18,2)")]
    public double Length { get; set; }
    
    [Column(TypeName = "decimal(18,2)")]
    public abstract double BaseValue { get; set; }
    [Column(TypeName = "decimal(18,2)")]
    public abstract double MaxTemperature { get; set; }
    
    [InverseProperty("RefItem")]
    public virtual ICollection<Lot> Lots { get; set; } = new List<Lot>();
    
    protected Item() { }
    public Item(string itemNumber, string description, double height, double width, double length) {
        ItemNumber = itemNumber;
        Description = description;
        Height = height;
        Width = width;
        Length = length;
        
    }
    
    public void AddLot(string lotNumber, DateTime dt)
    {
        var lot = Lot.CreateNewLot(lotNumber, dt, this);
        Lots.Add(lot);
    }
    
    public static IEnumerable<Item> ShowStockLevel(Item item)
    {
        throw new NotImplementedException();
    }
}
