using mas_wms.Model;
using mas_wms.Model.Meta;

namespace mas_wms.Tests;

public class ModelTests {
    [SetUp]
    public void Setup() { }

    [Test]
    public void BlockingBinBehaviour() {
        var testBin = new Shelf("1", "A", 1,10, 20,30);
        Assert.That(testBin.IsBlocked, Is.False);
        testBin.BlockBin();
        Assert.That(testBin.IsBlocked, Is.True);
        testBin.BlockBin();
        Assert.That(testBin.IsBlocked, Is.False);
        

    }
    [Test]
    public void BinsAddingToExtent() {
        // Shelf
        var testShelf = new Shelf("9.0", "Z", 2,10, 20,30);
        var eShelf = BusinessObject.GetExtension<Shelf>();
        Assert.That(eShelf, Contains.Item(testShelf));
        
        // Rack
        var testRack = new Rack("9.1", "Z",10, 20,30);
        var eRack = BusinessObject.GetExtension<Rack>();
        Assert.That(eRack, Contains.Item(testRack));
        
        // ReservedArea
        var testRA = new ReservedArea("9.2", "Z",10, 20,30);
        var eRA = BusinessObject.GetExtension<ReservedArea>();
        Assert.That(eRA, Contains.Item(testRA));
        
    }

    [Test]
    public void LoadExtentBins() {
        // Shelf
        var iniCountShelf = BusinessObject.GetExtensionSize<Shelf>();
        BusinessObject.ReadJsonExtension<Shelf>("test_files/shelf.json");
        var eShelf = BusinessObject.GetExtension<Shelf>();
        Assert.That(eShelf.Count, Is.EqualTo(iniCountShelf + 5));
        
        //Rack
        var iniCountRack = BusinessObject.GetExtensionSize<Rack>();
        BusinessObject.ReadJsonExtension<Rack>("test_files/rack.json");
        var eRack = BusinessObject.GetExtension<Rack>();
        Assert.That(eRack.Count, Is.EqualTo(iniCountRack + 4));
        
        // ReservedArea
        var iniCountRA = BusinessObject.GetExtensionSize<ReservedArea>();
        BusinessObject.ReadJsonExtension<ReservedArea>("test_files/reserved_area.json");
        var eRA = BusinessObject.GetExtension<ReservedArea>();
        Assert.That(eRA.Count, Is.EqualTo(iniCountRack + 3));
        
    }

    [Test]
    public void CompositionInFGLot() {

        var testFG = new FinishedGood("AAA1", "abcd", 3, 1, 2, 3, 12.3, 32.3);
        testFG.AddLot("1",DateTime.Now);
        testFG.AddLot("2",DateTime.Now);
        BusinessObject.WriteJsonExtension<FinishedGood>("test1234fg.json");
     
        
        
        
    } 
    
}
