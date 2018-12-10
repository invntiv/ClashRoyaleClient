using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class ClientHandleData
{
    private static ByteBuffer playerBuffer;
    public delegate void Packet_( byte[] data);
    public static Dictionary<int, Packet_> packetListener;
    private static int pLength;

    public static void InitializePacketListener()
    {
        packetListener = new Dictionary<int, Packet_>();
        packetListener.Add((int)ServerPackages.SWelcomeMsg, HandleWelcomeMsg);
    }

    public static void HandleData(byte[] data)
    {
        // Copying our packet information into a temporary array to edit anmd peek it
        byte[] buffer = (byte[])data.Clone();

        // Checking if the connected player which sent the package has an instance of the byte[]buffer
        // in order to read out the information of the byte[]buffer
        if (playerBuffer == null)
        {
            // If there is no instance, then create one
            playerBuffer = new ByteBuffer();
        }

        // Reading out the package from the player in order to check which package it actually is
        playerBuffer.WriteBytes(buffer);

        // Checking if the received package is empty. If so, do not continue executing
        if (playerBuffer.Count() == 0)
        {
            playerBuffer.Clear();
            return;
        }

        // Checking if the package actually contains information
        if (playerBuffer.Length() >= 4)
        {
            // if so, then read out full package length
            pLength = playerBuffer.ReadInteger(false);

            // if there is no package or package is invalid then close this method
            if (pLength <= 0)
            {
                playerBuffer.Clear();
                return;
            }
        }

        while (pLength > 0 & pLength <= playerBuffer.Length() - 4)
        {
            if (pLength <= playerBuffer.Length() - 4)
            {
                playerBuffer.ReadInteger();
                data = playerBuffer.ReadBytes(pLength);
                // CHECKING IF WE ARE LISTENING TO THIS SPECIFIC PACKAGE
                HandleDataPackages(data);
            }

            pLength = 0;
            if (playerBuffer.Length() >= 4)
            {
                pLength = playerBuffer.ReadInteger(false);
                // if there is no package or package is invalid then close this method
                if (pLength <= 0)
                {
                    playerBuffer.Clear();
                    return;
                }
            }

            if (pLength <= 1)
            {
                playerBuffer.Clear();
            }
        }
    }

    private static void HandleDataPackages(byte[] data)

    {
        Packet_ packet;
        ByteBuffer buffer = new ByteBuffer();
        buffer.WriteBytes(data);
        int packageID = buffer.ReadInteger();

        if (packetListener.TryGetValue(packageID, out packet))
        {
            packet.Invoke(data);
        }
    }

    private static void HandleWelcomeMsg(byte[] data)
    {
        ByteBuffer buffer = new ByteBuffer();
        buffer.WriteBytes(data);
        int packageID = buffer.ReadInteger();
        String msg = buffer.ReadString();

        Debug.Log(msg);

        ClientTCP.PACKAGE_ThankYou();
    }
}   

