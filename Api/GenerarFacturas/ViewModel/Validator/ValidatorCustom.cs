using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using ViewModel.Security;

namespace ViewModel.Validator
{
    public class ValidatorCustom<T> where T : class
    {
        public static Response Validator(T request)
        {
            Response response = new()
            {
                Success = true,
                Message = "Sin Error"
            };
            if (request.GetType() == typeof(T))
            {
                var item = request.GetType().GetProperties();
                response = ValidateProperties(item, request);
            }

            return response;
        }
        private static Response ValidateProperties(PropertyInfo[] propertyInfo, object request)
        {
            foreach (PropertyInfo item in propertyInfo)
            {


                var propertyName = item.PropertyType.Name.ToUpper();
                var property = item.PropertyType;
                var propertyValue = item.GetValue(request);
                if (propertyName.Equals("STRING"))
                {
                    var attributes = item.GetCustomAttributes(true);
                    foreach (object attrs in attributes)
                    {
                        ValidatorAttribute validator = attrs as ValidatorAttribute;
                        if (validator != null)
                        {
                            if (validator.Required)
                            {
                                string valor = (string)propertyValue;
                                if (string.IsNullOrWhiteSpace(valor))
                                {
                                    return new Response { Success = false, Message = string.Format(MessageUtil.MessageField, item.Name) };
                                }
                                else
                                {
                                    if (validator.Length > 0)
                                    {
                                        if (valor.Length < validator.Length)
                                            return new Response { Success = false, Message = string.Format(MessageUtil.MessageLength, item.Name, valor.Length, validator.Length) };
                                    }

                                }

                            }
                        }

                    }
                }
                else if (propertyName.Equals("Boolean")) { }
                else if (propertyName.Equals("INT32")) { }
                else if (propertyName.Equals("Single")) { }
                else if (propertyName.Equals("Double")) { }
                else if (propertyName.Equals("Decimal")) { }
                else if (propertyName.Equals("Byte")) { }
               
            }


            return new Response { Success = true, Message = "Modelo valido" };
        }
    }
}
