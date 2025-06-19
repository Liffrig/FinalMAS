using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using mas_wms.Model.Meta;

namespace mas_wms.Model;

[Table("Lots")]
public class Lot
{
    [System.ComponentModel.DataAnnotations.Key]
    [MaxLength(10)]
    public string LotNumber { get; set; }
    
    [Required]
    [Column(TypeName = "datetime")]
    public DateTime ExpiryDate { get; set; }
    
    [Required]
    public bool IsBlocked { get; set; }
    
    [Required]
    [StringLength(50)]
    [ForeignKey(nameof(RefItem))]
    public string ItemNumber { get; set; }
    
    // Navigation property
    [Required]
    public virtual Item RefItem { get; set; }

    protected Lot() { }

    private Lot(string lotNumber, DateTime expiryDate, Item item)
    {
        LotNumber = lotNumber;
        ExpiryDate = expiryDate;
        IsBlocked = false;
        RefItem = item;
        ItemNumber = item.ItemNumber;
    }

    public void BlockLot()
    {
        IsBlocked = !IsBlocked;
    }

    public static Lot CreateNewLot(string lotNumber, DateTime expiryDate, Item item)
    {
        return new Lot(lotNumber, expiryDate, item);
    }

    public static void BlockExpiredItems(Item item)
    {
        throw new NotImplementedException();
    }
}

