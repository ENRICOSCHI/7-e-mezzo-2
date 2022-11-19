using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _7_e_mezzo_2
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Iniziare la partita: si/no");//scrive la carta su console 
            string sceltaIniziale = Console.ReadLine();
            while (sceltaIniziale == "si")
            {
                Carte Mazzo = new Carte();
                Mazzo.SvuotaMazzoVuoto();
                Mazzo.resetSomma();//resetto il punteggio
                Mazzo.random();//richiama il random per metterlo a caso e pescare la carta
                Console.WriteLine(Mazzo.toString());//scrive la carta su console 
                Console.WriteLine("Pescare? S o N");
                string scelta = Console.ReadLine();//input, si può fare solo così
                string scelta1 = "S";//così non va in crisi o non entra nell'if NO
                while (scelta == "S" || scelta == "s")//turno giocatore
                {
                    Mazzo.random();//richiama il random per metterlo a caso e pescare la carta
                    Console.WriteLine(Mazzo.toString());//scrive la carta su console 
                    if (Mazzo.RitornaSomma() > 7.5)
                    {
                        Console.WriteLine("Hai perso, hai sforato il punteggio, hai fatto: " + Mazzo.RitornaSomma());
                        Console.WriteLine("Iniziare la partita: si/no");//scrive la carta su console
                        sceltaIniziale =Console.ReadLine();//per finire il programma
                        break;
                    }
                    Console.WriteLine("Pescare? S o N");
                    scelta1 = Console.ReadLine();//input, si può fare solo così
                    if (scelta1 == "N" || scelta1 == "n" && Mazzo.RitornaSomma() <= 7.5)
                    {
                        Mazzo.SommaGiocatore();//salvataggio punteggio
                        break;
                    }

                }//turno computer
                if (scelta == "N" || scelta == "n" || scelta1 == "N" || scelta1 == "n" && Mazzo.SommaGiocatore() <= 7.5)
                {
                    if (Mazzo.SommaGiocatore() <= 7.5)//così quando esce dal while dopo aver perso non entra qui
                    {
                        Console.WriteLine("Ora tocca al computer");
                        Mazzo.resetSomma();//resetto il punteggio
                        Console.WriteLine("Premi invio per continuare");
                        Console.ReadLine();//per farlo scendere lentamente
                        Mazzo.random();//richiama il random per metterlo a caso
                        Console.WriteLine(Mazzo.toString());//scrive la carta su console
                        while (Mazzo.RitornaSomma() < 5.5 || Mazzo.SommaGiocatore() > Mazzo.RitornaSomma())
                        {
                            Console.WriteLine("Premi invio per continuare");
                            Console.ReadLine();//per farlo scendere lentamente
                            Mazzo.random();//richiama il random per metterlo a caso
                            Console.WriteLine(Mazzo.toString());//scrive la carta su console
                            if (Mazzo.RitornaSomma() == Mazzo.SommaGiocatore())
                            {
                                Console.WriteLine("Ha vinto il computer");
                                break;
                            }
                        }
                        /*if (Mazzo.RitornaSomma() == Mazzo.SommaGiocatore())
                        {
                            Console.WriteLine("Ha vinto il computer");
                            Console.WriteLine("Punteggi, TU: " + Mazzo.SommaGiocatore() + " Computer: " + Mazzo.RitornaSomma());
                            Console.WriteLine("Iniziare la partita: s/n");//scrive la carta su console
                            Mazzo.SvuotaMazzoVuoto();
                            sceltaIniziale = Console.ReadLine();//per finire il programma
                        }*/
                        if (Mazzo.RitornaSomma() > 7.5)
                        {
                            Console.WriteLine("Hai vinto!!");
                        }
                        else if (Mazzo.RitornaSomma() > Mazzo.SommaGiocatore())
                        {
                            Console.WriteLine("Ha vinto il computer");
                        }
                        Console.WriteLine("Punteggi, TU: " + Mazzo.SommaGiocatore() + " Computer: " + Mazzo.RitornaSomma());
                        Console.WriteLine("Iniziare la partita: si/no");//scrive la carta su console
                        Mazzo.SvuotaMazzoVuoto();
                        sceltaIniziale = Console.ReadLine();//per finire il programma
                    }
                }
            }
            Console.WriteLine("premere invio per terminare l'applicazione");
            Console.ReadLine();               
        }
        public class Carte
        {
            Random rd = new Random();
            private string numero;
            private string segno;
            private double somma;
            private double sommaDelGiocatore;
            private int x;//fa schifo fare una roba del genere ma mi tocca
            double ncal;//per il calcolo che devo aggiungere 1
            private string[] arrnumero = {"Asso", "Due", "Tre", "Quattro", "Cinque", "Sei", "Sette", "Fante", "Cavallo", "Re"};
            private string[] arrsegno = {"Bastoni", "Denari", "Spade", "Coppe"};
            private string[,] mazzoVuoto = new string[40,40];//mazzo vuoto in cui inserire le carte
            public void random()
            {
               bool esci = false;
                while (esci == false)
                {
                    int num = rd.Next(0, 9);
                    int seg = rd.Next(0, 3);
                    if (mazzoVuoto[num, seg] == null)//se non c'è nel mazzo vuoto
                    {
                        mazzoVuoto[num, seg] = numero;
                        numero = arrnumero[num]; //numero
                        num += 1;
                        ncal = num;
                        segno = arrsegno[seg];//segno
                        esci = true;
                    }
                }
            }
            public string toString()
            {
                return numero + " di " + segno + " punteggio totale= " + calcoloPunteggio(); ;
            }
            private double calcoloPunteggio()
            {
                if (numero == "Fante" || numero == "Cavallo" || numero == "Re")
                    somma += 0.5;
                else
                    somma += ncal;
                return somma;
            }
            public double RitornaSomma()//giro del cazzo per non rendere calcoloPunteggio Public
            {
                return somma;
            }
            public void resetSomma()
            {
                somma = 0;
            }
            public double SommaGiocatore()
            {
                if(x==0)//fa schifo fare una roba del genere ma mi tocca
                    sommaDelGiocatore = somma;
                x++;
                return sommaDelGiocatore;
                
            }
            public void SvuotaMazzoVuoto()
            {
                for(int i = 0; i < 40; i++)
                {
                    for(int j = 0; j < 40; j++)
                    {
                        mazzoVuoto[i, j] = null;
                    }
                }
            }
        }
    }
}