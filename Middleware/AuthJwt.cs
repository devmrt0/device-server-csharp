using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using Microsoft.IdentityModel;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Http;
using System.Net.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.Text;



namespace device_server_c_
{
    public class AuthJwt
    {

        public object verifyToken(HttpContext context)
        {
            var token = context.Request.Headers["Authentication"].FirstOrDefault()?.Split(" ").Last();
            if (token == null){
                var result = new JsonResult(new { message = "Authentication Required" }) { StatusCode = StatusCodes.Status401Unauthorized };
                return result;
            }

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes("your-secret-key");
            try
            {
                tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    // set clockskew to zero so tokens expire exactly at token expiration time (instead of 5 minutes later)
                    ClockSkew = TimeSpan.Zero
                }, out SecurityToken validatedToken);

                var jwtToken = (JwtSecurityToken)validatedToken;
                var userId = int.Parse(jwtToken.Claims.First(x => x.Type == "id").Value);
                var result = new JsonResult(new { user = userId }) { StatusCode = StatusCodes.Status200OK };

                // return user id from JWT token if validation successful
                
                return result;
            }
            catch(Exception hata)
            {
                var result = new JsonResult(new { message = "Server Error:" + hata.Message }) { StatusCode = StatusCodes.Status500InternalServerError };
                return result;
            }


        }
    }
}