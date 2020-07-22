using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Business
{
    public class DataNotUpdatedException:Exception
    {
        //THIS CLASS INHERITS FROM EXCEPTION CLASS
        //IT IS USED TO CREATE AN INSTANCE AND INVOKE CATCH(BUSINESSEXCEPTION BE) BLOCK WHEN A BUSINESS RULE IS NOT MET
        //E.G: NEW ELECTION OVERLAP IN AN OLD ONE
        //THIS EXCEPTION MESSAGE CAN BE DISPLAYED TO THE USER IN A VIEWBAG BY RETURNING THE SAME VIEW

        public DataNotUpdatedException(string message):base(message)
        {

        }
    }
}
