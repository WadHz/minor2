using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace Opdracht_3_1.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;

        public string bankNaam = "";
        public string klantNaam = "";
        public string adres = "";
        public string woonplaats = "";
        public string type = "";
        public string beschrijving = "";
        public decimal prijs = 0;
        public bool toonBevestiging = false;

        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
            string? param1 = Request.Query["param1"];
            string? param2 = Request.Query["param2"];
            string? param3 = Request.Query["param3"];
            string? param4 = Request.Query["param4"];

            if (param1 != null && param2 != null)
            {
                bankNaam = param1;
                klantNaam = param2;
                adres = param3 ?? "";
                woonplaats = param4 ?? "";
                LeesBank(bankNaam);
                toonBevestiging = true;
            }
        }

        private void LeesBank(string bank)
        {
            string path = Path.Combine(Directory.GetCurrentDirectory(), "Bank.txt");
            if (System.IO.File.Exists(path))
            {
                string[] lines = System.IO.File.ReadAllLines(path);
                foreach (string line in lines)
                {
                    string[] parts = line.Split('|');
                    if (parts.Length >= 4 && parts[0].Equals(bank, StringComparison.OrdinalIgnoreCase))
                    {
                        bankNaam = parts[0];
                        type = parts[1];
                        beschrijving = parts[2];
                        prijs = decimal.Parse(parts[3]);
                        break;
                    }
                }
            }
        }
    }
}
