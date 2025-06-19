// using System;
// using mas_wms.Models;
// using MP3.Meta;
// using MP3.WH;
//
// namespace MP3.WH;
//
// public class KitLine : ILinkCreator<KitLine, Product>, ILinkCreator<KitLine, Kit> {
//     
//     public DateTime LastUpdate { get; private set; }
//     public int QtyRequired { get; private set; }
//     public int QtyActual { get; private set; }
//     public Product Product { get; private set; }
//     public Kit Kit { get; private set; }
//
//
//
//     public KitLine(Product prod, Kit kit, int qtyRequired) {
//
//         if (qtyRequired <= 0 ) {
//             throw new ArgumentException("Quantity required is zero or negative");
//         }
//         this.QtyRequired = qtyRequired;
//
//         if (prod == null || kit == null) {
//             throw new ArgumentException("Associations cannot be null");
//         }
//         
//         Link(kit);
//         Link(prod);
//         RecordUpdate();
//         
//     }
//
//     
//     public void Replenish() {
//         this.QtyActual++;
//         RecordUpdate();
//     }
//
//     public void UpdateQtyRequired(int qtyRequired) {
//         if (qtyRequired == 0) {
//             this.Unlink(this.Kit);
//             this.Unlink(this.Product);
//         }
//         else {
//             this.QtyRequired = qtyRequired;
//             RecordUpdate();
//         }
//
//     }
//     
//     
//
//     private void RecordUpdate() {
//         LastUpdate = DateTime.Now;
//     }
//     
//     
//     public void Link(Product withWhat) {
//         if (this.Product == null && withWhat != null) {
//             this.Product = withWhat;
//             withWhat.Link(this);
//         }
//     }
//     public void Unlink(Product fromWhat) {
//         if (this.Product == fromWhat) {
//             this.Product = null;
//             fromWhat.Unlink(this);
//         }
//     }
//     public void Link(Kit withWhat) {
//         if (this.Kit == null && withWhat != null) {
//             this.Kit = withWhat;
//             withWhat.Link(this);
//         }
//     }
//     
//     public void Unlink(Kit fromWhat) {
//         if (this.Kit == fromWhat) {
//             this.Kit = null;
//             fromWhat.Unlink(this);
//         }
//     }
// }
