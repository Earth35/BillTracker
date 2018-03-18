﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace TrackerLogic
{
    public static class Validator
    {
        public static bool BasicStatus = false;
        public static bool DateStatus = false;
        public static bool SymbolStatus = false;
        public static bool AmountStatus = false;

        public static void Run (string invoiceNumber, string issuedBy, string issueDate, string paymentDueDate,
            string monthSymbol, string yearSymbol, string totalAmountCharged, ref string symbolInput, ref string amountInput)
        {
            RunBasicValidation(invoiceNumber, issuedBy, issueDate, paymentDueDate, monthSymbol, yearSymbol, totalAmountCharged);
            RunDateValidation(issueDate, paymentDueDate);
            RunSymbolValidation(ref symbolInput);
            RunAmountValidation(ref amountInput);
        }

        private static void RunBasicValidation (string invoiceNumber, string issuedBy, string issueDate, string paymentDueDate,
            string monthSymbol, string yearSymbol, string totalAmountCharged)
        {
            List<bool> inputStatus = new List<bool>();
            inputStatus.Add(String.IsNullOrWhiteSpace(invoiceNumber));
            inputStatus.Add(String.IsNullOrWhiteSpace(issuedBy));
            inputStatus.Add(String.IsNullOrWhiteSpace(issueDate));
            inputStatus.Add(String.IsNullOrWhiteSpace(paymentDueDate));
            inputStatus.Add(String.IsNullOrWhiteSpace(monthSymbol));
            inputStatus.Add(String.IsNullOrWhiteSpace(yearSymbol));
            inputStatus.Add(String.IsNullOrWhiteSpace(totalAmountCharged));

            if (inputStatus.All(s => s == false))
            {
                BasicStatus = true;
            }
            else
            {
                BasicStatus = false;
            }
        }

        private static void RunDateValidation (string issueDate, string paymentDeadline)
        {
            Regex format = new Regex(@"^\d{4}-\d{2}-\d{2}$");

            if ((format.IsMatch(issueDate) == true) && (format.IsMatch(paymentDeadline) == true))
            {
                DateStatus = true;
            }
            else
            {
                DateStatus = false;
            }
        }

        private static void RunSymbolValidation (ref string symbolInput)
        {
            Regex symbolPatternValidPartial = new Regex(@"^([1-9])/\d{2}$"); // 1-9 range for month variant
            Regex symbolPatternValidFull = new Regex(@"^(0[1-9])|(1[0-2])/\d{2}$"); // 1-12 range for M(M), any 2-digit year

            if (symbolPatternValidPartial.IsMatch(symbolInput))
            {
                symbolInput = symbolInput.Insert(0, "0");
                SymbolStatus = true;
            }
            else if (symbolPatternValidFull.IsMatch(symbolInput))
            {
                SymbolStatus = true;
            }
            else
            {
                SymbolStatus = false;
            }
        }

        private static void RunAmountValidation (ref string amountInput)
        {
            Regex plnOnlyPattern = new Regex(@"^\d+$");
            Regex incompleteDecimalPattern = new Regex(@"^\d+(\.|,)\d$");
            Regex fullPattern = new Regex(@"^\d+(\.|,)\d{2}$");

            if (plnOnlyPattern.IsMatch(amountInput))
            {
                amountInput += ",00";
                AmountStatus = true;
            }
            else if (incompleteDecimalPattern.IsMatch(amountInput))
            {
                ValidateDecimalSeparator(ref amountInput);
                amountInput += "0";
                AmountStatus = true;
            }
            else if (fullPattern.IsMatch(amountInput))
            {
                ValidateDecimalSeparator(ref amountInput);
                AmountStatus = true;
            }
            else
            {
                AmountStatus = false;
            }
        }

        private static void ValidateDecimalSeparator(ref string input)
        {
            Regex pattern = new Regex(@"\.\d{1,2}$");

            if (pattern.IsMatch(input))
            {
                input.Replace(".", ",");
            }
        }
    }
}
