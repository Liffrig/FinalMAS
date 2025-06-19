// using MP3.WH;
//
// namespace mas_wms.Models;
//
// public class Kit: Item {
//
//     
//     private ICollection<KitLine> _definition = new HashSet<KitLine>();
//     public ICollection<KitLine> Definition => _definition.ToList().AsReadOnly();
//     
//     public override decimal BaseValue { get; set; }
//     public override decimal MaxTemperature { get; set; }
//
//
//     public Kit(
//         string itemNumber,
//         string description,
//         double height,
//         double width,
//         double length) :
//         base(itemNumber, description, height, width, length) { }
//     
//     
//     
//     public void Link(KitLine withWhat) {
//         if (_definition.Contains(withWhat)) return;
//         
//         this._definition.Add(withWhat);
//         withWhat.Link(this);
//         
//     }
//     public void Unlink(KitLine fromWhat) {
//         if (_definition.Contains(fromWhat)) {
//             this._definition.Remove(fromWhat);
//             fromWhat.Unlink(this);
//         }
//     }
//
//  
// }
