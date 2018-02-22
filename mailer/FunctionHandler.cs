using System;
using System.Text;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
namespace Function
{
    public class FunctionHandler
    {
        public void Handle(string input) {
            if (!string.IsNullOrWhiteSpace(input))
            {
                APIResponse results = null;
                Guid _corelationGuid = Guid.NewGuid();
                AppDetails appModel=null;
                try
                {
                    appModel = JsonConvert.DeserializeObject<AppDetails>(input);
                }
                catch (Exception ex)
                {
                   // Console.WriteLine("The input is not a valid application model " + ex.Message);
                    results = new APIResponse
                    {

                        CorelationID = _corelationGuid.ToString(),
                        Status = $"Unable to deserialize JSON{ex.Message}",
                        StatusCode = HttpStatusCode.BadRequest

                    };
                    Console.WriteLine(JsonConvert.SerializeObject(results));
                    return;
                    
                }
                if (appModel.ToAddress == null)
                {
                    results = new APIResponse
                    {

                        CorelationID = _corelationGuid.ToString(),
                        Status = "To Address is empty",
                        StatusCode = HttpStatusCode.BadRequest

                    };
                    Console.WriteLine(JsonConvert.SerializeObject(results));
                    return;
                }
                if (appModel.ToAddress.Count==0)
                {
                    results = new APIResponse
                    {

                        CorelationID = _corelationGuid.ToString(),
                        Status = "To Address is empty",
                        StatusCode = HttpStatusCode.BadRequest

                    };
                    Console.WriteLine(JsonConvert.SerializeObject(results));
                    return;
                }
                OutgoingMailModel mailModel = new OutgoingMailModel {
                    FromAddress = Props.FromAddress,
                    ToAddress = appModel.ToAddress,
                    Subject = $"{appModel.AlertType} in application {appModel.AppName} with app id {appModel.AppID}",
                    Body = appModel.Message

                };
                HttpClient httpClient = new HttpClient();
                try
                {
                    StringContent mailContent= new StringContent(JsonConvert.SerializeObject(mailModel), Encoding.UTF8,"application/json");
                   var response =  httpClient.PostAsync(Props.MailServiceUrl, mailContent).Result;
                    response.EnsureSuccessStatusCode();
                    results = new APIResponse
                    {

                        CorelationID = _corelationGuid.ToString(),
                        Status = "mail sent successfully",
                        StatusCode = HttpStatusCode.OK

                    };
                    Console.WriteLine(JsonConvert.SerializeObject(results));
                    return;
                }
                catch (Exception ex)
                {
                    results = new APIResponse
                    {

                        CorelationID = _corelationGuid.ToString(),
                        Status = $"Error sendin email {ex.Message}",
                        StatusCode = HttpStatusCode.BadRequest

                    };
                    Console.WriteLine(JsonConvert.SerializeObject(results));
                    
                    return;
                }
            }
            
        }
    }
}
