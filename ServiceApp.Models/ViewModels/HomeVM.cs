﻿using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
 

namespace ServiceApp.Models.ViewModels
{
    public class HomeVM
    {
        public IEnumerable<Article>  ListArticles { get; set; }
        public IEnumerable<Employee> ListEmployees { get; set; }
    }
}
