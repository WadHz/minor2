using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace opdr3.Pages
{
    public class BestelModel : PageModel
    {
        public string bank = "";
        public string type = "";
        public string beschrijving = "";
        public decimal prijs = 0;

        public void OnGet()
        {
            string? bankParam = Request.Query["bank"];
            if (bankParam != null)
            {
                bank = bankParam;
                LeesBank(bank);
            }
        }

        public IActionResult OnPost()
        {
            string? naam = Request.Form["naam"];
            string? adres = Request.Form["adres"];
            string? woonplaats = Request.Form["woonplaats"];
            string? bankParam = Request.Form["bank"];

            if (bankParam != null)
            {
                bank = bankParam;
            }

         
            List<string> bestelling = new List<string> { bank, naam ?? "", adres ?? "", woonplaats ?? "" };
            HttpContext.Session.SetString("bestelling", string.Join("|", bestelling));

            return RedirectToPage("Index");
        }

        private void LeesBank(string bankNaam)
        {
            string path = Path.Combine(Directory.GetCurrentDirectory(), "Bank.txt");
            if (System.IO.File.Exists(path))
            {
                string[] lines = System.IO.File.ReadAllLines(path);
                foreach (string line in lines)
                {
                    string[] parts = line.Split('|');
                    if (parts.Length >= 4 && parts[0].Equals(bankNaam, StringComparison.OrdinalIgnoreCase))
                    {
                        bank = parts[0];
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
