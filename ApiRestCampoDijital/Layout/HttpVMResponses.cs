using System.Net;

namespace ApiRestCampoDijital.Layout
{
    public class HttpVMResponses<T>
    {
        public HttpStatusCode StatusCode {  get; set; }

        public T Datos { get; set; }
        public List<string> Messages {  get; set; }

        public HttpVMResponses()
        {
            this.StatusCode = HttpStatusCode.OK;
            this.Datos = default(T);
            this.Messages = new List<string>();
        }
    }
}
