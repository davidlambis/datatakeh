﻿using SQLite.Net.Attributes;
using SQLiteNetExtensions.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DATATAKEH.Models
{
    public class User
    {      
        [PrimaryKey]
        public int UserId { get; set; }

        public string UserName { get; set; }

        public string Cedula { get; set; }

        public string Password { get; set; }

        public string Email { get; set; }

        public bool IsAdmin { get; set; }

        public bool IsUser { get; set; }

        public bool IsRemembered { get; set; }

        [OneToMany(CascadeOperations = CascadeOperation.All)]
        public List<Project> Projects { get; set; }

        public override int GetHashCode()
        {
            return UserId;
        }

    }
}
