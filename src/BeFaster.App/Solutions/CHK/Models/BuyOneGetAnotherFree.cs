﻿namespace BeFaster.App.Solutions.CHK.Models
{
    public class BuyOneGetAnotherFree : SpecialOffer
    {
        public char FreeItemId { get; set; }
        public int FreeItemQuantity { get; set; }
    }
}