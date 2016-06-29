using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using NetComm;
using System.Data.SqlClient;

namespace Host
{

    public class Persona
    {
        public string nombre { get; set; }
        public int num { get; set; }
        public string carta { get; set; }
    }
    public partial class MainDisplay : Form
    {
        List<Persona> lista = new List<Persona>(); //crea la lista para la ronda 1 con la carta de cada usuario
        public MainDisplay()
        {
            InitializeComponent();
        }

        NetComm.Host Server;
        private void MainDisplay_Load(object sender, EventArgs e)
        {
            Server = new NetComm.Host(2020); //Initialize the Server object, connection will use the 2020 port number
            Server.StartConnection(); //Starts listening for incoming clients

            //Adding event handling methods, to handle the server messages
            Server.onConnection += new NetComm.Host.onConnectionEventHandler(Server_onConnection);
            Server.lostConnection += new NetComm.Host.lostConnectionEventHandler(Server_lostConnection);
            Server.DataReceived += new NetComm.Host.DataReceivedEventHandler(Server_DataReceived);
            Cxo();
        }
        private void MainDisplay_FormClosing(object sender, FormClosingEventArgs e)
        {
            Server.CloseConnection(); //Closes all of the opened connections and stops listening
        }
        void Cxo()
        {
            Log.AppendText("Bienvenido al Berenjena Online" + Environment.NewLine);
        }
        static void RandomearMazo(ref string[] arr)
        {
            if (arr == null)
            {
                arr = new string[90];
            }

            var rand = new Random();
            for (int i = arr.Length - 1; i > 0; i--)
            {
                int n = rand.Next(i + 1);
                string temp = arr[i];
                arr[i] = arr[n];
                arr[n] = temp;               
            }
        }   
        void Server_lostConnection(string id)
        {
            if (Log.IsDisposed) return; //Fixes the invoke error
            Log.AppendText(id + " disconnected" + Environment.NewLine); //Updates the log textbox when user leaves the room
        }
        void Server_onConnection(string id)
        {
            Log.AppendText(id + " connected!" + Environment.NewLine); //Updates the log textbox when new user joined
        }
        string ConvertBytesToString(byte[] bytes)
        {
            return ASCIIEncoding.ASCII.GetString(bytes);
        }
        byte[] ConvertStringToBytes(string str)
        {
            return ASCIIEncoding.ASCII.GetBytes(str);
        }
        void GuardarClienteNum(string ID, int random,string cartax,List<Persona> lista)
        {
            if (lista.Exists(x => x.nombre == ID && x.carta == cartax) == false)
            {
                lista.Add(new Persona
                {
                    nombre = ID,
                    num = random,
                    carta = cartax});

            }
            else
            {
                lista.Find(x => x.nombre == ID && x.carta == cartax).num = random;
            }
        }
        void Mandarcartaronda1 (string clientID, int random,string pos)
        {
            switch (random)
            {
                case 1:
                    Server.SendData(clientID, ConvertStringToBytes("*oro1" + pos));
                    break;
                case 2:
                    Server.SendData(clientID, ConvertStringToBytes("*esp2" +pos));
                    break;
                case 3:
                    Server.SendData(clientID, ConvertStringToBytes("*esp3" + pos));
                    break;
                case 4:
                    Server.SendData(clientID, ConvertStringToBytes("*oro4" + pos));
                    break;
                case 5:
                    Server.SendData(clientID, ConvertStringToBytes("*copa5" + pos));
                    break;
                case 6:
                    Server.SendData(clientID, ConvertStringToBytes("*copa6" + pos));
                    break;
                case 7:
                    Server.SendData(clientID, ConvertStringToBytes("*esp7" + pos));
                    break;
                case 8:
                    Server.SendData(clientID, ConvertStringToBytes("*esp8" + pos));
                    break;
                case 9:
                    Server.SendData(clientID, ConvertStringToBytes("*basto9" + pos));
                    break;
                case 10:
                    Server.SendData(clientID, ConvertStringToBytes("*copa10" + pos));
                    break;
                case 11:
                    Server.SendData(clientID, ConvertStringToBytes("*copa11" + pos));
                    break;
                case 12:
                    Server.SendData(clientID, ConvertStringToBytes("*basto12" + pos));
                    break;

            }
        }
        void switchparaporciento (string data,string ID,List<Persona> lista)
        {
            data = data.Substring(1, 4);
            switch (data)
            {
                case "c1p1":
                    foreach (Persona lqv in lista)
                    {
                        if (lqv.nombre == ID && lqv.carta == "1")
                        {
                            foreach (string clientID in Server.Users)
                            {
                                if (clientID != ID)
                                    Server.SendData(clientID, ConvertStringToBytes("!player1" + lqv.num.ToString() + lqv.carta));
                            }
                        }
                    }

                    break;
                case "c1p4":
                    foreach (Persona lqv in lista)
                    {
                        if (lqv.nombre == ID && lqv.carta == "1")
                        {
                            foreach (string clientID in Server.Users)
                            {
                                if (clientID != ID)
                                    Server.SendData(clientID, ConvertStringToBytes("!player4" + lqv.num.ToString() + lqv.carta));
                            }
                        }
                    }

                    break;
                case "c1p5":
                    foreach (Persona lqv in lista)
                    {
                        if (lqv.nombre == ID && lqv.carta == "1")
                        {
                            foreach (string clientID in Server.Users)
                            {
                                if (clientID != ID)
                                    Server.SendData(clientID, ConvertStringToBytes("!player5" + lqv.num.ToString() + lqv.carta));
                            }
                        }
                    }

                    break;
                case "c2p4":
                    foreach (Persona lqv in lista)
                    {
                        if (lqv.nombre == ID && lqv.carta == "2")
                        {
                            foreach (string clientID in Server.Users)
                            {
                                if (clientID != ID)
                                    Server.SendData(clientID, ConvertStringToBytes("!player4" + lqv.num.ToString() + lqv.carta));
                            }
                        }
                    }

                    break;
                case "c2p5":
                    foreach (Persona lqv in lista)
                    {
                        if (lqv.nombre == ID && lqv.carta == "2")
                        {
                            foreach (string clientID in Server.Users)
                            {
                                if (clientID != ID)
                                    Server.SendData(clientID, ConvertStringToBytes("!player5" + lqv.num.ToString() + lqv.carta));
                            }
                        }
                    }

                    break;
                case "c3p4":
                    foreach (Persona lqv in lista)
                    {
                        if (lqv.nombre == ID && lqv.carta == "3")
                        {
                            foreach (string clientID in Server.Users)
                            {
                                if (clientID != ID)
                                    Server.SendData(clientID, ConvertStringToBytes("!player4" + lqv.num.ToString() + lqv.carta));
                            }
                        }
                    }

                    break;
                case "c3p5":
                    foreach (Persona lqv in lista)
                    {
                        if (lqv.nombre == ID && lqv.carta == "3")
                        {
                            foreach (string clientID in Server.Users)
                            {
                                if (clientID != ID)
                                    Server.SendData(clientID, ConvertStringToBytes("!player5" + lqv.num.ToString() + lqv.carta));
                            }
                        }
                    }

                    break;
                case "c4p4":
                    foreach (Persona lqv in lista)
                    {
                        if (lqv.nombre == ID && lqv.carta == "4")
                        {
                            foreach (string clientID in Server.Users)
                            {
                                if (clientID != ID)
                                    Server.SendData(clientID, ConvertStringToBytes("!player4" + lqv.num.ToString() + lqv.carta));
                            }
                        }
                    }

                    break;
                case "c4p5":
                    foreach (Persona lqv in lista)
                    {
                        if (lqv.nombre == ID && lqv.carta == "4")
                        {
                            foreach (string clientID in Server.Users)
                            {
                                if (clientID != ID)
                                    Server.SendData(clientID, ConvertStringToBytes("!player5" + lqv.num.ToString() + lqv.carta));
                            }
                        }
                    }

                    break;
                case "c5p4":
                    foreach (Persona lqv in lista)
                    {
                        if (lqv.nombre == ID && lqv.carta == "5")
                        {
                            foreach (string clientID in Server.Users)
                            {
                                if (clientID != ID)
                                    Server.SendData(clientID, ConvertStringToBytes("!player4" + lqv.num.ToString() + lqv.carta));
                            }
                        }
                    }

                    break;
                case "c5p5":
                    foreach (Persona lqv in lista)
                    {
                        if (lqv.nombre == ID && lqv.carta == "5")
                        {
                            foreach (string clientID in Server.Users)
                            {
                                if (clientID != ID)
                                    Server.SendData(clientID, ConvertStringToBytes("!player5" + lqv.num.ToString() + lqv.carta));
                            }
                        }
                    }

                    break;
                case "c6p4":
                    foreach (Persona lqv in lista)
                    {
                        if (lqv.nombre == ID && lqv.carta == "6")
                        {
                            foreach (string clientID in Server.Users)
                            {
                                if (clientID != ID)
                                    Server.SendData(clientID, ConvertStringToBytes("!player4" + lqv.num.ToString() + lqv.carta));
                            }
                        }
                    }

                    break;
                case "c6p5":
                    foreach (Persona lqv in lista)
                    {
                        if (lqv.nombre == ID && lqv.carta == "6")
                        {
                            foreach (string clientID in Server.Users)
                            {
                                if (clientID != ID)
                                    Server.SendData(clientID, ConvertStringToBytes("!player5" + lqv.num.ToString() + lqv.carta));
                            }
                        }
                    }

                    break;
                case "c7p4":
                    foreach (Persona lqv in lista)
                    {
                        if (lqv.nombre == ID && lqv.carta == "7")
                        {
                            foreach (string clientID in Server.Users)
                            {
                                if (clientID != ID)
                                    Server.SendData(clientID, ConvertStringToBytes("!player4" + lqv.num.ToString() + lqv.carta));
                            }
                        }
                    }

                    break;
                case "c7p5":
                    foreach (Persona lqv in lista)
                    {
                        if (lqv.nombre == ID && lqv.carta == "7")
                        {
                            foreach (string clientID in Server.Users)
                            {
                                if (clientID != ID)
                                    Server.SendData(clientID, ConvertStringToBytes("!player5" + lqv.num.ToString() + lqv.carta));
                            }
                        }
                    }

                    break;
                case "c1p2":
                    foreach (Persona lqv in lista)
                    {
                        if (lqv.nombre == ID && lqv.carta == "1")
                        {
                            foreach (string clientID in Server.Users)
                            {
                                if (clientID != ID)
                                    Server.SendData(clientID, ConvertStringToBytes("!player2" + lqv.num.ToString() + lqv.carta));
                            }
                        }
                    }
                    break;
                case "c1p3":
                    foreach (Persona lqv in lista)
                    {
                        if (lqv.nombre == ID && lqv.carta == "1")
                        {
                            foreach (string clientID in Server.Users)
                            {
                                if (clientID != ID)
                                    Server.SendData(clientID, ConvertStringToBytes("!player3" + lqv.num.ToString() + lqv.carta));
                            }
                        }
                    }
                    break;
                case "c2p1":
                    foreach (Persona lqv in lista)
                    {
                        if (lqv.nombre == ID && lqv.carta == "2")
                        {
                            foreach (string clientID in Server.Users)
                            {
                                if (clientID != ID)
                                    Server.SendData(clientID, ConvertStringToBytes("!player1" + lqv.num.ToString() + lqv.carta));
                            }
                        }
                    }
                    break;
                case "c2p2":
                    foreach (Persona lqv in lista)
                    {
                        if (lqv.nombre == ID && lqv.carta == "2")
                        {
                            foreach (string clientID in Server.Users)
                            {
                                if (clientID != ID)
                                    Server.SendData(clientID, ConvertStringToBytes("!player2" + lqv.num.ToString() + lqv.carta));
                            }
                        }
                    }
                    break;
                case "c2p3":
                    foreach (Persona lqv in lista)
                    {
                        if (lqv.nombre == ID && lqv.carta == "2")
                        {
                            foreach (string clientID in Server.Users)
                            {
                                if (clientID != ID)
                                    Server.SendData(clientID, ConvertStringToBytes("!player3" + lqv.num.ToString() + lqv.carta));
                            }
                        }
                    }
                    break;
                case "c3p1":
                    foreach (Persona lqv in lista)
                    {
                        if (lqv.nombre == ID && lqv.carta == "3")
                        {
                            foreach (string clientID in Server.Users)
                            {
                                if (clientID != ID)
                                    Server.SendData(clientID, ConvertStringToBytes("!player1" + lqv.num.ToString() + lqv.carta));
                            }
                        }
                    }
                    break;
                case "c4p1":
                    foreach (Persona lqv in lista)
                    {
                        if (lqv.nombre == ID && lqv.carta == "4")
                        {
                            foreach (string clientID in Server.Users)
                            {
                                if (clientID != ID)
                                    Server.SendData(clientID, ConvertStringToBytes("!player1" + lqv.num.ToString() + lqv.carta));
                            }
                        }
                    }
                    break;
                case "c5p1":
                    foreach (Persona lqv in lista)
                    {
                        if (lqv.nombre == ID && lqv.carta == "5")
                        {
                            foreach (string clientID in Server.Users)
                            {
                                if (clientID != ID)
                                    Server.SendData(clientID, ConvertStringToBytes("!player1" + lqv.num.ToString() + lqv.carta));
                            }
                        }
                    }
                    break;
                case "c6p1":
                    foreach (Persona lqv in lista)
                    {
                        if (lqv.nombre == ID && lqv.carta == "6")
                        {
                            foreach (string clientID in Server.Users)
                            {
                                if (clientID != ID)
                                    Server.SendData(clientID, ConvertStringToBytes("!player1" + lqv.num.ToString() + lqv.carta));
                            }
                        }
                    }
                    break;
                case "c7p1":
                    foreach (Persona lqv in lista)
                    {
                        if (lqv.nombre == ID && lqv.carta == "7")
                        {
                            foreach (string clientID in Server.Users)
                            {
                                if (clientID != ID)
                                    Server.SendData(clientID, ConvertStringToBytes("!player1" + lqv.num.ToString() + lqv.carta));
                            }
                        }
                    }
                    break;
                case "c3p2":
                    foreach (Persona lqv in lista)
                    {
                        if (lqv.nombre == ID && lqv.carta == "3")
                        {
                            foreach (string clientID in Server.Users)
                            {
                                if (clientID != ID)
                                    Server.SendData(clientID, ConvertStringToBytes("!player2" + lqv.num.ToString() + lqv.carta));
                            }
                        }
                    }
                    break;
                case "c4p2":
                    foreach (Persona lqv in lista)
                    {
                        if (lqv.nombre == ID && lqv.carta == "4")
                        {
                            foreach (string clientID in Server.Users)
                            {
                                if (clientID != ID)
                                    Server.SendData(clientID, ConvertStringToBytes("!player2" + lqv.num.ToString() + lqv.carta));
                            }
                        }
                    }
                    break;
                case "c5p2":
                    foreach (Persona lqv in lista)
                    {
                        if (lqv.nombre == ID && lqv.carta == "5")
                        {
                            foreach (string clientID in Server.Users)
                            {
                                if (clientID != ID)
                                    Server.SendData(clientID, ConvertStringToBytes("!player2" + lqv.num.ToString() + lqv.carta));
                            }
                        }
                    }
                    break;
                case "c6p2":
                    foreach (Persona lqv in lista)
                    {
                        if (lqv.nombre == ID && lqv.carta == "6")
                        {
                            foreach (string clientID in Server.Users)
                            {
                                if (clientID != ID)
                                    Server.SendData(clientID, ConvertStringToBytes("!player2" + lqv.num.ToString() + lqv.carta));
                            }
                        }
                    }
                    break;
                case "c7p2":
                    foreach (Persona lqv in lista)
                    {
                        if (lqv.nombre == ID && lqv.carta == "7")
                        {
                            foreach (string clientID in Server.Users)
                            {
                                if (clientID != ID)
                                    Server.SendData(clientID, ConvertStringToBytes("!player2" + lqv.num.ToString() + lqv.carta));
                            }
                        }
                    }
                    break;
                case "c3p3":
                    foreach (Persona lqv in lista)
                    {
                        if (lqv.nombre == ID && lqv.carta == "3")
                        {
                            foreach (string clientID in Server.Users)
                            {
                                if (clientID != ID)
                                    Server.SendData(clientID, ConvertStringToBytes("!player3" + lqv.num.ToString() + lqv.carta));
                            }
                        }
                    }
                    break;
                case "c4p3":
                    foreach (Persona lqv in lista)
                    {
                        if (lqv.nombre == ID && lqv.carta == "4")
                        {
                            foreach (string clientID in Server.Users)
                            {
                                if (clientID != ID)
                                    Server.SendData(clientID, ConvertStringToBytes("!player3" + lqv.num.ToString() + lqv.carta));
                            }
                        }
                    }
                    break;
                case "c5p3":
                    foreach (Persona lqv in lista)
                    {
                        if (lqv.nombre == ID && lqv.carta == "5")
                        {
                            foreach (string clientID in Server.Users)
                            {
                                if (clientID != ID)
                                    Server.SendData(clientID, ConvertStringToBytes("!player3" + lqv.num.ToString() + lqv.carta));
                            }
                        }
                    }
                    break;
                case "c6p3":
                    foreach (Persona lqv in lista)
                    {
                        if (lqv.nombre == ID && lqv.carta == "6")
                        {
                            foreach (string clientID in Server.Users)
                            {
                                if (clientID != ID)
                                    Server.SendData(clientID, ConvertStringToBytes("!player3" + lqv.num.ToString() + lqv.carta));
                            }
                        }
                    }
                    break;
                case "c7p3":
                    foreach (Persona lqv in lista)
                    {
                        if (lqv.nombre == ID && lqv.carta == "7")
                        {
                            foreach (string clientID in Server.Users)
                            {
                                if (clientID != ID)
                                    Server.SendData(clientID, ConvertStringToBytes("!player3" + lqv.num.ToString() + lqv.carta));
                            }
                        }
                    }
                    break;


            }
        }
        void msgGanoPierdo(byte[] Data,string ID)
        {
            foreach (string clientID in Server.Users)
            {
                if (clientID != ID)
                {
                    Server.SendData(clientID,Data);
                }
           }
        }
        void Server_DataReceived(string ID, byte[] Data)
        {
            string aux = ConvertBytesToString(Data);

            if (ConvertBytesToString(Data) == "/roll1") //ronda 1
            {
                foreach (string clientID in Server.Users)
                { Server.SendData(clientID, ConvertStringToBytes("/1"));
                    switch (ID)
                    {
                        case "Rombo":
                            Server.SendData(clientID, ConvertStringToBytes("Reparte Rombo"));
                            break;
                        case "Baco":
                            Server.SendData(clientID, ConvertStringToBytes("Reparte Baco"));
                            break;
                        case "Ocha":
                            Server.SendData(clientID, ConvertStringToBytes("Reparte  Ocha"));
                            break;
                        case "Geda":
                            Server.SendData(clientID, ConvertStringToBytes("Reparte Geda"));
                            break;
                        case "elYarol":
                            Server.SendData(clientID, ConvertStringToBytes("Reparte elYarol"));
                            break;

                    }
                }

                List<Persona> parts = new List<Persona>(); //crea la lista para la ronda 1 con la carta de cada usuario
                Log.AppendText("Sale nueva Ronda " + Environment.NewLine);
                Random r = new Random();
                foreach (string clientID in Server.Users) //manda a cada usuario una carta random
                {
                    int cxo = r.Next(1, 12);
                    GuardarClienteNum(clientID, cxo,"1", lista);
                    Server.SendData(clientID, ConvertStringToBytes("Sale nueva Ronda ")); //Sends the message to the clients
                    if (clientID == "elYarol")
                    {
                        Server.SendData("elYarol", ConvertStringToBytes("*player1" + cxo.ToString()+"1"));
                        //Mandarcartaronda1(clientID, cxo, "c1p1");

                    }
                    if (clientID == "Baco")
                    {
                        Server.SendData("Baco", ConvertStringToBytes("*player2" + cxo.ToString() + "1"));

                        //Mandarcartaronda1(clientID, cxo, "c1p2");
                    }
                    if (clientID == "Ocha")
                    {
                        Server.SendData("Ocha", ConvertStringToBytes("*player3" + cxo.ToString() + "1"));

                        //Mandarcartaronda1(clientID, cxo, "c1p3");
                    }
                    if (clientID == "Geda")
                    {
                        Server.SendData("Geda", ConvertStringToBytes("*player4" + cxo.ToString() + "1"));

                        //Mandarcartaronda1(clientID, cxo, "c1p3");
                    }
                    if (clientID == "Rombo")
                    {
                        Server.SendData("Rombo", ConvertStringToBytes("*player5" + cxo.ToString() + "1"));

                        //Mandarcartaronda1(clientID, cxo, "c1p3");
                    }
                }
                
            }
            if (ConvertBytesToString(Data) == "/roll3") //ronda 3
            {
                foreach (string clientID in Server.Users)
                {
                  switch (ID )
                    {
                        case "Rombo":
                            Server.SendData(clientID, ConvertStringToBytes("Reparte Rombo"));
                            break;
                        case "Baco":
                            Server.SendData(clientID, ConvertStringToBytes("Reparte Baco"));
                            break;
                        case "Ocha":
                            Server.SendData(clientID, ConvertStringToBytes("Reparte  Ocha"));
                            break;
                        case "Geda":
                            Server.SendData(clientID, ConvertStringToBytes("Reparte Geda"));
                            break;
                        case "elYarol":
                            Server.SendData(clientID, ConvertStringToBytes("Reparte elYarol"));
                            break;

                    }
                          
                  Server.SendData(clientID, ConvertStringToBytes("/3"));
                  
                    switch (ID)
                    {
                        case "Rombo":
                            Server.SendData(clientID, ConvertStringToBytes("Reparte Rombo"));
                            break;
                        case "Baco":
                            Server.SendData(clientID, ConvertStringToBytes("Reparte Baco"));
                            break;
                        case "Ocha":
                            Server.SendData(clientID, ConvertStringToBytes("Reparte  Ocha"));
                            break;
                        case "Geda":
                            Server.SendData(clientID, ConvertStringToBytes("Reparte Geda"));
                            break;
                        case "elYarol":
                            Server.SendData(clientID, ConvertStringToBytes("Reparte elYarol"));
                            break;

                    }
                }

              

                // MAZO PELUDO 90 cartas|| 8 onces, 7 sietes, 8 dos, 8 unos, 6 nueves, 6 ochos, 8 diez, 8 doces, 8 cuatros,  8 cincos, 7 tres, ocho seis||

                string[] mazo = { "1", "1", "1", "1","1","1","1","1",
                                  "2","2", "2", "2","2","2", "2","2",
                                  "3", "3", "3", "3","3","3","3",
                                 "4", "4", "4", "4","4","4","4","4",
                                  "5", "5", "5", "5","5","5","5","5",
                                  "6", "6", "6", "6","6","6","6","6",
                                   "7", "7", "7", "7","7","7","7",
                              "8", "8", "8", "8","8","8",
                              "9", "9", "9", "9","9","9",
                             "10", "10", "10", "10","10","10","10","10",
                             "11", "11", "11", "11","11","11","11","11",
                             "12", "12", "12", "12","12","12","12","12", };


                RandomearMazo(ref mazo);
                                               
                List<Persona> parts = new List<Persona>(); //crea la lista para la ronda 1 con la carta de cada usuario
                Log.AppendText("Sale nueva Ronda de 3" + Environment.NewLine);
                int j = 0;
                           
                for (int i = 1; i < 4; i++) //repite dar la mano
                {
                    
                    foreach (string clientID in Server.Users) //manda a cada usuario una carta random
                    {
                        
                        GuardarClienteNum(clientID, int.Parse(mazo[j]), i.ToString(), lista);
                        if (clientID == "elYarol")
                        {
                            Server.SendData("elYarol", ConvertStringToBytes("*player1" + mazo[j].ToString() + i.ToString()));
                            Log.AppendText("*player1" + mazo[j].ToString() + i.ToString() +Environment.NewLine);                      
                        }
                        if (clientID == "Baco")
                        {
                            Server.SendData("Baco", ConvertStringToBytes("*player2" + mazo[j].ToString() + i.ToString()));
                            Log.AppendText("*player2" + mazo[j].ToString() + i.ToString() + Environment.NewLine);
                        }
                        if (clientID == "Ocha")
                        {
                            Server.SendData("Ocha", ConvertStringToBytes("*player3" + mazo[j].ToString() + i.ToString()));
                            Log.AppendText("*player3" + mazo[j].ToString() + i.ToString() + Environment.NewLine);
                        }
                        if (clientID == "Geda")
                        {
                            Server.SendData("Geda", ConvertStringToBytes("*player4" + mazo[j].ToString() + i.ToString()));

                        }
                        if (clientID == "Rombo")
                        {
                            Server.SendData("Rombo", ConvertStringToBytes("*player5" + mazo[j].ToString() + i.ToString()));

                        }
                        j++;
                    }
                }
            }
            if (ConvertBytesToString(Data) == "/roll5") //ronda 3
            {
                foreach (string clientID in Server.Users)
                {
                    switch (ID)
                    {
                        case "Rombo":
                            Server.SendData(clientID, ConvertStringToBytes("Reparte Rombo"));
                            break;
                        case "Baco":
                            Server.SendData(clientID, ConvertStringToBytes("Reparte Baco"));
                            break;
                        case "Ocha":
                            Server.SendData(clientID, ConvertStringToBytes("Reparte  Ocha"));
                            break;
                        case "Geda":
                            Server.SendData(clientID, ConvertStringToBytes("Reparte Geda"));
                            break;
                        case "elYarol":
                            Server.SendData(clientID, ConvertStringToBytes("Reparte elYarol"));
                            break;

                    }

                    Server.SendData(clientID, ConvertStringToBytes("/5"));
                   
                }

                string[] mazo = { "1", "1", "1", "1",
                    "1",
                    "1",
                    "1",

                              "2",
                    "2",
                    "2",
                    "2",
                    "2",
                    "2", "2","2",
                              "3", "3", "3", "3","3","3","3",
                              "4", "4", "4", "4","4","4","4","4",
                              "5", "5", "5", "5","5","5","5","5",
                              "6", "6", "6", "6","6","6","6","6",
                              "7", "7", "7", "7","7","7","7",
                              "8", "8", "8", "8","8","8",
                              "9", "9", "9", "9","9","9",
                             "10", "10", "10", "10","10","10","10",
                             "11", "11", "11", "11","11","11","11",
                             "12", "12", "12", "12","12","12","12" };

                RandomearMazo(ref mazo);

                List<Persona> parts = new List<Persona>(); //crea la lista para la ronda 1 con la carta de cada usuario
                Log.AppendText("Sale nueva Ronda de 5" + Environment.NewLine);
                int j = 0;

                for (int i = 1; i < 6; i++) //repite dar la mano
                {

                    foreach (string clientID in Server.Users) //manda a cada usuario una carta random
                    {
                        
                        GuardarClienteNum(clientID, int.Parse(mazo[j]), i.ToString(), lista);
                        if (clientID == "elYarol")
                        {
                            Server.SendData("elYarol", ConvertStringToBytes("*player1" + mazo[j].ToString() + i.ToString()));
                            Log.AppendText("*player1" + mazo[j].ToString() + i.ToString() + Environment.NewLine);
                        }
                        if (clientID == "Baco")
                        {
                            Server.SendData("Baco", ConvertStringToBytes("*player2" + mazo[j].ToString() + i.ToString()));
                            Log.AppendText("*player2" + mazo[j].ToString() + i.ToString() + Environment.NewLine);
                        }
                        if (clientID == "Ocha")
                        {
                            Server.SendData("Ocha", ConvertStringToBytes("*player3" + mazo[j].ToString() + i.ToString()));
                            Log.AppendText("*player3" + mazo[j].ToString() + i.ToString() + Environment.NewLine);
                        }
                        if (clientID == "Geda")
                        {
                            Server.SendData("Geda", ConvertStringToBytes("*player4" + mazo[j].ToString() + i.ToString()));

                        }
                        if (clientID == "Rombo")
                        {
                            Server.SendData("Rombo", ConvertStringToBytes("*player5" + mazo[j].ToString() + i.ToString()));

                        }
                        j++;
                    }
                }
            }
            if (ConvertBytesToString(Data) == "/roll7") //ronda 3
            {
                foreach (string clientID in Server.Users)
                {
                    switch (ID)
                    {
                        case "Rombo":
                            Server.SendData(clientID, ConvertStringToBytes("Reparte Rombo"));
                            break;
                        case "Baco":
                            Server.SendData(clientID, ConvertStringToBytes("Reparte Baco"));
                            break;
                        case "Ocha":
                            Server.SendData(clientID, ConvertStringToBytes("Reparte  Ocha"));
                            break;
                        case "Geda":
                            Server.SendData(clientID, ConvertStringToBytes("Reparte Geda"));
                            break;
                        case "elYarol":
                            Server.SendData(clientID, ConvertStringToBytes("Reparte elYarol"));
                            break;

                    }
                    Server.SendData(clientID, ConvertStringToBytes("/7"));
                   
                }

                string[] mazo = { "1", "1", "1", "1",
                    "1",
                    "1",
                    "1",

                              "2",
                    "2",
                    "2",
                    "2",
                    "2",
                    "2", "2","2",
                              "3", "3", "3", "3","3","3","3",
                              "4", "4", "4", "4","4","4","4","4",
                              "5", "5", "5", "5","5","5","5","5",
                              "6", "6", "6", "6","6","6","6","6",
                              "7", "7", "7", "7","7","7","7",
                              "8", "8", "8", "8","8","8",
                              "9", "9", "9", "9","9","9",
                             "10", "10", "10", "10","10","10","10",
                             "11", "11", "11", "11","11","11","11",
                             "12", "12", "12", "12","12","12","12" };

                RandomearMazo(ref mazo);

                List<Persona> parts = new List<Persona>(); //crea la lista para la ronda 1 con la carta de cada usuario
                Log.AppendText("Sale nueva Ronda de 7" + Environment.NewLine);
                int j = 0;

                for (int i = 1; i < 8; i++) //repite dar la mano
                {

                    foreach (string clientID in Server.Users) //manda a cada usuario una carta random
                    {
                        
                        GuardarClienteNum(clientID, int.Parse(mazo[j]), i.ToString(), lista);
                        if (clientID == "elYarol")
                        {
                            Server.SendData("elYarol", ConvertStringToBytes("*player1" + mazo[j].ToString() + i.ToString()));
                            Log.AppendText("*player1" + mazo[j].ToString() + i.ToString() + Environment.NewLine);
                        }
                        if (clientID == "Baco")
                        {
                            Server.SendData("Baco", ConvertStringToBytes("*player2" + mazo[j].ToString() + i.ToString()));
                            Log.AppendText("*player2" + mazo[j].ToString() + i.ToString() + Environment.NewLine);
                        }
                        if (clientID == "Ocha")
                        {
                            Server.SendData("Ocha", ConvertStringToBytes("*player3" + mazo[j].ToString() + i.ToString()));
                            Log.AppendText("*player3" + mazo[j].ToString() + i.ToString() + Environment.NewLine);
                        }
                        if (clientID == "Geda")
                        {
                            Server.SendData("Geda", ConvertStringToBytes("*player4" + mazo[j].ToString() + i.ToString()));

                        }
                        if (clientID == "Rombo")
                        {
                            Server.SendData("Rombo", ConvertStringToBytes("*player5" + mazo[j].ToString() + i.ToString()));

                        }
                        j++;
                    }
                }
            }


            if (aux[0].ToString() == "@")
            {
                Server.Brodcast(ConvertStringToBytes("@"));
            }
            if (aux[0].ToString() == "$")
            {
                Server.Brodcast(ConvertStringToBytes("$"));
            }
            if (aux[0].ToString() == "%")
            {
                switchparaporciento(aux, ID, lista);
            }
            if (aux == "+")
            {
                Server.Brodcast(ConvertStringToBytes("+"));
            }
            if (aux == "-")
            {
                Server.Brodcast(ConvertStringToBytes("-"));
            }
            if (aux[0].ToString()=="#")
            {
                msgGanoPierdo(Data, ID);
            }
            if (aux[0].ToString() == "&")
            {
                foreach (string clientID in Server.Users)
                    Server.SendData(clientID, Data);
            }

            else
            {
                if (ConvertBytesToString(Data)[0].ToString() != "%")
                {
                    Log.AppendText(ID + " " + ConvertBytesToString(Data) + Environment.NewLine); //Updates the log when a new message arrived, converting the Data bytes to a string
                    foreach (string clientID in Server.Users)
                    {

                        Server.SendData(clientID, ConvertStringToBytes(ID +" : " + ConvertBytesToString(Data)));
                        //Server.Brodcast(ConvertStringToBytes(ID + Data));
                    }
                }
            }
                }

            }


        }
    

