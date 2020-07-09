using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

//namespace WebApplication1.Localization
//the above namespace will push the compiler to look for the resources files in the location: WebApplication1.Resources.Localization
//which doesn't exist, so we need to rectify the namespace of Labels.Cs/Messages.cs/Menu.cs by removing the word 'Localization' from it

namespace WebApplication1
{
    public class Labels
    {
        //this class is used to be passed to IStringLocalizer<T> when using Localization of different html elements text
    }
}
