using Network;
using Network.Enums;
using Network.Extensions;
using Shared;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;

using System.Threading;

using Newtonsoft.Json;

namespace Server
{
    public class Server
    {
        private static ServerConnectionContainer serverConnectionContainer;
        static void Main(string[] args)
        {
            serverConnectionContainer = ConnectionFactory.CreateServerConnectionContainer(1234, false);

            //2. Apply optional settings.
            #region Optional settings
            serverConnectionContainer.ConnectionLost += (a, b, c) => Console.WriteLine($"{serverConnectionContainer.Count} {b.ToString()} Connection lost {a.IPRemoteEndPoint.Port}. Reason {c.ToString()}");
            serverConnectionContainer.ConnectionEstablished += connectionEstablished;
            serverConnectionContainer.AllowBluetoothConnections = false;
            serverConnectionContainer.AllowUDPConnections = true;
            serverConnectionContainer.UDPConnectionLimit = 2;
            #endregion Optional settings

            //Call start here, because we had to enable the bluetooth property at first.
            serverConnectionContainer.Start();

            // players.Add(new Player()) ;//
            /*while (true)
            {
                if (players.Count > 15)
                    players.Clear();
                
            }
            */

            players.Add(player1);/**/
            players.Add(player15);
            Thread.Sleep(Timeout.Infinite);
        }
        static List<Player> players = new List<Player>();
        static Player player = new Player();
 
        static string json;
        static int player_ = 0;
        public static string update = "";
        static Player player15 = new Player();
        static Player player1 = new Player();
        private static void connectionEstablished(Connection connection, ConnectionType type)
        {
            
            player_++;

            connection.RegisterStaticPacketHandler<PositionUpdate>((position, _) =>
            {
            player.x = position.X;
            player.y = position.Y;
            player.z = position.Z;
            player.username = position.username;


                 
                if(position.username == "bruhuser")
                {
                    player15.y = position.Y;
                player15.x = position.X;
                player15.z = position.Z;
                
                }
                if (position.username == "bruh5")
                {
                    
                    player1.y = position.Y;
                    player1.x = position.X;
                    player1.z = position.Z;
                    
            }
                //players.Add();
                // if (update.Length > 800)
                //    update = "position";
                //if (update.Contains(position.username))
                //   update.Replace(position.username, "tempdata" + new Random().Next(100, 300).ToString());

                //if (!update.Contains(position.username))
                //update += " pos_update " + position.username+" "+position.X+" "+position.Y+" "+position.Z;//)

                /*if(update.Contains(position.username))
                    {
                        string[] v = update.Split(' ');
                        for (int xj = 0; xj < update.Length; xj++)
                        {
                            if (v[xj] == "pos_update")
                            {
                                if(v[xj+1] == position.username)
                                {
                                    Console.WriteLine(v[xj + 1]);
                                    v[xj + 2] = position.X.ToString();
                                    v[xj + 3] = position.Y.ToString();
                                    v[xj + 4] = position.Z.ToString();
                                }
                                /* for (int xjj = 0; xjj < players.Count; xjj++)
                                 {
                                     if (v[xj + 1] == players[xjj].username)//players
                                     {

                                        /* players[xjj].x = Single.Parse(v[xj + 2]);
                                         players[xjj].y = Single.Parse(v[xj + 3]);
                                         players[xjj].z = Single.Parse(v[xj + 4]);*/
                /*   }
               }*/
                /*       }
                   }

               }*/
                /*  for (int xj = 0; xj < players.Count; xj++)
                  {
                      if(!players[xj].username.Contains(player.username))// position.username
                      {
                          Console.WriteLine("does memory leak happen here ");
                          players.Add(player);
                      }
                  }*/


                // if(players.Count > 300)
                /*
              int index = players.FindIndex(find => find.username == player.username);
              if(players[index].username== player.username)
              {
                  players[index].x = player.x;/*new Random().Next(8, 10) position.X*/
                /*
                  players[index].y = player.y;//position.Y
                  players[index].z = player.z;//position.Z
              }*/
                //int player_ = players.Equals//players.FindIndex(find => find.username.Contains(position.username));


                //  Console.WriteLine(player_);

                /*   for (int xj = 0; xj < players.Count; xj++)
                   {
                       if (players.username.Equals(position.username)) ;// == 
                       {*/


                /*    }
                    //Console.WriteLine("data");
                }*/
                //if (!players.user.Contains(player))
                // for (int xj = 0; xj < players.Count; xj++)//
                // 
                //   if(username.Contains(position.username))//!


                //  players.Clear();
                /* if(!players.Contains(player))


                      if(players.Count > player_+1)
                      {
                          Console.WriteLine("player object");


                  }*/
                // Console.WriteLine(players.Count);
                // joined = true;
                // 


                //  

                /* if (players.Count > 0)
                 {//1
                     if(players[1].username == player.username )
                     {
                         players[1].x = position.X;
                     players[1].y = position.Y; /*[xj]  [xj] [xj]*/
                /*    players[1].z = position.Z;/*player_ player_ player_*/
                /* }
             }*/
                /*if (players.Count > 1)
                {
                    if (players[2].username == player.username)
                    {
                        players[2].x = position.X;
                    players[2].y = position.Y; /*[xj]  [xj] [xj]*/
                /*  players[2].z = position.Z;/*player_ player_ player_*/
                /*    }
                }
                */

                Console.WriteLine($"{player.username} X:{player.x} Y:{player.y} Z:{player.z}");
                json = JsonConvert.SerializeObject(players);//s
            });

            connection.RegisterStaticPacketHandler<PositionRequest>(PositionRequestFromClient);
        }

        private static void PositionRequestFromClient(PositionRequest request, Connection connection)
        {

           
            PositionResponse response = new PositionResponse(request)
            {
                X = player.x,
                Y = player.y,
                Z = player.z,
                camX = player.camX,
                username = player.username,
                camY = player.camY,
                players = json,//players
                update = update
  
        };
           
            connection.Send(response);
        }
    }
}
