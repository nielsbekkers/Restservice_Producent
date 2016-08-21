using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.IO;

namespace ConsoleRestSvc
{

    [ServiceContract]
    public interface IService
    {
        [OperationContract]
        [WebGet(ResponseFormat=WebMessageFormat.Json,UriTemplate = "/EchoWithGet?string={s}")]
        string EchoWithGet(string s);

        [OperationContract]
        [WebInvoke(RequestFormat=WebMessageFormat.Json)]
        string EchoWithPost(Productie p);

        [OperationContract]
        [WebGet(ResponseFormat = WebMessageFormat.Json,
                UriTemplate = "/GetData?num={s}")]
        Productie GetData(string s);

        //[OperationContract]
        //[WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest,
        //        UriTemplate = "/PostPerson")]
        //Productie PostProductie(Stream s);

        //[OperationContract]
        //[WebInvoke(Method = "PUT", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest,
        //        UriTemplate = "/PostPerson?id={id}")]
        //Productie PutPerson(Stream s , string id);

        //[WebInvoke(Method = "DELETE", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest,
        //       UriTemplate = "/DeletePerson?id={id}")]
        //void DeletePerson(string id);
    }
}