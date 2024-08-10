using ERPv1.HR.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ERPv1.HR.Services
{
    public class TaxManager
    {


        public decimal CalcTaxUpdated(decimal MonthlySalary, decimal Efaa)
        {
            var YearlySalary = MonthlySalary * 12;
            decimal TaxableSalary = 0;
            TaxableSalary =  YearlySalary - Efaa;
           
            
            decimal Tax = 0;
            List<decimal> TaxDetails = new List<decimal>();
            var Brackets = new List<TaxBrackets>()
            {
                new TaxBrackets(){ MinValue = 0 , MaxValue = 15000 , TaxPrecentage=0 ,MinRange = 0, MaxRange= 600000,StartBrakcet =0  ,IsLastBraket= false, WithMaxValue=false},
                new TaxBrackets(){ MinValue = 15000 , MaxValue = 30000 , TaxPrecentage=0.025M, MinRange = 600000, MaxRange= 700000,StartBrakcet =1,IsLastBraket= false, WithMaxValue=false},
                new TaxBrackets(){ MinValue = 30000 , MaxValue = 45000 , TaxPrecentage=0.1M,MinRange = 700000, MaxRange= 800000,StartBrakcet =2, IsLastBraket= false, WithMaxValue=false},
                new TaxBrackets(){ MinValue = 45000 , MaxValue = 60000 , TaxPrecentage=0.15M,MinRange = 800000, MaxRange= 900000,StartBrakcet =3, IsLastBraket= false, WithMaxValue=false},
                new TaxBrackets(){ MinValue = 60000 , MaxValue = 200000 , TaxPrecentage=0.2M,MinRange = 900000, MaxRange= 1000000,StartBrakcet =4, IsLastBraket= false, WithMaxValue=false},
                new TaxBrackets(){ MinValue = 200000 , MaxValue = 400000 , TaxPrecentage=0.225M,MinRange = 1000000, MaxRange= 0,StartBrakcet =5, IsLastBraket= false, WithMaxValue=false},
                new TaxBrackets(){ MinValue = 400000 , MaxValue = 0 , TaxPrecentage=0.25M,MinRange = 0, MaxRange= 0,StartBrakcet =1, IsLastBraket= true, WithMaxValue=false},
            };

            var FirstBracket = Brackets.Where(
                x => (TaxableSalary >=x.MinRange  && TaxableSalary < x.MaxRange) 
            || (TaxableSalary>=x.MinRange && x.MaxRange == 0)).FirstOrDefault();

            if(FirstBracket.StartBrakcet >0)
            {
                FirstBracket.WithMaxValue = true;
            }



            for (int i = FirstBracket.StartBrakcet; i < Brackets.Count; i++)
            {
                decimal differance = 0;
                decimal BracketTax = 0;
                if (Brackets[i].WithMaxValue)
                {
                    differance = Brackets[i].MaxValue;
                }
                else
                {
                    differance = Brackets[i].MaxValue - Brackets[i].MinValue;
                }
                
                if (!Brackets[i].IsLastBraket)
                {
                    if (TaxableSalary <= differance)
                    {
                        BracketTax= TaxableSalary * Brackets[i].TaxPrecentage;
                        TaxDetails.Add(BracketTax);
                        Tax += BracketTax;

                        return Tax;
                    }
                    else
                    {
                        BracketTax = differance * Brackets[i].TaxPrecentage;
                        TaxDetails.Add(BracketTax);
                        Tax += BracketTax;
                        TaxableSalary = TaxableSalary - differance;
                    }
                }
                else
                {
                    BracketTax = TaxableSalary * Brackets[i].TaxPrecentage;
                    TaxDetails.Add(BracketTax);
                    Tax += BracketTax;
                    return Tax;
                }

            }

            return Tax;
        }

        public decimal CalcTax(decimal MonthlySalary, decimal Efaa)
        {
            var YearlySalary = MonthlySalary * 12;
            var TaxableSalary = YearlySalary - Efaa;
            decimal Tax = 0;

            // Bracket 1
            if (TaxableSalary <= 15000)
            {

                TaxableSalary = 0;
                return Tax;
            }
            else
            {
                TaxableSalary = TaxableSalary - 15000;
            }
            // Bracket 2
            if (TaxableSalary <= 15000)
            {
                Tax += TaxableSalary * 0.025M;
                return Tax;
            }
            else
            {
                Tax += 15000 * 0.025M;
                TaxableSalary = TaxableSalary - 15000;
            }

            // Bracket 3
            if (TaxableSalary <= 15000)
            {
                Tax += TaxableSalary * 0.1M;
                return Tax;
            }
            else
            {
                Tax += 15000 * 0.1M;
                TaxableSalary = TaxableSalary - 15000;
            }
            // Bracket 4
            if (TaxableSalary <= 15000)
            {
                Tax += TaxableSalary * 0.15M;
                return Tax;
            }
            else
            {
                Tax += 15000 * 0.15M;
                TaxableSalary = TaxableSalary - 15000;
            }
            // Bracket 5
            if (TaxableSalary <= 140000)
            {
                Tax += TaxableSalary * 0.2M;
                return Tax;
            }
            else
            {
                Tax += 140000 * 0.2M;
                TaxableSalary = TaxableSalary - 140000;
            }

            // Bracket 6
            if (TaxableSalary <= 200000)
            {
                Tax += TaxableSalary * 0.225M;
                return Tax;
            }
            else
            {
                Tax += 200000 * 0.225M;
                TaxableSalary = TaxableSalary - 200000;
            }
            // Bracket 7
            if (TaxableSalary > 0)
            {
                Tax += TaxableSalary * 0.25M;
                return Tax;
            }
            return Tax;
        }

    }
}
