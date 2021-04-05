using System;
using System.Collections.Generic;

namespace A133590.Ejercicio48
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Ejercicio 48");

            Stack<string> lotes = new Stack<string>();
            Queue<string> camiones = new Queue<string>();
            Dictionary<string, int> despachos = new Dictionary<string, int>();

            while (true)
            {
                Console.Write("> ");
                string comando = Console.ReadLine().Trim();
                if (comando.Length == 0)
                {
                    Console.WriteLine("Comando inválido.");
                    continue;
                }
                string[] cadenasComando = comando.Split();



                switch (cadenasComando[0])
                {
                    case "Lote":
                        if (cadenasComando.Length < 2)
                        {
                            Console.WriteLine("Comando correcto, error de sintaxis.");
                            break;
                        }

                        bool exito = true;
                        string codigoLote = cadenasComando[1];
                        if (codigoLote.Length == 6)
                        {
                            foreach (char c in codigoLote.Substring(0, 3)) exito &= Char.IsLetter(c);
                            foreach (char c in codigoLote.Substring(3, 3)) exito &= Char.IsDigit(c);
                        }
                        else exito = false;

                        if(lotes.Contains(codigoLote))
                        {
                            Console.WriteLine("Lote existente en el depósito");
                            continue;
                        }

                        if (exito)
                        {
                            lotes.Push(codigoLote);
                        }
                        else
                        {
                            Console.WriteLine("Código de lote inválido. Debe tener 3 letras al inicio y 3 números al final (Ej: ABC123)");
                            continue;
                        }
                        break;
                    case "Camión":
                        if (cadenasComando.Length < 4)
                        {
                            Console.WriteLine("Comando correcto, error de sintaxis.");
                            break;
                        }

                        string patente = cadenasComando[1];
                        exito = true;
                        foreach (char c in patente) exito &= Char.IsLetter(c);

                        if(!exito)
                        {
                            Console.WriteLine("Patente inválida, deben ser 6 letras");
                            continue;
                        }
                        if(despachos.ContainsKey(patente))
                        {
                            Console.WriteLine("Ya existe un camión con esta patente en espera..");
                            continue;
                        }

                        int cantidadDespachos = -1;
                        if (Int32.TryParse(cadenasComando[3], out cantidadDespachos))
                        {
                            if (cantidadDespachos > 0)
                            {
                                despachos.Add(patente, cantidadDespachos);
                                camiones.Enqueue(patente);
                            }
                            else
                            {
                                Console.WriteLine("Error: Número fuera del rango. Ingrese al menos 1 despacho.");
                            }
                        }
                        else
                        {
                            Console.WriteLine("Error de sintaxis. La cantidad de despachos debe ser un número.");
                        }
                        break;
                    case "fin":
                        Console.WriteLine("Presione cualquier tecla para continuar..");
                        Console.ReadKey();
                        return;
                    default:
                        Console.WriteLine("Comando inválido. Comandos disponibles: ");
                        Console.WriteLine("Lote [codigo]");
                        Console.WriteLine("Camión [patente] Despacho [cantidad]");
                        Console.WriteLine("fin");
                        continue;
                }

                while (lotes.Count > 0)
                {
                    if (camiones.Count > 0)
                    {
                        string patente = camiones.Peek();
                        int despachosRestantes = despachos[patente];
                        while (lotes.Count > 0 && despachosRestantes > 0)
                        {
                            Console.WriteLine($"Se le asigna el lote '{lotes.Pop()}' al camión de patente: {patente}");
                            despachosRestantes--;
                        }
                        if (despachosRestantes == 0)
                        {
                            Console.WriteLine($"Se ha completado el envío para el camión: {camiones.Dequeue()}");
                            despachos.Remove(patente);
                            if (camiones.Count > 0)
                            {
                                Console.WriteLine($"Próximo camión: {camiones.Peek()}");
                            }
                        }
                        else
                        {
                            despachos[patente] = despachosRestantes;
                        }
                    }
                    else break;
                }
                Console.WriteLine($"Camiones en espera: {camiones.Count}");
                Console.WriteLine($"Lotes en depósito: {lotes.Count}");

            }
        }
    }
}
