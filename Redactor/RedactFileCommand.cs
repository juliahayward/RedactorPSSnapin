﻿using System.Management.Automation;


namespace JuliaHayward.Redactor
{
    [Cmdlet("Redact", "File")]
    public class RedactFileCommand : Cmdlet
    {
        [Parameter(Position = 0, Mandatory=true, HelpMessage = "Name of file(s) to redact")]
        public string[] Name
        {
            get { return fileNames; }
            set { fileNames = value; }
        }

        private string[] fileNames;

        protected override void ProcessRecord()
        {
            var dict = new TokenDictionary();
            dict.Load();

            foreach (string name in fileNames)
            {
                WriteVerbose("Redacting " + name);
                var fileContents = System.IO.File.ReadAllText(name);

                foreach (var token in dict.RedactionTokens.Keys)
                    fileContents = fileContents.Replace(token, dict.RedactionTokens[token]);

                System.IO.File.WriteAllText(name, fileContents);

            }
        }
    }
}
