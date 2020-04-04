using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace M19G1.Exceptions
{
    public class EmailTemplateNotFoundException:Exception
    {
         public EmailTemplateNotFoundException()
        {
        }

        public EmailTemplateNotFoundException(string message) : base(message)
        {
        }

        public EmailTemplateNotFoundException(string message, Exception innerException) : base(message, innerException)
        {
        }

    }
}