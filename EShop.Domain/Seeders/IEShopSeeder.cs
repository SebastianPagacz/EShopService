﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EShop.Domain.Repositories;

namespace EShop.Domain.Seeders;

public interface IEShopSeeder
{
    Task SeedAsync();
}
