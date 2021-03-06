﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chariot.Framework.SurtiApp
{
    public class Inventario_bodegaModelApp
    {
        public List<InventarioDetalebodega> warehouse_inventory { get; set; }  = new List<InventarioDetalebodega>();


    }

    public class InventarioDetalebodega
    {
        public int subwarehouse { get; set; }
        public ProductoBodega product { get; set; } = new ProductoBodega();
        public double? quantity { get; set; } = 180;
        public double? weight { get; set; } = 18.200;
        public double? warehouse_quantity { get; set; } = 0;
        public double? id { get; set; }

    }

    public class ProductoBodega
    {

        public string name { get; set; }
        public string short_description { get; set; }
        public string full_description { get; set; }
        public string sku { get; set; }
        public double? approved_rating_sum { get; set; }
        public double? price { get; set; }
        public bool disable_buy_button { get; set; }
        public double? stock_quantity { get; set; }
        public bool has_tier_prices { get; set; }
        public List<int> manufacturer_ids { get; set; } = new List<int>(); 
        public List<int> category_ids { get; set; } = new List<int>();
        public List<ImagePoductoBodega> images { get; set; } = new List<ImagePoductoBodega>();
        public bool has_discounts_applied { get; set; }
        public List<int> discount_ids { get; set; }
    


        public double? conversion_product_id { get; set; } = 0;
        public double? inventory_warehouse { get; set; }
        public double? inventory_weight { get; set; } = 10;
        public double? weight_warehouse { get; set; } = 12.2;
        public double? is_price_by_unit { get; set; }
        public double? price_by_unit { get; set; } = 1.21;
        public string unit_type { get; set; }
        public bool is_taxed { get; set; }
        public int id { get; set; }
    }


    public class ImagePoductoBodega
    {
        public int id { get; set; }

        public int picture_id { get; set; }

        public int position { get; set; }
        public string src { get; set; }
        public string attachment { get; set; }

    }


}