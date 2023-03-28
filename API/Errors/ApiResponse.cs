namespace API.Errors
{
    public class ApiResponse
    {
        public int StatusCode { get; set; }
        public string Message { get; set; }

        public ApiResponse(int statusCode,string message=null)
        {
            StatusCode = statusCode;
            Message = message;
            
           
        }

        private string GetDefaultMessageForStatusCode(int statusCode)
        {
            return statusCode switch
            {
                400=>"A bad Request , You Have Made",
                401=>"Authorized  , You are not !",
                404=>"Response fount it is not , You Have Made",
                500=>"server error occured , You Have Made",
                _=>null
            };
        }


    }
}
