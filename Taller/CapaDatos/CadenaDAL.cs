using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaDatos
{
    public class CadenaDAL
    { 
        public string cadenaDato { get; set; }

        public CadenaDAL()
        {
            IConfigurationBuilder builder = new ConfigurationBuilder();
            builder.AddJsonFile(Path.Combine(Directory.GetCurrentDirectory(), "appsettings.json"));
            IConfigurationRoot root = builder.Build();
            cadenaDato = root.GetConnectionString("cn");
        }
            
    }
}
