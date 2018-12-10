using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class ByteBuffer : IDisposable
{
    private List<Byte> Buff;
    private byte[] readBuff;
    private int readPos;
    private bool buffUpdated = false;

    public ByteBuffer()
    {
        Buff = new List<byte>();
        readPos = 0;

    }

    public int GetReadPos()
    {
        return readPos;
    }
    public byte[] ToArray()
    {
        return Buff.ToArray();
    }
    public int Count()
    {
        return Buff.Count;
    }
    public int Length()
    {
        return Count() - readPos;
    }
    public void Clear()
    {
        Buff.Clear();
        readPos = 0;
    }

    public void WriteByte(byte input)
    {
        Buff.Add(input);
        buffUpdated = true;
    }
    public void WriteBytes(byte[] input)
    {
        Buff.AddRange(input);
        buffUpdated = true;
    }
    public void WriteShort(short input)
    {
        Buff.AddRange(BitConverter.GetBytes(input));
        buffUpdated = true;
    }
    public void WriteInteger(int input)
    {
        Buff.AddRange(BitConverter.GetBytes(input));
        buffUpdated = true;
    }
    public void WriteLong(long input)
    {
        Buff.AddRange(BitConverter.GetBytes(input));
        buffUpdated = true;
    }
    public void WriteFloat(float input)
    {
        Buff.AddRange(BitConverter.GetBytes(input));
        buffUpdated = true;
    }
    public void WriteString(string input)
    {
        Buff.AddRange(BitConverter.GetBytes(input.Length));
        Buff.AddRange(Encoding.ASCII.GetBytes(input));
        buffUpdated = true;
    }
    public void WriteVector3(Vector3 input)
    {
        byte[] vectorArray = new byte[sizeof(float) * 3];

        Buffer.BlockCopy(BitConverter.GetBytes(input.x), 0, vectorArray, (0 * sizeof(float)), sizeof(float));
        Buffer.BlockCopy(BitConverter.GetBytes(input.y), 0, vectorArray, (1 * sizeof(float)), sizeof(float));
        Buffer.BlockCopy(BitConverter.GetBytes(input.z), 0, vectorArray, (2 * sizeof(float)), sizeof(float));

        Buff.AddRange(vectorArray);
        buffUpdated = true;
    }
    public void WriteQuaternion(Quaternion input)
    {
        byte[] vectorArray = new byte[sizeof(float) * 4];

        Buffer.BlockCopy(BitConverter.GetBytes(input.x), 0, vectorArray, (0 * sizeof(float)), sizeof(float));
        Buffer.BlockCopy(BitConverter.GetBytes(input.y), 0, vectorArray, (1 * sizeof(float)), sizeof(float));
        Buffer.BlockCopy(BitConverter.GetBytes(input.z), 0, vectorArray, (2 * sizeof(float)), sizeof(float));
        Buffer.BlockCopy(BitConverter.GetBytes(input.w), 0, vectorArray, (2 * sizeof(float)), sizeof(float));

        Buff.AddRange(vectorArray);
        buffUpdated = true;
    }

    public byte ReadByte(bool Peek = true)
    {
        if (Buff.Count > readPos)
        {
            if (buffUpdated)
            {
                readBuff = Buff.ToArray();
                buffUpdated = false;
            }

            byte value = readBuff[readPos];
            if (Peek & Buff.Count > readPos)
            {
                readPos += 1;
            }
            return value;
        }
        else
        {
            throw new Exception("BYTE ERROR");
        }
    }
    public byte[] ReadBytes(int Length, bool Peek = true)
    {
        if (Buff.Count > readPos)
        {
            if (buffUpdated)
            {
                readBuff = Buff.ToArray();
                buffUpdated = false;
            }

            byte[] value = Buff.GetRange(readPos, Length).ToArray();
            if (Peek)
            {
                readPos += Length;
            }
            return value;
        }
        else
        {
            throw new Exception("BYTE[] ERROR");
        }
    }
    public short ReadShort(bool Peek = true)
    {
        if (Buff.Count > readPos)
        {
            if (buffUpdated)
            {
                readBuff = Buff.ToArray();
                buffUpdated = false;
            }

            short value = BitConverter.ToInt16(readBuff, readPos);
            if (Peek & Buff.Count > readPos)
            {
                readPos += 2;
            }
            return value;
        }
        else
        {
            throw new Exception("BYTE ERROR");
        }
    }
    public int ReadInteger(bool Peek = true)
    {
        if (Buff.Count > readPos)
        {
            if (buffUpdated)
            {
                readBuff = Buff.ToArray();
                buffUpdated = false;
            }

            int value = BitConverter.ToInt32(readBuff, readPos);
            if (Peek & Buff.Count > readPos)
            {
                readPos += 4;
            }
            return value;
        }
        else
        {
            throw new Exception("INTEGER ERROR");
        }
    }
    public long ReadLong(bool Peek = true)
    {
        if (Buff.Count > readPos)
        {
            if (buffUpdated)
            {
                readBuff = Buff.ToArray();
                buffUpdated = false;
            }

            long value = BitConverter.ToInt64(readBuff, readPos);
            if (Peek & Buff.Count > readPos)
            {
                readPos += 8;
            }
            return value;
        }
        else
        {
            throw new Exception("LONG ERROR");
        }
    }
    public float ReadFloat(bool Peek = true)
    {
        if (Buff.Count > readPos)
        {
            if (buffUpdated)
            {
                readBuff = Buff.ToArray();
                buffUpdated = false;
            }

            float value = BitConverter.ToSingle(readBuff, readPos);
            if (Peek & Buff.Count > readPos)
            {
                readPos += 4;
            }
            return value;
        }
        else
        {
            throw new Exception("FLOAT ERROR");
        }
    }
    public string ReadString(bool Peek = true)
    {
        int length = ReadInteger(true);

        if (buffUpdated)
            {
                readBuff = Buff.ToArray();
                buffUpdated = false;
            }

        string value = Encoding.ASCII.GetString(readBuff, readPos, length);
        if (Peek & Buff.Count > readPos)
        {
            readPos += length;
        }
            return value;
        }
    public Vector3 ReadVector3(bool Peek = true)
    {
       
      if (buffUpdated)
      {
            readBuff = Buff.ToArray();
            buffUpdated = false;
      }

       byte[] value = Buff.GetRange(readPos, sizeof(float) * 3).ToArray();
       Vector3 vector3;
       vector3.x = BitConverter.ToSingle(value, 0 * sizeof(float));
       vector3.y = BitConverter.ToSingle(value, 1 * sizeof(float));
       vector3.z = BitConverter.ToSingle(value, 2 * sizeof(float));

        if (Peek)
       {
            readPos += (sizeof(float) * 3);
        }
        return vector3;
     
    }
    public Quaternion ReadQuaternion(bool Peek = true)
    {

        if (buffUpdated)
        {
            readBuff = Buff.ToArray();
            buffUpdated = false;
        }

        byte[] value = Buff.GetRange(readPos, sizeof(float) * 4).ToArray();
        Quaternion quaternion;
        quaternion.x = BitConverter.ToSingle(value, 0 * sizeof(float));
        quaternion.y = BitConverter.ToSingle(value, 1 * sizeof(float));
        quaternion.z = BitConverter.ToSingle(value, 2 * sizeof(float));
        quaternion.w = BitConverter.ToSingle(value, 3 * sizeof(float));

        if (Peek)
        {
            readPos += (sizeof(float) * 3);
        }
        return quaternion;

    }

    private bool disposedValue = false;
    protected virtual void Dispose(bool disposing)
    {
        if (!disposing)
        {
            if (disposing)
            {
                Buff.Clear();
                readPos = 0;
            }
            disposedValue = true;
        }
    }
    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }
}
