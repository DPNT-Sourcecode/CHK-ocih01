namespace BeFaster.App.Solutions.CHK.Models
{
    public class BuyOneGetAnotherFreeOffer : SpecialOffer
    {
        public char ProductId { get; set; }
        public char FreeItemId { get; set; }
        public int FreeItemQuantity { get; set; }
    }
}