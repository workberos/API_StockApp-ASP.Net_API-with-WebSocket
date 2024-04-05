namespace API_Stock.Extentions
{
    public static class HttpContextExtentions
    {
        public static int GetUserId(this HttpContext httpContext)
        {
            return httpContext.Items["UserId"] as int? ??
                throw new Exception("UserId not found int httpContext.Items");
        }
    }
}
