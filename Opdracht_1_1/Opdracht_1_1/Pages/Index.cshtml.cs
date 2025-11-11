using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace Opdracht_1_1.Pages
{
    public class IndexModel : PageModel
    {
        public string mijnNaam = "";
        public string mijnStudentnummer = "";
        public string mijnLeeftijd = "";

        public void OnGet()
        {
            string? naam = Request.Query["mijnNaam"];
            if (naam != null)
            {
                mijnNaam = naam;
            }

            string? studentnummer = Request.Query["mijnStudentnummer"];
            if (studentnummer != null)
            {
                mijnStudentnummer = studentnummer;
            }

            string? leeftijd = Request.Query["mijnLeeftijd"];
            if (leeftijd != null)
            {
                mijnLeeftijd = leeftijd;
            }
        }
    }
}
