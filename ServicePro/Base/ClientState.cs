﻿using System.Net.Sockets;

namespace ServicePro.Base
{
    //客户端状态
    public class ClientState
	{
		public Socket socket;
		public ByteArray readBuff = new ByteArray();
		public long lastPingTime = 0;
		//玩家
		//public Player player;
	}
}
