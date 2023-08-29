using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Examples;
internal static class Web
{
   /// <summary>
   /// A Mock method for an asyncronous retrieval of a website's text
   /// </summary>
   /// <returns></returns>
   internal static async Task<string> RetrieveExampleWebsiteText()
   {
      await Task.Delay(250);
      return "This domain is for use in illustrative examples in documents. You may use this domain in literature without prior coordination or asking for permission.";
   }
}
