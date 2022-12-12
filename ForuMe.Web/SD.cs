namespace ForuMe.Web
{
    public static class SD
    {
        public static string BlogAPIBase { get; set; }
        public static string IdentityAPIBase { get; set; }
        public enum ApiType
        { 
            GET, 
            POST, 
            PUT, 
            DELETE
        }

    }
}
