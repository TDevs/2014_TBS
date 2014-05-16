using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace Oas.Service.Xml
{
    public abstract class XmlObject<T>
    {
        public T Read(string fileName)
        {
            XDocument doc = XDocument.Load(fileName);

            var properties = typeof(T).GetProperties();
            T obj = Activator.CreateInstance<T>();
            foreach (XElement el in doc.Root.Elements())
            {
                var pro = properties.FirstOrDefault(t => t.Name.ToUpper().Equals(el.Name.ToString().ToUpper()));
                if (pro != null)
                {
                    string[] format = new string[] { "MM-dd-yyyy HH:mm:ss" };
                    string value = el.Value;
                    DateTime datetime;

                    if (DateTime.TryParse(value, out datetime))
                        pro.SetValue(obj, datetime, null);
                    else
                        pro.SetValue(obj, el.Value, null);


                }
            }

            return obj;
        }


    }
}
