﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MagicItems.Shared.Models
{
    public partial class ShopDTO
    {
        public ShopDTO(string shopName, string shopOwner)
        {
            ShopName = shopName;
            ShopOwner = shopOwner;
        }

        public string ShopName { get; set; } = "New Shop";
        public string ShopOwner { get; set; } = "No name";

    }
}
