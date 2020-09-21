using Microsoft.WindowsAzure.MobileServices;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace MovieMatcher.Model
{
    public class Database
    {
        public static MobileServiceClient client = new MobileServiceClient("https://moviematch5.azurewebsites.net");
        
        public async static Task<bool> Save<T>(T value) 
        {
            try
            {
                await client.GetTable<T>().InsertAsync(value);
                return true;
            }
            catch (MobileServiceInvalidOperationException msioe)
            {
                var response = await msioe.Response.Content.ReadAsStringAsync();
                Console.WriteLine("Mobile service invalid operation: ");
                Console.WriteLine(response);
                return false;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return false;
            }
        }

        public static async Task<List<T>> LoadAll<T>()
        {
            return await client.GetTable<T>().ToListAsync();
        }

        public static async Task<List<Person>> getUser(Person person)
        {
            IMobileServiceTable<Person> personTable = client.GetTable<Person>();
            return await personTable.Where(e => e.Emailaddress == person.Emailaddress).ToListAsync();
        }
    }
}
