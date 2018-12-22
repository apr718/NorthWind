﻿// Code generated by Microsoft (R) AutoRest Code Generator 0.16.0.0
// Changes may cause incorrect behavior and will be lost if the code is
// regenerated.

namespace MyAPI.Models
{
    using System;
    using System.Linq;
    using System.Collections.Generic;
    using Newtonsoft.Json;
    using Microsoft.Rest;
    using Microsoft.Rest.Serialization;

    public partial class Product
    {
        /// <summary>
        /// Initializes a new instance of the Product class.
        /// </summary>
        public Product() { }

        /// <summary>
        /// Initializes a new instance of the Product class.
        /// </summary>
        public Product(int? productID = default(int?), string productName = default(string), string supplierName = default(string), int? supplierID = default(int?), string categoryName = default(string), int? categoryID = default(int?), string quantityPerUnit = default(string), double? unitPrice = default(double?), int? unitsInStock = default(int?), int? unitsOnOrder = default(int?), int? reorderLevel = default(int?), bool? discontinued = default(bool?))
        {
            ProductID = productID;
            ProductName = productName;
            SupplierName = supplierName;
            SupplierID = supplierID;
            CategoryName = categoryName;
            CategoryID = categoryID;
            QuantityPerUnit = quantityPerUnit;
            UnitPrice = unitPrice;
            UnitsInStock = unitsInStock;
            UnitsOnOrder = unitsOnOrder;
            ReorderLevel = reorderLevel;
            Discontinued = discontinued;
        }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "productID")]
        public int? ProductID { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "productName")]
        public string ProductName { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "supplierName")]
        public string SupplierName { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "supplierID")]
        public int? SupplierID { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "categoryName")]
        public string CategoryName { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "categoryID")]
        public int? CategoryID { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "quantityPerUnit")]
        public string QuantityPerUnit { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "unitPrice")]
        public double? UnitPrice { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "unitsInStock")]
        public int? UnitsInStock { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "unitsOnOrder")]
        public int? UnitsOnOrder { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "reorderLevel")]
        public int? ReorderLevel { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "discontinued")]
        public bool? Discontinued { get; set; }

    }
}