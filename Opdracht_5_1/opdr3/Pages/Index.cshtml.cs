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
        public DateTime besteldatum = DateTime.Now;
        public DateTime verwachteLevering = DateTime.Now.AddDays(42); // 6 weken = 42 dagen

        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
            // Lees gegevens uit Session
            string? bestellingStr = HttpContext.Session.GetString("bestelling");
            
            if (bestellingStr != null)
            {
                string[] gekozenBank = bestellingStr.Split('|');
                if (gekozenBank.Length >= 4)
                {
                    bankNaam = gekozenBank[0];
                    klantNaam = gekozenBank[1];
                    adres = gekozenBank[2];
                    woonplaats = gekozenBank[3];
                    LeesBank(bankNaam);
                    toonBevestiging = true;
                    
                    // Bereken datums
                    besteldatum = DateTime.Now;
                    verwachteLevering = besteldatum.AddDays(42); // 6 weken
                    
                    // Clear de session na het tonen
                    HttpContext.Session.Clear();
                }
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
