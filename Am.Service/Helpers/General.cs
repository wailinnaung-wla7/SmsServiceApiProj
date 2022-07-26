using Am.Infrastructure.Dto.SmsService;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Am.Service.Helpers
{
    public static class General
    {
        public static string GenerateUniqueCode(string Id)
        {
            return Id + DateTime.UtcNow.ToString("yyyyMMddHHmmss");
        }
        public static int GenerateRandomNumber()
        {
            Random rnd = new Random();
            int random = rnd.Next(40, 101);
            return random;
        }
        public static async Task<dynamic> CallServiceClient(Object Obj)
        {
            HttpClientHandler clientHandler = new HttpClientHandler();
            clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };

            using (var httpClient = new HttpClient(clientHandler))
            {
                StringContent content = new StringContent(JsonConvert.SerializeObject(Obj), Encoding.UTF8, "application/json");

                using (var response = await httpClient.PostAsync("https://localhost:7286/api/v1/SmsService/SendSmsFromThirdParty", content))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    var Object = JsonConvert.DeserializeObject<dynamic>(apiResponse);
                    return Object;
                }
            }
        }

        public static List<SMSTransactionCreationDTO> PopulateSmsTransaction(SendBulkSmsRequestDTO requestModel, List<string> FailedNumbers, string ServiceCode)
        {
            var responselst = new List<SMSTransactionCreationDTO>();
            foreach (var model in requestModel.PhoneNumbers)
            {
                var Transaction = new SMSTransactionCreationDTO
                {
                    ServiceCode = ServiceCode,
                    Message = requestModel.Body,
                    PhoneNumber = model,
                    Status = FailedNumbers.Any(a=>model.Contains(a)) ? General.StatusFail : General.StatusSuccess
                };
                responselst.Add(Transaction);
            }
            return responselst;
        }

        public static dynamic Cast(dynamic source, Type dest)
        {
            return Convert.ChangeType(source, dest);
        }
        public static string StatusSuccess = "Success";
        public static string StatusFail = "Fail";
    }
}
