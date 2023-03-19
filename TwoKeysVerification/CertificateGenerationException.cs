using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwoKeysVerification
{
    public class CertificateGenerationException : Exception//обертка над exception
    {
        public CertificateGenerationException(string message) 
            : base(message)
        {

        }
    }
}
