namespace Nethi.Global_Exception
{
    public class CustomException
    {
        public static Dictionary<string, string> ExceptionMessages { get; } =
        new Dictionary<string, string>
        {
            { "NoId", "Id is not matched.Try Again" },
            { "NoLocate", "No Locations were found.Try Again" },
            { "NoSpot", "No Spots were found.Try Again" },
            { "CantEmpty", "This Entry cannot be null" },
            { "NoUpdate", "No value is Updated" }
        };
    }
}
