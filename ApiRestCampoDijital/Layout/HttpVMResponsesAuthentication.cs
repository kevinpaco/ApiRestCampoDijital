using System.Net;

namespace ApiRestCampoDijital.Layout
{
    public class HttpVMResponsesAuthentication<T>
    {
        public HttpStatusCode StatusCode { get; set; }

        public string Token { get; set; }
        public T Datos {  get; set; }
        public List<string> Messages { get; set; }

        public HttpVMResponsesAuthentication()
        {
            this.StatusCode = HttpStatusCode.OK;
            this.Token =string.Empty;
            this.Datos = default(T);
            this.Messages = new List<string>();
        }
    }
}
