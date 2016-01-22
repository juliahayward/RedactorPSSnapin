﻿using System.Management.Automation;


namespace JuliaHayward.Redactor
{
    [Cmdlet("Redact", "File", HelpUri = "https://github.com/juliahayward/RedactorPSPlugin")]
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
            foreach (string name in fileNames)
            {
                WriteVerbose("Redacting " + name);
                var fileContents = System.IO.File.ReadAllText(name);

                foreach (var token in TokenDictionary.RedactionTokens.Keys)
                    fileContents = fileContents.Replace(token, TokenDictionary.RedactionTokens[token]);

                System.IO.File.WriteAllText(name, fileContents);

            }
        }
    }
}
