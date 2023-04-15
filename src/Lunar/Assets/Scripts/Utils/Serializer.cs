using System;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace Savidiy.Utils
{
    public class Serializer<T> where T : class
    {
        public string Serialize(T obj)
        {
            string serialize;

            MemoryStream ms = new MemoryStream();
            BinaryFormatter formatter = new BinaryFormatter();
            try
            {
                formatter.Serialize(ms, obj);
                serialize = Convert.ToBase64String(ms.ToArray());
            }
            catch (SerializationException e)
            {
                Console.WriteLine("Failed to serialize. Reason: " + e.Message);
                throw;
            }
            finally
            {
                ms.Close();
            }

            return serialize;
        }

        public T Deserialize(string text)
        {
            T data;

            byte[] bytes = Convert.FromBase64String(text);
            MemoryStream ms = new MemoryStream(bytes);

            try
            {
                BinaryFormatter formatter = new BinaryFormatter();
                data = (T) formatter.Deserialize(ms);
            }
            catch (SerializationException e)
            {
                Console.WriteLine("Failed to deserialize. Reason: " + e.Message);
                throw;
            }
            finally
            {
                ms.Close();
            }

            return data;
        }
    }
}