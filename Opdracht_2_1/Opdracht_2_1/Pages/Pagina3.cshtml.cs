using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Opdracht_2_1.Pages
{
    public class Pagina3Model : PageModel
    {
        public int uitkomst = 0;

        public void OnGet()
        {
            string? getal1Str = Request.Query["getal1"];
            string? getal2Str = Request.Query["getal2"];
            
            if (getal1Str != null && getal2Str != null)
            {
                int getal1 = int.Parse(getal1Str);
                int getal2 = int.Parse(getal2Str);
                uitkomst = getal1 + getal2;
            }
        }
    }
}
