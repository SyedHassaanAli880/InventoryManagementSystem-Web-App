﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace InventoryManagementSystem.Models
{
    public partial class st_getStockResult
    {
        [Column("Product ID")]
        public long ProductID { get; set; }
        public string Product { get; set; }
        public string Barcode { get; set; }
        [Column("Expiry Date")]
        public string ExpiryDate { get; set; }
        [Column("Buying Date")]
        public string BuyingDate { get; set; }
        [Column("Per Unit Price")]
        public decimal PerUnitPrice { get; set; }
        public string Category { get; set; }
        [Column("Available Stock")]
        public int AvailableStock { get; set; }
        [Column("Total Amount")]
        public decimal? TotalAmount { get; set; }
        public string Status { get; set; }
    }
}