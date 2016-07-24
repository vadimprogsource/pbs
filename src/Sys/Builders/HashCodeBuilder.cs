using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Sys.Builders
{
    public class HashCodeBuilder
    {
        private readonly List<byte> buffer = new List<byte>();


        public HashCodeBuilder Append(string value)
        {
            buffer.AddRange(Encoding.ASCII.GetBytes(value));
            return this;
        }


        public HashCodeBuilder Append(byte value)
        {
            buffer.AddRange(BitConverter.GetBytes(value));
            return this;
        }


        public HashCodeBuilder Append(char value)
        {
            buffer.AddRange(BitConverter.GetBytes(value));
            return this;
        }


        public HashCodeBuilder Append(short value)
        {
            buffer.AddRange(BitConverter.GetBytes(value));
            return this;
        }


        public HashCodeBuilder Append(int value)
        {
            buffer.AddRange(BitConverter.GetBytes(value));
            return this;
        }

        public HashCodeBuilder Append(long value)
        {
            buffer.AddRange(BitConverter.GetBytes(value));
            return this;
        }



        public HashCodeBuilder Append(TimeSpan value)
        {
            buffer.AddRange(BitConverter.GetBytes(value.Ticks));
            return this;
        }


        public HashCodeBuilder Append(DateTime value)
        {
            buffer.AddRange(BitConverter.GetBytes(value.Ticks));
            return this;
        }


        public byte[] ToMD5()
        {
            return MD5.Create().ComputeHash(buffer.ToArray());
        }


        public Guid ToGuid()
        {
           return  new Guid(ToMD5());
        }

        public override int GetHashCode()
        {
            int hc = 1024;

            foreach (byte x in ToMD5())
            {
                hc ^= x;
            }


            return hc;
        }
    }
}
