using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using Microsoft.IdentityModel;
using System.IdentityModel.Tokens.Jwt;


namespace device_server_c_
{
    public class WsToken
    {
        public static string getTokenWs(string queryToken, string headersToken)
        {
            var token = "";
            if (Common.IsEmptyStr(queryToken) == false)
            {
                token = queryToken;
            }
            else
            {
                if (Common.IsEmptyStr(headersToken) == false)
                    token = headersToken;
            }
            return token;

        }

        public static string verifyTokenWs(string token)
        {
            if (token == "")
            {
                var response = new { token = token };
                return ApiResponse.Error(10001, "", response, "");
            }

            try
            {
                var handler = new JwtSecurityTokenHandler();
                var decoded = handler.ReadJwtToken(token);
                //var deviceid = decoded.deviceid;
                //var language = decoded.language;
                var response = new { 
                //deviceid = deviceid, 
                //language = language, 
                token = token 
                };
                return ApiResponse.Success(response, "");
            }
            catch (Exception hata)
            {
                var response = new { token = token };
                if (hata.Message.ToString() == "TokenExpiredError")
                {
                    return ApiResponse.Error(10002, "", response, "");
                }
                else
                {
                    return ApiResponse.Error(10003, "", response, "");
                }

            }

        }




    }










}