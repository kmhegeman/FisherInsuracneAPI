using System;
using System.Collections.Generic;
using FisherInsuranceApi.Models;

namespace FisherInsuranceApi.Data
{
    public class MemoryStore : IMemoryStore
    {
        // A list that runs in-memory and holds quotes
        private Dictionary<int, Quote> quotes;
        // A list that runs in-memory and holds claims
        private Dictionary<int, Claim> claims;

        // Constructor. This fills our in memory store with quotes for Auto Insurance
        public MemoryStore()
        {
            quotes = new Dictionary<int, Quote>();

            new List<Quote>
            {
                new Quote { Product="Auto", ExpireDate=DateTime.Now.AddDays(45), Price=350.00M }, 
                new Quote { Product="Auto", ExpireDate=DateTime.Now.AddDays(45), Price=300.00M },
                new Quote { Product="Auto", ExpireDate=DateTime.Now.AddDays(45), Price=450.00M },
                new Quote { Product="Auto", ExpireDate=DateTime.Now.AddDays(45), Price=250.00M },
            }.ForEach(quote => this.CreateQuote(quote)); 

            claims = new Dictionary<int, Claim>();
            new List<Claim>
            {
                new Claim { PolicyNumber="OH89600", LossDate=DateTime.Now.AddDays(-17), LossAmount=10000.00M, Status="Open"},
                new Claim { PolicyNumber="OH48780", LossDate=DateTime.Now.AddDays(-24), LossAmount=4000.00M, Status="Open"},
                new Claim { PolicyNumber="OH84580", LossDate=DateTime.Now.AddDays(-31), LossAmount=50000.00M, Status="Open"},
                new Claim { PolicyNumber="OH59750", LossDate=DateTime.Now.AddDays(-10), LossAmount=19000.00M, Status="Paid"}
            }.ForEach(claim => this.CreateClaim(claim));

        }

        // Simulate 'Create' in data store
        public Quote CreateQuote(Quote quote)
        {
            if (quote.Id == 0)
            {
                int key = quotes.Count;

                while (quotes.ContainsKey(key))
                {
                    key++;
                };
                quote.Id = key;
            }
            quotes[quote.Id] = quote;
            return quote;
        }

        // Simulate 'Retrieve All' - a lambda expression for getting all quotes
        public IEnumerable<Quote> RetrieveAllQuotes => quotes.Values;

        // Simulate 'Retrieve'  - lambda expression for getting a specific quote; returns null if not found
        public Quote RetrieveQuote(int id) => quotes.ContainsKey(id) ? quotes[id] : null;

        // Simulate 'Update'  - lambda expression for getting a specific quote; returns null if not found
        public Quote UpdateQuote(Quote quote)
        {
            quotes[quote.Id] = quote;
            return quote;
        }

        // Simulate 'Delete'  
        public void DeleteQuote(int id)
        {
            quotes.Remove(id);
        }

        public Claim CreateClaim(Claim claim)
        {
              if (claim.Id == 0)
            {
                int key = claims.Count;

                while (claims.ContainsKey(key))
                {
                    key++;
                };
                claim.Id = key;
            }
            claims[claim.Id] = claim;
            return claim;
        }

        public IEnumerable<Claim> RetrieveAllClaims => claims.Values;

        public Claim RetrieveClaim(int id) => claims.ContainsKey(id) ? claims[id] : null;

        public Claim UpdateClaim(Claim claim)
        {
            claims[claim.Id] = claim;
            return claim;
        }
        
        public void DeleteClaim(int id)
        {
            claims.Remove(id);
        }
    }
}