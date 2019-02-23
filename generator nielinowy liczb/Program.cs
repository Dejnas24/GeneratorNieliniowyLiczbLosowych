using System;
using System.IO;

namespace generator_nielinowy_liczb
{
    internal class Program
    {
        public static bool czyPierwsza(int p)
        {
            if (p < 2)
            {
                return false;
            }

            for (int i = 2; i * i <= p; i++)
            {
                if (p % i == 0)
                {
                    return false;
                }
            }

            return true;
        }

        private static void Main(string[] args)
        {
            // bool wykonuj = false;
            Console.BufferWidth = 5000;
            ConsoleKeyInfo keyInfo = new ConsoleKeyInfo();
            do
            {
                Console.WriteLine("Wykonaj nastepującą operacje:");
                Console.WriteLine("Oblicz i zapisz - W;\nWyswietl raport - R;\nWyczyć raport - D;\nZakończ program - Q;");
                Console.Write("Podaj operacje:");
                string operacja = Console.ReadLine();
                Console.Clear();
                switch (operacja.ToUpper())
                {
                    case "W":

                        try
                        {
                            Console.Write("Podaj liczbe pierwszą z reszta dzielenia mod: ");
                            int mod = Int32.Parse(Console.ReadLine());

                            Console.Clear();
                            do
                            {
                                if (czyPierwsza(mod) == true)
                                {
                                    int[] x = new int[mod];

                                    int[] xod = new int[mod];
                                    int[] odwrotnoscWynik = new int[mod];
                                    int[] X = new int[mod * mod];
                                    Console.Write("Podaj wartość a: ");
                                    int a = Int32.Parse(Console.ReadLine());
                                    Console.Clear();
                                    Console.Write("Podaj wartość b: ");
                                    int b = Int32.Parse(Console.ReadLine());
                                    Console.Clear();
                                    Console.Write("Podaj:\nX(0) = ");
                                    X[0] = Int32.Parse(Console.ReadLine());
                                    Console.Clear();
                                    try
                                    {
                                        using (StreamWriter plik = new StreamWriter("../raport.txt", true))
                                        {
                                            plik.WriteLine("-----------------------------------------------------------------");
                                            plik.WriteLine("------------------------------RAPORT-----------------------------");
                                            plik.WriteLine("-----------------------------------------------------------------");
                                            plik.WriteLine();

                                            plik.WriteLine("Gdzie:\t mod = {0},\t a = {1},\t b = {2},\t X(0) = {3};", mod, a, b, X[0]);
                                            plik.WriteLine();
                                            Console.Write("X     |");
                                            plik.Write("X     |");
                                            for (int i = 0; i < x.Length; i++)
                                            {
                                                x[i] = i;
                                                Console.Write(" " + x[i] + " |");
                                                plik.Write(" " + x[i] + " |");
                                                xod[i] = i;
                                            }

                                            odwrotnoscWynik[0] = x[0];

                                            Console.WriteLine();
                                            plik.WriteLine();

                                            for (int i = 0; i < x.Length; i++)
                                            {
                                                Console.Write("-----");
                                                plik.Write("-----");
                                            }
                                            Console.WriteLine();
                                            plik.WriteLine();
                                            Console.Write("X^(-1)| ");
                                            plik.Write("X^(-1)| ");
                                            Console.Write(odwrotnoscWynik[0] + " |");
                                            plik.Write(odwrotnoscWynik[0] + " |");
                                            for (int i = 1; i < x.Length; i++)
                                            {
                                                for (int k = 0; k < x.Length; k++)
                                                {
                                                    if (x[i] * xod[k] % mod == 1)
                                                    {
                                                        odwrotnoscWynik[i] = xod[k];
                                                    }
                                                }

                                                Console.Write(" " + odwrotnoscWynik[i] + " |");
                                                plik.Write(" " + odwrotnoscWynik[i] + " |");
                                            }
                                            Console.WriteLine("\n\n");
                                            plik.WriteLine("\n\n");
                                            Console.WriteLine("Obliczenie generatora nieliniowego liczb losowych: \n");
                                            plik.WriteLine("Obliczenie generatora nieliniowego liczb losowych: \n");

                                            for (int j = 1; j < X.Length; j++)
                                            {
                                                X[j] = (a * odwrotnoscWynik[X[j - 1]] + b) % mod;

                                                Console.WriteLine("X({0}) = ({1}*{2}+{3}) mod {4} = {5}", j, a, odwrotnoscWynik[X[j - 1]], b, mod, X[j]);
                                                plik.WriteLine("X({0}) = ({1}*{2}+{3}) mod {4} = {5}", j, a, odwrotnoscWynik[X[j - 1]], b, mod, X[j]);

                                                if (X[0] == X[j])
                                                {
                                                    Console.WriteLine("\nOkres generatora wynosi: {0}", j);
                                                    plik.WriteLine("\nOkres generatora wynosi: {0}", j);
                                                    break;
                                                }
                                            }
                                            plik.Close();
                                        }
                                    }
                                    catch (IndexOutOfRangeException ex)
                                    {
                                        Console.WriteLine("Wartośc X(0) nie może być większa od mod!");
                                        Console.WriteLine(ex.ToString());
                                    }
                                    catch (Exception ex)
                                    {
                                        Console.WriteLine("Plik został uszkodzony: " + ex.ToString());
                                    }

                                    break;
                                }
                                else if (czyPierwsza(mod) == false)
                                {
                                    Console.WriteLine("Nastepująca liczba {0} nie jest liczbą pierwszą", mod);
                                    Console.WriteLine("Operacja sie nie powiodła, wykonaj jeszcze raz operację!");

                                    break;
                                }
                            } while (czyPierwsza(mod) == false);
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine("Nie prawidłowa Wartość, błąd następujący: " + ex.ToString());
                        }
                        Console.ReadKey();
                        Console.Clear();

                        break;

                    case "R":
                        try
                        {
                            using (StreamReader plikOdczyt = new StreamReader("../raport.txt"))
                            {
                                string line;
                                while ((line = plikOdczyt.ReadLine()) != null)
                                {
                                    Console.WriteLine(line);
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine("Nieznajedziono pliku lub został uszkodzony: " + ex.ToString());
                        }
                        Console.ReadKey();
                        Console.Clear();
                        break;

                    case "D":
                        try
                        {
                            StreamWriter wyczysc = new StreamWriter("../raport.txt", false);
                            wyczysc.Close();
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine("Plik został uszkodzony: " + ex.ToString());
                        }
                        Console.ReadKey();
                        Console.Clear();
                        break;

                    case "Q":
                        Console.WriteLine("Czy chcesz napewno zakończyć program?");
                        Console.WriteLine("Aby zakończyć to użyj klawicza \"Q\" lub \"ESC\", jeśli nie to dowolny inny klawisz:");
                        keyInfo = Console.ReadKey();
                        Console.Clear();

                        break;

                    default:
                        Console.WriteLine("Nie ma takiej operacji!");
                        Console.WriteLine("Aby wykonać nastepującą operacje trzeba urzyć następujących klawiczy:");
                        Console.WriteLine("Oblicz i zapisz - W;\nWyswietl raport - R;\nWyczyć raport - D;");
                        Console.ReadKey();
                        Console.Clear();
                        break;
                }
            } while ((keyInfo.Key != ConsoleKey.Q) && (keyInfo.Key != ConsoleKey.Escape));
            Console.ReadKey();
        }
    }
}