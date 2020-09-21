using Microsoft.WindowsAzure.MobileServices;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Threading.Tasks;

namespace MovieMatcher.Model
{
    public class Person
    {
        public string Id { get; set; }
        public string Nickname { get; set; }
        public string Emailaddress { get; set; }
    }
}
