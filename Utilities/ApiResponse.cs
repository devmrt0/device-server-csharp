using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

namespace device_server_c_
{
    public class ApiResponse
    {


        public static string Error(int code, string detail, object data, string requestid)
        {
            var msg = "";
            if (Common.IsEmptyStr(detail) == true)
            {
                msg = ErrorListGet(code);
            }
            else
            {
                msg = detail;
            }
            if (Common.IsEmptyStr(data.ToString()) == false)
            {
                if (Common.IsEmptyStr(requestid) == false)
                {
                    var response = new { status = -1, code = code, detail = msg, data = data, requestid = requestid };
                    return JsonConvert.SerializeObject(response);
                }
                else
                {
                    var response = new { status = -1, code = code, detail = msg, data = data };
                    return JsonConvert.SerializeObject(response);

                }

            }
            else
            {
                if (Common.IsEmptyStr(requestid) == false)
                {
                    var response = new { status = -1, code = code, detail = msg, requestid = requestid };
                    return JsonConvert.SerializeObject(response);

                }
                else
                {

                    var response = new { status = -1, code = code, detail = msg };
                    return JsonConvert.SerializeObject(response);

                }

            }

        }

        public static object Error2(int code, string detail, object data, string requestid)
        {
            var msg = "";
            if (Common.IsEmptyStr(detail) == true)
            {
                msg = ErrorListGet(code);
            }
            else
            {
                msg = detail;
            }
            if (Common.IsEmptyStr(data.ToString()) == false)
            {
                if (Common.IsEmptyStr(requestid) == false)
                {
                    var response = new { status = -1, code = code, detail = msg, data = data, requestid = requestid };
                    return response;
                }
                else
                {
                    var response = new { status = -1, code = code, detail = msg, data = data };
                    return response;

                }

            }
            else
            {
                if (Common.IsEmptyStr(requestid) == false)
                {
                    var response = new { status = -1, code = code, detail = msg, requestid = requestid };
                    return response;

                }
                else
                {

                    var response = new { status = -1, code = code, detail = msg };
                    return response;

                }

            }

        }

        public static string Warning(int code, string detail)
        {
            var msg = "";
            if (Common.IsEmptyStr(detail))
            {
                msg = ErrorListGet(code);
            }
            else
            {
                msg = detail;
            }

            var response = new { status = 0, code = code, detail = msg };
            return JsonConvert.SerializeObject(response);

        }

        public static object Warning2(int code, string detail)
        {
            var msg = "";
            if (Common.IsEmptyStr(detail))
            {
                msg = ErrorListGet(code);
            }
            else
            {
                msg = detail;
            }

            var response = new { status = 0, code = code, detail = msg };
            return response;

        }


        public static string Success(object data, string requestid)
        {
            if (Common.IsEmptyStr(data.ToString()) == false)
            {

                if (Common.IsEmptyStr(requestid) == false)
                {
                    var response = new { status = 1, code = 0, data = data, requestid = requestid };
                    return JsonConvert.SerializeObject(response);

                }
                else
                {
                    var response = new { status = 1, code = 0, data = data };
                    return JsonConvert.SerializeObject(response);

                }


            }
            else
            {
                if (Common.IsEmptyStr(requestid) == false)
                {

                    var response = new { status = 1, code = 0, requestid = requestid };
                    return JsonConvert.SerializeObject(response);


                }
                else
                {
                    var response = new { status = 1, code = 0 };
                    return JsonConvert.SerializeObject(response);

                }

            }

        }

        public static object Success2(object data, string requestid)
        {
            if (Common.IsEmptyStr(data.ToString()) == false)
            {

                if (Common.IsEmptyStr(requestid) == false)
                {
                    var response = new { status = 1, code = 0, data = data, requestid = requestid };
                    return response;

                }
                else
                {
                    var response = new { status = 1, code = 0, data = data };
                    return response;

                }


            }
            else
            {
                if (Common.IsEmptyStr(requestid) == false)
                {

                    var response = new { status = 1, code = 0, requestid = requestid };
                    return response;


                }
                else
                {
                    var response = new { status = 1, code = 0 };
                    return response;

                }

            }

        }


        public static string ErrorListGet(int code)
        {
            var errors = new Dictionary<int, string>();
            errors.Add(10001, "token must have a value for auth.");
            errors.Add(10002, "expired.");
            errors.Add(10003, "Invalid Token.");
            errors.Add(10004, "başka bir cihaz bağlanmaya çalıştığı için bağlantı kapatıldı.");
            errors.Add(10005, "this device already connected.");
            errors.Add(10006, "cihaz bağlı olmadığı için komut cihaza gönderilemedi.");
            errors.Add(10007, "no data found.");
            errors.Add(10008, "Token Not Found in device list.");
            errors.Add(10009, "Invalid requestid.");
            errors.Add(10010, "Invalid device response.");
            errors.Add(10011, "Command timeout response.");
            errors.Add(10012, "invalid method.");
            errors.Add(10013, "Schema Validaiton Error.");
            errors.Add(10014, "Json body must have value.");
            errors.Add(20015, "Dosya bulunamadı.");

            return errors[code];

        }
    }










}