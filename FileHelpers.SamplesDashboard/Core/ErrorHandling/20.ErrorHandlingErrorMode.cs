﻿using System;
using System.Collections;
using System.Collections.Generic;
using FileHelpers;

namespace Demos
{
    /// <summary>
    /// Simple class with use the error mode for a value not in the enumerator
    /// </summary>
    public class ErrorModeErrorHandlingDemo
        :IDemo
    {
        //-> {Example.Name:ErrorMode Error handling}
        //-> {Example.Description:Read the file rejecting bad records}

        //-> {Example.File:RunEngine.cs}
        /// <summary>
        /// Run an example of running a file with an error using the
        /// ErrorMode option to capture bad records
        /// </summary>
        /// <remarks>
        /// In the standard mode you can catch the exceptions when something fails.
        /// </remarks>
        public void Run()
        {
            var engine = new DelimitedFileEngine<Customer>();

            // Switch error mode on
            engine.ErrorManager.ErrorMode = ErrorMode.SaveAndContinue;

            //  This fails with not in enumeration error
            Customer[] customers = engine.ReadFile("Input.txt");

            // This will display error from line 2 of the file.
            foreach (ErrorInfo err in engine.ErrorManager.Errors)
            {
                Console.WriteLine();
                Console.WriteLine("Error on Line number: {0}", err.LineNumber);
                Console.WriteLine("Record causing the problem: {0}", err.RecordString);
                Console.WriteLine("Complete exception information: {0}", err.ExceptionInfo.ToString());
            }
        }
        //-> {/Example.File}

        //-> {Example.File:Customers with Enum.cs}
        /// <summary>
        /// Sample customer class that is delimited by | default
        /// </summary>
        /// <remarks>
        /// Notice last feild is our enumerator
        /// </remarks>
        [DelimitedRecord("|")]
        public class Customer
        {
            public string CustomerID;
            public string CompanyName;
            public string ContactName;
            public CustomerTitle ContactTitle;
        }
        //-> {/Example.File}

        //-> {Example.File:Input.txt}
        /*ALFKI|Alfreds Futterkiste|Maria Anders|SalesRepresentative
        ANATR|Ana Trujillo Emparedados y helados|Ana Trujillo|NotInEnum
        FRANR|France restauration|Carine Schmitt|MarketingManager
        ANTON|Antonio Moreno Taquería|Antonio Moreno|Owner*/
        //-> {/Example.File}

        //-> {Example.File:TheEnumerator.cs}
        /// <summary>
        /// Different titles describing position in company
        /// </summary>
        public enum CustomerTitle
        {
            Owner,
            SalesRepresentative,
            MarketingManager
        }
        //-> {/Example.File}
    }
}