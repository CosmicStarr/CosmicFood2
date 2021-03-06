﻿using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Models;

namespace CosmicFood2.Data.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Category> GetCategories { get; set; }
        public DbSet<FoodType> GetFoodTypes { get; set; }
        public DbSet<MenuItems> GetMenuItems { get; set; }
    }
}
