using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Concrete
{
    public class CategoryLanguage
    {
        public int Id { get; set; }
        public string CategoryName { get; set; }
        public string SeoUrl { get; set; }
        public string LangCode { get; set; }
        public int CategoryId { get; set; }
        public Category Category { get; set; }
    }
}

/*
    Category
    1 - Telefon
    2 - Notebook
 
 */

/*
    Category Language
    1 - Telefon | RU | 1
    2 - Telefon | AZ | 1
    3 - Telefon | EN | 1

    4 - Notebook | RU | 2
    5 - Notebook | AZ | 2
    6 - Notebook | EN | 2
 
 */