using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Slvz_Unrecoverable.SLVZ.Solution;

public class CheckForPremium
{
    public static async Task<bool> Premium()
    {
        try
        {
            var response = await new HttpClient().GetStringAsync("https://slvz.ir/api/unrecoverable/premium");

            return response == "true" ? true : false;
        }
        catch { return false; }
    }
}
